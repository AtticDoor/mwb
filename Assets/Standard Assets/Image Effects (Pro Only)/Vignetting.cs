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
        this.CheckSupport(false);
        this.vignetteMaterial = this.CheckShaderAndCreateMaterial(this.vignetteShader, this.vignetteMaterial);
        this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
        this.chromAberrationMaterial = this.CheckShaderAndCreateMaterial(this.chromAberrationShader, this.chromAberrationMaterial);
        if (!this.isSupported)
        {
            this.ReportAutoDisable();
        }
        return this.isSupported;
    }

    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (this.CheckResources() == false)
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
        Graphics.Blit(source, halfRezColor, this.chromAberrationMaterial, 0);
        Graphics.Blit(halfRezColor, quarterRezColor);
        int it = 0;
        while (it < 2)
        {
            this.separableBlurMaterial.SetVector("offsets", new Vector4(0f, this.blurSpread * oneOverBaseSize, 0f, 0f));
            Graphics.Blit(quarterRezColor, secondQuarterRezColor, this.separableBlurMaterial);
            this.separableBlurMaterial.SetVector("offsets", new Vector4((this.blurSpread * oneOverBaseSize) / widthOverHeight, 0f, 0f, 0f));
            Graphics.Blit(secondQuarterRezColor, quarterRezColor, this.separableBlurMaterial);
            it++;
        }
        this.vignetteMaterial.SetFloat("_Intensity", this.intensity);
        this.vignetteMaterial.SetFloat("_Blur", this.blur);
        this.vignetteMaterial.SetTexture("_VignetteTex", quarterRezColor);
        Graphics.Blit(source, color, this.vignetteMaterial);
        this.chromAberrationMaterial.SetFloat("_ChromaticAberration", this.chromaticAberration);
        Graphics.Blit(color, destination, this.chromAberrationMaterial, 1);
        RenderTexture.ReleaseTemporary(color);
        RenderTexture.ReleaseTemporary(halfRezColor);
        RenderTexture.ReleaseTemporary(quarterRezColor);
        RenderTexture.ReleaseTemporary(secondQuarterRezColor);
    }

    public Vignetting()
    {
        this.intensity = 0.375f;
        this.chromaticAberration = 0.2f;
        this.blur = 0.1f;
        this.blurSpread = 1.5f;
    }

}