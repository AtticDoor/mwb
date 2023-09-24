using UnityEngine;
using System.Collections;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Depth of Field (HDR, Scatter, Lens Blur)")]
public partial class DepthOfFieldScatter : PostEffectsBase
{
    public bool visualizeFocus;
    public float focalLength;
    public float focalSize;
    public float aperture;
    public Transform focalTransform;
    public float maxBlurSize;
    public enum BlurQuality
    {
        Low = 0,
        Medium = 1,
        High = 2
    }


    public enum BlurResolution
    {
        High = 0,
        Low = 1
    }


    public DepthOfFieldScatter.BlurQuality blurQuality;
    public DepthOfFieldScatter.BlurResolution blurResolution;
    public bool foregroundBlur;
    public float foregroundOverlap;
    public Shader dofHdrShader;
    private float focalDistance01;
    private Material dofHdrMaterial;
    public override bool CheckResources()
    {
        this.CheckSupport(true);
        this.dofHdrMaterial = this.CheckShaderAndCreateMaterial(this.dofHdrShader, this.dofHdrMaterial);
        if (!this.isSupported)
        {
            this.ReportAutoDisable();
        }
        return this.isSupported;
    }

    public virtual float FocalDistance01(float worldDist)
    {
        return this.GetComponent<Camera>().WorldToViewportPoint(((worldDist - this.GetComponent<Camera>().nearClipPlane) * this.GetComponent<Camera>().transform.forward) + this.GetComponent<Camera>().transform.position).z / (this.GetComponent<Camera>().farClipPlane - this.GetComponent<Camera>().nearClipPlane);
    }

    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (this.CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        int i = 0;
        float internalBlurWidth = this.maxBlurSize;
        int blurRtDivider = this.blurResolution == BlurResolution.High ? 1 : 2;
        // clamp values so they make sense
        if (this.aperture < 0f)
        {
            this.aperture = 0f;
        }
        if (this.maxBlurSize < 0f)
        {
            this.maxBlurSize = 0f;
        }
        this.focalSize = Mathf.Clamp(this.focalSize, 0f, 0.3f);
        // focal & coc calculations
        this.focalDistance01 = this.focalTransform ? this.GetComponent<Camera>().WorldToViewportPoint(this.focalTransform.position).z / this.GetComponent<Camera>().farClipPlane : this.FocalDistance01(this.focalLength);
        bool isInHdr = source.format == RenderTextureFormat.ARGBHalf;
        RenderTexture scene = blurRtDivider > 1 ? RenderTexture.GetTemporary(source.width / blurRtDivider, source.height / blurRtDivider, 0, source.format) : null;
        if (scene)
        {
            scene.filterMode = FilterMode.Bilinear;
        }
        RenderTexture rtLow = RenderTexture.GetTemporary(source.width / (2 * blurRtDivider), source.height / (2 * blurRtDivider), 0, source.format);
        RenderTexture rtLow2 = RenderTexture.GetTemporary(source.width / (2 * blurRtDivider), source.height / (2 * blurRtDivider), 0, source.format);
        if (rtLow)
        {
            rtLow.filterMode = FilterMode.Bilinear;
        }
        if (rtLow2)
        {
            rtLow2.filterMode = FilterMode.Bilinear;
        }
        this.dofHdrMaterial.SetVector("_CurveParams", new Vector4(0f, this.focalSize, this.aperture / 10f, this.focalDistance01));
        // foreground blur
        if (this.foregroundBlur)
        {
            RenderTexture rtLowTmp = RenderTexture.GetTemporary(source.width / (2 * blurRtDivider), source.height / (2 * blurRtDivider), 0, source.format);
            // Capture foreground CoC only in alpha channel and increase CoC radius
            Graphics.Blit(source, rtLow2, this.dofHdrMaterial, 4);
            this.dofHdrMaterial.SetTexture("_FgOverlap", rtLow2);
            float fgAdjustment = (internalBlurWidth * this.foregroundOverlap) * 0.225f;
            this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, fgAdjustment, 0f, fgAdjustment));
            Graphics.Blit(rtLow2, rtLowTmp, this.dofHdrMaterial, 2);
            this.dofHdrMaterial.SetVector("_Offsets", new Vector4(fgAdjustment, 0f, 0f, fgAdjustment));
            Graphics.Blit(rtLowTmp, rtLow, this.dofHdrMaterial, 2);
            this.dofHdrMaterial.SetTexture("_FgOverlap", null); // NEW: not needed anymore
            // apply adjust FG coc back to high rez coc texture
            Graphics.Blit(rtLow, source, this.dofHdrMaterial, 7);
            RenderTexture.ReleaseTemporary(rtLowTmp);
        }
        else
        {
            this.dofHdrMaterial.SetTexture("_FgOverlap", null); // ugly FG overlaps as a result
        }
        // capture remaing CoC (fore & *background*)
        Graphics.Blit(source, source, this.dofHdrMaterial, this.foregroundBlur ? 3 : 0);
        RenderTexture cocRt = source;
        if (blurRtDivider > 1)
        {
            Graphics.Blit(source, scene, this.dofHdrMaterial, 6);
            cocRt = scene;
        }
        // spawn a few low rez parts in high rez image to get a bigger blur
        // resulting quality is higher than directly blending preblurred buffers
        Graphics.Blit(cocRt, rtLow2, this.dofHdrMaterial, 6);
        Graphics.Blit(rtLow2, cocRt, this.dofHdrMaterial, 8);
        //  blur and apply to color buffer 
        int blurPassNumber = 10;
        switch (this.blurQuality)
        {
            case BlurQuality.Low:
                blurPassNumber = blurRtDivider > 1 ? 13 : 10;
                break;
            case BlurQuality.Medium:
                blurPassNumber = blurRtDivider > 1 ? 12 : 11;
                break;
            case BlurQuality.High:
                blurPassNumber = blurRtDivider > 1 ? 15 : 14;
                break;
            default:
                Debug.Log("DOF couldn't find valid blur quality setting", this.transform);
                break;
        }
        if (this.visualizeFocus)
        {
            Graphics.Blit(source, destination, this.dofHdrMaterial, 1);
        }
        else
        {
            this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, 0f, 0f, internalBlurWidth));
            this.dofHdrMaterial.SetTexture("_LowRez", cocRt); // only needed in low resolution profile. and then, ugh, we get an ugly transition from nonblur -> blur areas
            Graphics.Blit(source, destination, this.dofHdrMaterial, blurPassNumber);
        }
        if (rtLow)
        {
            RenderTexture.ReleaseTemporary(rtLow);
        }
        if (rtLow2)
        {
            RenderTexture.ReleaseTemporary(rtLow2);
        }
        if (scene)
        {
            RenderTexture.ReleaseTemporary(scene);
        }
    }

    public DepthOfFieldScatter()
    {
        this.focalLength = 10f;
        this.focalSize = 0.05f;
        this.aperture = 10f;
        this.maxBlurSize = 2f;
        this.blurQuality = BlurQuality.Medium;
        this.blurResolution = BlurResolution.Low;
        this.foregroundOverlap = 0.55f;
        this.focalDistance01 = 10f;
    }

}