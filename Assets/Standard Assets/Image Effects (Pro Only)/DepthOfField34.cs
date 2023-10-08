using UnityEngine;

public enum Dof34QualitySetting
{
    OnlyBackground = 1,
    BackgroundAndForeground = 2
}

public enum DofResolution
{
    High = 2,
    Medium = 3,
    Low = 4
}

public enum DofBlurriness
{
    Low = 1,
    High = 2,
    VeryHigh = 4
}

public enum BokehDestination
{
    Background = 1,
    Foreground = 2,
    BackgroundAndForeground = 3
}

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Depth of Field (3.4)")]
public partial class DepthOfField34 : PostEffectsBase
{
    private static int SMOOTH_DOWNSAMPLE_PASS;
    private static float BOKEH_EXTRA_BLUR;
    public Dof34QualitySetting quality;
    public DofResolution resolution;
    public bool simpleTweakMode;
    public float focalPoint;
    public float smoothness;
    public float focalZDistance;
    public float focalZStartCurve;
    public float focalZEndCurve;
    private float focalStartCurve;
    private float focalEndCurve;
    private float focalDistance01;
    public Transform objectFocus;
    public float focalSize;
    public DofBlurriness bluriness;
    public float maxBlurSpread;
    public float foregroundBlurExtrude;
    public Shader dofBlurShader;
    private Material dofBlurMaterial;
    public Shader dofShader;
    private Material dofMaterial;
    public bool visualize;
    public BokehDestination bokehDestination;
    private float widthOverHeight;
    private float oneOverBaseSize;
    public bool bokeh;
    public bool bokehSupport;
    public Shader bokehShader;
    public Texture2D bokehTexture;
    public float bokehScale;
    public float bokehIntensity;
    public float bokehThreshholdContrast;
    public float bokehThreshholdLuminance;
    public int bokehDownsample;
    private Material bokehMaterial;
    public virtual void CreateMaterials()
    {
        dofBlurMaterial = CheckShaderAndCreateMaterial(dofBlurShader, dofBlurMaterial);
        dofMaterial = CheckShaderAndCreateMaterial(dofShader, dofMaterial);
        bokehSupport = bokehShader.isSupported;
        if ((bokeh && bokehSupport) && bokehShader)
        {
            bokehMaterial = CheckShaderAndCreateMaterial(bokehShader, bokehMaterial);
        }
    }

    public override bool CheckResources()
    {
        CheckSupport(true);
        dofBlurMaterial = CheckShaderAndCreateMaterial(dofBlurShader, dofBlurMaterial);
        dofMaterial = CheckShaderAndCreateMaterial(dofShader, dofMaterial);
        bokehSupport = bokehShader.isSupported;
        if ((bokeh && bokehSupport) && bokehShader)
        {
            bokehMaterial = CheckShaderAndCreateMaterial(bokehShader, bokehMaterial);
        }
        if (!isSupported)
        {
            ReportAutoDisable();
        }
        return isSupported;
    }

    public virtual void OnDisable()
    {
        Quads.Cleanup();
    }

    public override void OnEnable()
    {
        GetComponent<Camera>().depthTextureMode = GetComponent<Camera>().depthTextureMode | DepthTextureMode.Depth;
    }

    public virtual float FocalDistance01(float worldDist)
    {
        return GetComponent<Camera>().WorldToViewportPoint(((worldDist - GetComponent<Camera>().nearClipPlane) * GetComponent<Camera>().transform.forward) + GetComponent<Camera>().transform.position).z / (GetComponent<Camera>().farClipPlane - GetComponent<Camera>().nearClipPlane);
    }

    public virtual int GetDividerBasedOnQuality()
    {
        int divider = 1;
        if (resolution == DofResolution.Medium)
        {
            divider = 2;
        }
        else
        {
            if (resolution == DofResolution.Low)
            {
                divider = 2;
            }
        }
        return divider;
    }

