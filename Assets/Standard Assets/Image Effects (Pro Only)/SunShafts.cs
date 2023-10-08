using UnityEngine;

public enum SunShaftsResolution
{
    Low = 0,
    Normal = 1,
    High = 2
}

public enum ShaftsScreenBlendMode
{
    Screen = 0,
    Add = 1
}

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Sun Shafts")]
public partial class SunShafts : PostEffectsBase
{
    public SunShaftsResolution resolution;
    public ShaftsScreenBlendMode screenBlendMode;
    public Transform sunTransform;
    public int radialBlurIterations;
    public Color sunColor;
    public float sunShaftBlurRadius;
    public float sunShaftIntensity;
    public float useSkyBoxAlpha;
    public float maxRadius;
    public bool useDepthTexture;
    public Shader sunShaftsShader;
    private Material sunShaftsMaterial;
    public Shader simpleClearShader;
    private Material simpleClearMaterial;
    public override bool CheckResources()
    {
        CheckSupport(useDepthTexture);
        sunShaftsMaterial = CheckShaderAndCreateMaterial(sunShaftsShader, sunShaftsMaterial);
        simpleClearMaterial = CheckShaderAndCreateMaterial(simpleClearShader, simpleClearMaterial);
        if (!isSupported)
        {
            ReportAutoDisable();
        }
        return isSupported;
    }

    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        // we actually need to check this every frame
        if (useDepthTexture)
        {
            GetComponent<Camera>().depthTextureMode = GetComponent<Camera>().depthTextureMode | DepthTextureMode.Depth;
        }
        float divider = 4f;
        if (resolution == SunShaftsResolution.Normal)
        {
            divider = 2f;
        }
        else
        {
            if (resolution == SunShaftsResolution.High)
            {
                divider = 1f;
            }
        }
        Vector3 v = Vector3.one * 0.5f;
        if (sunTransform)
        {
            v = GetComponent<Camera>().WorldToViewportPoint(sunTransform.position);
        }
        else
        {
            v = new Vector3(0.5f, 0.5f, 0f);
        }
        RenderTexture secondQuarterRezColor = RenderTexture.GetTemporary((int)(source.width / divider), (int)(source.height / divider), 0);
        RenderTexture lrDepthBuffer = RenderTexture.GetTemporary((int)(source.width / divider), (int)(source.height / divider), 0);
        // mask out everything except the skybox
        // we have 2 methods, one of which requires depth buffer support, the other one is just comparing images
        sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(1f, 1f, 0f, 0f) * sunShaftBlurRadius);
        sunShaftsMaterial.SetVector("_SunPosition", new Vector4(v.x, v.y, v.z, maxRadius));
        sunShaftsMaterial.SetFloat("_NoSkyBoxMask", 1f - useSkyBoxAlpha);
        if (!useDepthTexture)
        {
            RenderTexture tmpBuffer = RenderTexture.GetTemporary(source.width, source.height, 0);
            RenderTexture.active = tmpBuffer;
            GL.ClearWithSkybox(false, GetComponent<Camera>());
            sunShaftsMaterial.SetTexture("_Skybox", tmpBuffer);
            Graphics.Blit(source, lrDepthBuffer, sunShaftsMaterial, 3);
            RenderTexture.ReleaseTemporary(tmpBuffer);
        }
        else
        {
            Graphics.Blit(source, lrDepthBuffer, sunShaftsMaterial, 2);
        }
        // paint a small black small border to get rid of clamping problems
        DrawBorder(lrDepthBuffer, simpleClearMaterial);
        // radial blur:
        radialBlurIterations = ClampBlurIterationsToSomethingThatMakesSense(radialBlurIterations);
        float ofs = sunShaftBlurRadius * (1f / 768f);
        sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0f, 0f));
        sunShaftsMaterial.SetVector("_SunPosition", new Vector4(v.x, v.y, v.z, maxRadius));
        int it2 = 0;
        while (it2 < radialBlurIterations)
        {
            // each iteration takes 2 * 6 samples
            // we update _BlurRadius each time to cheaply get a very smooth look
            Graphics.Blit(lrDepthBuffer, secondQuarterRezColor, sunShaftsMaterial, 1);
            ofs = (sunShaftBlurRadius * (((it2 * 2f) + 1f) * 6f)) / 768f;
            sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0f, 0f));
            Graphics.Blit(secondQuarterRezColor, lrDepthBuffer, sunShaftsMaterial, 1);
            ofs = (sunShaftBlurRadius * (((it2 * 2f) + 2f) * 6f)) / 768f;
            sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0f, 0f));
            it2++;
        }
        // put together:
        if (v.z >= 0f)
        {
            sunShaftsMaterial.SetVector("_SunColor", new Vector4(sunColor.r, sunColor.g, sunColor.b, sunColor.a) * sunShaftIntensity);
        }
        else
        {
            sunShaftsMaterial.SetVector("_SunColor", Vector4.zero); // no backprojection !
        }
        sunShaftsMaterial.SetTexture("_ColorBuffer", lrDepthBuffer);
        Graphics.Blit(source, destination, sunShaftsMaterial, screenBlendMode == ShaftsScreenBlendMode.Screen ? 0 : 4);
        RenderTexture.ReleaseTemporary(lrDepthBuffer);
        RenderTexture.ReleaseTemporary(secondQuarterRezColor);
    }

    // helper functions
    private int ClampBlurIterationsToSomethingThatMakesSense(int its)
    {
        if (its < 1)
        {
            return 1;
        }
        else
        {
            if (its > 4)
            {
                return 4;
            }
            else
            {
                return its;
            }
        }
    }

    public SunShafts()
    {
        resolution = SunShaftsResolution.Normal;
        screenBlendMode = ShaftsScreenBlendMode.Screen;
        radialBlurIterations = 2;
        sunColor = Color.white;
        sunShaftBlurRadius = 2.5f;
        sunShaftIntensity = 1.15f;
        useSkyBoxAlpha = 0.75f;
        maxRadius = 0.75f;
        useDepthTexture = true;
    }

}