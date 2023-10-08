using UnityEngine;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Vignette and Chromatic Aberration")]
public partial class Vignetting : PostEffectsBase
{
    public float intensity;
    public float chromaticAberration;
    public float blur;
    public float blurSpread;
    // needed shaders & materials
    public Shader vignetteShader;
    private Material vignetteMaterial;
    public Shader separableBlurShader;
    private Material separableBlurMaterial;
    public Shader chromAberrationShader;
    private Material chromAberrationMaterial;
    public override bool CheckResources()
    {
        CheckSupport(false);
        vignetteMaterial = CheckShaderAndCreateMaterial(vignetteShader, vignetteMaterial);
        separableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, separableBlurMaterial);
        chromAberrationMaterial = CheckShaderAndCreateMaterial(chromAberrationShader, chromAberrationMaterial);
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
        float widthOverHeight = (1f * source.width) / (1f * source.height);
        float oneOverBaseSize = 1f / 512f;
        RenderTexture color = RenderTexture.GetTemporary(source.width, source.height, 0);
        RenderTexture halfRezColor = RenderTexture.GetTemporary((int)(source.width / 2f), (int)(source.height / 2f), 0);
        RenderTexture quarterRezColor = RenderTexture.GetTemporary((int)(source.width / 4f), (int)(source.height / 4f), 0);
        RenderTexture secondQuarterRezColor = RenderTexture.GetTemporary((int)(source.width / 4f), (int)(source.height / 4f), 0);
        Graphics.Blit(source, halfRezColor, chromAberrationMaterial, 0);
        Graphics.Blit(halfRezColor, quarterRezColor);
        int it = 0;
        while (it < 2)
        {
            separableBlurMaterial.SetVector("offsets", new Vector4(0f, blurSpread * oneOverBaseSize, 0f, 0f));
            Graphics.Blit(quarterRezColor, secondQuarterRezColor, separableBlurMaterial);
            separableBlurMaterial.SetVector("offsets", new Vector4((blurSpread * oneOverBaseSize) / widthOverHeight, 0f, 0f, 0f));
            Graphics.Blit(secondQuarterRezColor, quarterRezColor, separableBlurMaterial);
            it++;
        }
        vignetteMaterial.SetFloat("_Intensity", intensity);
        vignetteMaterial.SetFloat("_Blur", blur);
        vignetteMaterial.SetTexture("_VignetteTex", quarterRezColor);
        Graphics.Blit(source, color, vignetteMaterial);
        chromAberrationMaterial.SetFloat("_ChromaticAberration", chromaticAberration);
        Graphics.Blit(color, destination, chromAberrationMaterial, 1);
        RenderTexture.ReleaseTemporary(color);
        RenderTexture.ReleaseTemporary(halfRezColor);
        RenderTexture.ReleaseTemporary(quarterRezColor);
        RenderTexture.ReleaseTemporary(secondQuarterRezColor);
    }

    public Vignetting()
    {
        intensity = 0.375f;
        chromaticAberration = 0.2f;
        blur = 0.1f;
        blurSpread = 1.5f;
    }

}