    public virtual int GetLowResolutionDividerBasedOnQuality(int baseDivider)
    {
        int lowTexDivider = baseDivider;
        if (resolution == DofResolution.High)
        {
            lowTexDivider = lowTexDivider * 2;
        }
        if (resolution == DofResolution.Low)
        {
            lowTexDivider = lowTexDivider * 2;
        }
        return lowTexDivider;
    }

    private RenderTexture foregroundTexture;
    private RenderTexture mediumRezWorkTexture;
    private RenderTexture finalDefocus;
    private RenderTexture lowRezWorkTexture;
    private RenderTexture bokehSource;
    private RenderTexture bokehSource2;
    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        if (smoothness < 0.1f)
        {
            smoothness = 0.1f;
        }
        // update needed focal & rt size parameter
        bokeh = bokeh && bokehSupport;
        float bokehBlurAmplifier = bokeh ? DepthOfField34.BOKEH_EXTRA_BLUR : 1f;
        bool blurForeground = quality > Dof34QualitySetting.OnlyBackground;
        float focal01Size = focalSize / (GetComponent<Camera>().farClipPlane - GetComponent<Camera>().nearClipPlane);
        if (simpleTweakMode)
        {
            focalDistance01 = objectFocus ? GetComponent<Camera>().WorldToViewportPoint(objectFocus.position).z / GetComponent<Camera>().farClipPlane : FocalDistance01(focalPoint);
            focalStartCurve = focalDistance01 * smoothness;
            focalEndCurve = focalStartCurve;
            blurForeground = blurForeground && (focalPoint > (GetComponent<Camera>().nearClipPlane + Mathf.Epsilon));
        }
        else
        {
            if (objectFocus)
            {
                Vector3 vpPoint = GetComponent<Camera>().WorldToViewportPoint(objectFocus.position);
                vpPoint.z = vpPoint.z / GetComponent<Camera>().farClipPlane;
                focalDistance01 = vpPoint.z;
            }
            else
            {
                focalDistance01 = FocalDistance01(focalZDistance);
            }
            focalStartCurve = focalZStartCurve;
            focalEndCurve = focalZEndCurve;
            blurForeground = blurForeground && (focalPoint > (GetComponent<Camera>().nearClipPlane + Mathf.Epsilon));
        }
        widthOverHeight = (1f * source.width) / (1f * source.height);
        oneOverBaseSize = 1f / 512f;
        dofMaterial.SetFloat("_ForegroundBlurExtrude", foregroundBlurExtrude);
        dofMaterial.SetVector("_CurveParams", new Vector4(simpleTweakMode ? 1f / focalStartCurve : focalStartCurve, simpleTweakMode ? 1f / focalEndCurve : focalEndCurve, focal01Size * 0.5f, focalDistance01));
        dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * source.width), 1f / (1f * source.height), 0f, 0f));
        int divider = GetDividerBasedOnQuality();
        int lowTexDivider = GetLowResolutionDividerBasedOnQuality(divider);
        AllocateTextures(blurForeground, source, divider, lowTexDivider);
        // WRITE COC to alpha channel		
        // source is only being bound to detect y texcoord flip
        Graphics.Blit(source, source, dofMaterial, 3);
        // better DOWNSAMPLE (could actually be weighted for higher quality)
        Downsample(source, mediumRezWorkTexture);
        // BLUR A LITTLE first, which has two purposes
        // 1.) reduce jitter, noise, aliasing
        // 2.) produce the little-blur buffer used in composition later     	     
        Blur(mediumRezWorkTexture, mediumRezWorkTexture, DofBlurriness.Low, 4, maxBlurSpread);
        if (bokeh && ((bokehDestination & BokehDestination.Background) != (BokehDestination)0))
        {
            dofMaterial.SetVector("_Threshhold", new Vector4(bokehThreshholdContrast, bokehThreshholdLuminance, 0.95f, 0f));
            // add and mark the parts that should end up as bokeh shapes
            Graphics.Blit(mediumRezWorkTexture, bokehSource2, dofMaterial, 11);
            // remove those parts (maybe even a little tittle bittle more) from the regurlarly blurred buffer		
            //Graphics.Blit (mediumRezWorkTexture, lowRezWorkTexture, dofMaterial, 10);
            Graphics.Blit(mediumRezWorkTexture, lowRezWorkTexture);//, dofMaterial, 10);
            // maybe you want to reblur the small blur ... but not really needed.
            //Blur (mediumRezWorkTexture, mediumRezWorkTexture, DofBlurriness.Low, 4, maxBlurSpread);						
            // bigger BLUR
            Blur(lowRezWorkTexture, lowRezWorkTexture, bluriness, 0, maxBlurSpread * bokehBlurAmplifier);
        }
        else
        {
            // bigger BLUR
            Downsample(mediumRezWorkTexture, lowRezWorkTexture);
            Blur(lowRezWorkTexture, lowRezWorkTexture, bluriness, 0, maxBlurSpread);
        }
        dofBlurMaterial.SetTexture("_TapLow", lowRezWorkTexture);
        dofBlurMaterial.SetTexture("_TapMedium", mediumRezWorkTexture);
        Graphics.Blit(null, finalDefocus, dofBlurMaterial, 3);
        // we are only adding bokeh now if the background is the only part we have to deal with
        if (bokeh && ((bokehDestination & BokehDestination.Background) != (BokehDestination)0))
        {
            AddBokeh(bokehSource2, bokehSource, finalDefocus);
        }
        dofMaterial.SetTexture("_TapLowBackground", finalDefocus);
        dofMaterial.SetTexture("_TapMedium", mediumRezWorkTexture); // needed for debugging/visualization
        // FINAL DEFOCUS (background)
        Graphics.Blit(source, blurForeground ? foregroundTexture : destination, dofMaterial, visualize ? 2 : 0);
        // FINAL DEFOCUS (foreground)
        if (blurForeground)
        {
            // WRITE COC to alpha channel			
            Graphics.Blit(foregroundTexture, source, dofMaterial, 5);
            // DOWNSAMPLE (unweighted)
            Downsample(source, mediumRezWorkTexture);
            // BLUR A LITTLE first, which has two purposes
            // 1.) reduce jitter, noise, aliasing
            // 2.) produce the little-blur buffer used in composition later   
            BlurFg(mediumRezWorkTexture, mediumRezWorkTexture, DofBlurriness.Low, 2, maxBlurSpread);
            if (bokeh && ((bokehDestination & BokehDestination.Foreground) != (BokehDestination)0))
            {
                dofMaterial.SetVector("_Threshhold", new Vector4(bokehThreshholdContrast * 0.5f, bokehThreshholdLuminance, 0f, 0f));
                // add and mark the parts that should end up as bokeh shapes
                Graphics.Blit(mediumRezWorkTexture, bokehSource2, dofMaterial, 11);
                // remove the parts (maybe even a little tittle bittle more) that will end up in bokeh space			
                //Graphics.Blit (mediumRezWorkTexture, lowRezWorkTexture, dofMaterial, 10);
                Graphics.Blit(mediumRezWorkTexture, lowRezWorkTexture);//, dofMaterial, 10);
                // big BLUR		
                BlurFg(lowRezWorkTexture, lowRezWorkTexture, bluriness, 1, maxBlurSpread * bokehBlurAmplifier);
            }
            else
            {
                // big BLUR		
                BlurFg(mediumRezWorkTexture, lowRezWorkTexture, bluriness, 1, maxBlurSpread);
            }
            // simple upsample once						
            Graphics.Blit(lowRezWorkTexture, finalDefocus);
            dofMaterial.SetTexture("_TapLowForeground", finalDefocus);
            Graphics.Blit(source, destination, dofMaterial, visualize ? 1 : 4);
            if (bokeh && ((bokehDestination & BokehDestination.Foreground) != (BokehDestination)0))
            {
                AddBokeh(bokehSource2, bokehSource, destination);
            }
        }
        ReleaseTextures();
    }

    public virtual void Blur(RenderTexture from, RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
    {
        RenderTexture tmp = RenderTexture.GetTemporary(to.width, to.height);
        if (iterations > (DofBlurriness)1)
        {
            BlurHex(from, to, blurPass, spread, tmp);
            if (iterations > (DofBlurriness)2)
            {
                dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
                Graphics.Blit(to, tmp, dofBlurMaterial, blurPass);
                dofBlurMaterial.SetVector("offsets", new Vector4((spread / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
                Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
            }
        }
        else
        {
            dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
            Graphics.Blit(from, tmp, dofBlurMaterial, blurPass);
            dofBlurMaterial.SetVector("offsets", new Vector4((spread / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
            Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
        }
        RenderTexture.ReleaseTemporary(tmp);
    }

    public virtual void BlurFg(RenderTexture from, RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
    {
        // we want a nice, big coc, hence we need to tap once from this (higher resolution) texture
        dofBlurMaterial.SetTexture("_TapHigh", from);
        RenderTexture tmp = RenderTexture.GetTemporary(to.width, to.height);
        if (iterations > (DofBlurriness)1)
        {
            BlurHex(from, to, blurPass, spread, tmp);
            if (iterations > (DofBlurriness)2)
            {
                dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
                Graphics.Blit(to, tmp, dofBlurMaterial, blurPass);
                dofBlurMaterial.SetVector("offsets", new Vector4((spread / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
                Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
            }
        }
        else
        {
            dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
            Graphics.Blit(from, tmp, dofBlurMaterial, blurPass);
            dofBlurMaterial.SetVector("offsets", new Vector4((spread / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
            Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
        }
        RenderTexture.ReleaseTemporary(tmp);
    }

    public virtual void BlurHex(RenderTexture from, RenderTexture to, int blurPass, float spread, RenderTexture tmp)
    {
        dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
        Graphics.Blit(from, tmp, dofBlurMaterial, blurPass);
        dofBlurMaterial.SetVector("offsets", new Vector4((spread / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
        Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
        dofBlurMaterial.SetVector("offsets", new Vector4((spread / widthOverHeight) * oneOverBaseSize, spread * oneOverBaseSize, 0f, 0f));
        Graphics.Blit(to, tmp, dofBlurMaterial, blurPass);
        dofBlurMaterial.SetVector("offsets", new Vector4((spread / widthOverHeight) * oneOverBaseSize, -spread * oneOverBaseSize, 0f, 0f));
        Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
    }

    public virtual void Downsample(RenderTexture from, RenderTexture to)
    {
        dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * to.width), 1f / (1f * to.height), 0f, 0f));
        Graphics.Blit(from, to, dofMaterial, DepthOfField34.SMOOTH_DOWNSAMPLE_PASS);
    }

    public virtual void AddBokeh(RenderTexture bokehInfo, RenderTexture tempTex, RenderTexture finalTarget)
    {
        if (bokehMaterial)
        {
            Mesh[] meshes = Quads.GetMeshes(tempTex.width, tempTex.height); // quads: exchanging more triangles with less overdraw			
            RenderTexture.active = tempTex;
            GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
            GL.PushMatrix();
            GL.LoadIdentity();
            // point filter mode is important, otherwise we get bokeh shape & size artefacts
            bokehInfo.filterMode = FilterMode.Point;
            float arW = (bokehInfo.width * 1f) / (bokehInfo.height * 1f);
            float sc = 2f / (1f * bokehInfo.width);
            sc = sc + (((bokehScale * maxBlurSpread) * DepthOfField34.BOKEH_EXTRA_BLUR) * oneOverBaseSize);
            bokehMaterial.SetTexture("_Source", bokehInfo);
            bokehMaterial.SetTexture("_MainTex", bokehTexture);
            bokehMaterial.SetVector("_ArScale", new Vector4(sc, sc * arW, 0.5f, 0.5f * arW));
            bokehMaterial.SetFloat("_Intensity", bokehIntensity);
            bokehMaterial.SetPass(0);
            foreach (Mesh m in meshes)
            {
                if (m)
                {
                    Graphics.DrawMeshNow(m, Matrix4x4.identity);
                }
            }
            GL.PopMatrix();
            Graphics.Blit(tempTex, finalTarget, dofMaterial, 8);
            // important to set back as we sample from this later on
            bokehInfo.filterMode = FilterMode.Bilinear;
        }
    }

    public virtual void ReleaseTextures()
    {
        if (foregroundTexture)
        {
            RenderTexture.ReleaseTemporary(foregroundTexture);
        }
        if (finalDefocus)
        {
            RenderTexture.ReleaseTemporary(finalDefocus);
        }
        if (mediumRezWorkTexture)
        {
            RenderTexture.ReleaseTemporary(mediumRezWorkTexture);
        }
        if (lowRezWorkTexture)
        {
            RenderTexture.ReleaseTemporary(lowRezWorkTexture);
        }
        if (bokehSource)
        {
            RenderTexture.ReleaseTemporary(bokehSource);
        }
        if (bokehSource2)
        {
            RenderTexture.ReleaseTemporary(bokehSource2);
        }
    }

    public virtual void AllocateTextures(bool blurForeground, RenderTexture source, int divider, int lowTexDivider)
    {
        foregroundTexture = null;
        if (blurForeground)
        {
            foregroundTexture = RenderTexture.GetTemporary(source.width, source.height, 0);
        }
        mediumRezWorkTexture = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
        finalDefocus = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
        lowRezWorkTexture = RenderTexture.GetTemporary(source.width / lowTexDivider, source.height / lowTexDivider, 0);
        bokehSource = null;
        bokehSource2 = null;
        if (bokeh)
        {
            bokehSource = RenderTexture.GetTemporary(source.width / (lowTexDivider * bokehDownsample), source.height / (lowTexDivider * bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
            bokehSource2 = RenderTexture.GetTemporary(source.width / (lowTexDivider * bokehDownsample), source.height / (lowTexDivider * bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
            bokehSource.filterMode = FilterMode.Bilinear;
            bokehSource2.filterMode = FilterMode.Bilinear;
            RenderTexture.active = bokehSource2;
            GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
        }
        // to make sure: always use bilinear filter setting
        source.filterMode = FilterMode.Bilinear;
        finalDefocus.filterMode = FilterMode.Bilinear;
        mediumRezWorkTexture.filterMode = FilterMode.Bilinear;
        lowRezWorkTexture.filterMode = FilterMode.Bilinear;
        if (foregroundTexture)
        {
            foregroundTexture.filterMode = FilterMode.Bilinear;
        }
    }

    public DepthOfField34()
    {
        quality = Dof34QualitySetting.OnlyBackground;
        resolution = DofResolution.Low;
        simpleTweakMode = true;
        focalPoint = 1f;
        smoothness = 0.5f;
        focalZStartCurve = 1f;
        focalZEndCurve = 1f;
        focalStartCurve = 2f;
        focalEndCurve = 2f;
        focalDistance01 = 0.1f;
        bluriness = DofBlurriness.High;
        maxBlurSpread = 1.75f;
        foregroundBlurExtrude = 1.15f;
        bokehDestination = BokehDestination.Background;
        widthOverHeight = 1.25f;
        oneOverBaseSize = 1f / 512f;
        bokehSupport = true;
        bokehScale = 2.4f;
        bokehIntensity = 0.15f;
        bokehThreshholdContrast = 0.1f;
        bokehThreshholdLuminance = 0.55f;
        bokehDownsample = 1;
    }

    static DepthOfField34()
    {
        DepthOfField34.SMOOTH_DOWNSAMPLE_PASS = 6;
        DepthOfField34.BOKEH_EXTRA_BLUR = 2f;
    }

}