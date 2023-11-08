using UnityEngine;

public enum LensflareStyle34
{
    Ghosting = 0,
    Anamorphic = 1,
    Combined = 2
}

public enum TweakMode34
{
    Basic = 0,
    Complex = 1
}

public enum HDRBloomMode
{
    Auto = 0,
    On = 1,
    Off = 2
}

public enum BloomScreenBlendMode
{
    Screen = 0,
    Add = 1
}

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Bloom (HDR, Lens Flares)")]
public partial class BloomAndLensFlares : PostEffectsBase
{
    public TweakMode34 tweakMode;
    public BloomScreenBlendMode screenBlendMode;
    public HDRBloomMode hdr;
    private bool doHdr;
    public float sepBlurSpread;
    public float useSrcAlphaAsMask;
    public float bloomIntensity;
    public float bloomThreshhold;
    public int bloomBlurIterations;
    public bool lensflares;
    public int hollywoodFlareBlurIterations;
    public LensflareStyle34 lensflareMode;
    public float hollyStretchWidth;
    public float lensflareIntensity;
    public float lensflareThreshhold;
    public Color flareColorA;
    public Color flareColorB;
    public Color flareColorC;
    public Color flareColorD;
    public float blurWidth;
    public Texture2D lensFlareVignetteMask;
    public Shader lensFlareShader;
    private Material lensFlareMaterial;
    public Shader vignetteShader;
    private Material vignetteMaterial;
    public Shader separableBlurShader;
    private Material separableBlurMaterial;
    public Shader addBrightStuffOneOneShader;
    private Material addBrightStuffBlendOneOneMaterial;
    public Shader screenBlendShader;
    private Material screenBlend;
    public Shader hollywoodFlaresShader;
    private Material hollywoodFlaresMaterial;
    public Shader brightPassFilterShader;
    private Material brightPassFilterMaterial;
    public override bool CheckResources()
    {
        CheckSupport(false);
        screenBlend = CheckShaderAndCreateMaterial(screenBlendShader, screenBlend);
        lensFlareMaterial = CheckShaderAndCreateMaterial(lensFlareShader, lensFlareMaterial);
        vignetteMaterial = CheckShaderAndCreateMaterial(vignetteShader, vignetteMaterial);
        separableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, separableBlurMaterial);
        addBrightStuffBlendOneOneMaterial = CheckShaderAndCreateMaterial(addBrightStuffOneOneShader, addBrightStuffBlendOneOneMaterial);
        hollywoodFlaresMaterial = CheckShaderAndCreateMaterial(hollywoodFlaresShader, hollywoodFlaresMaterial);
        brightPassFilterMaterial = CheckShaderAndCreateMaterial(brightPassFilterShader, brightPassFilterMaterial);
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
        // screen blend is not supported when HDR is enabled (will cap values)
        doHdr = false;
        if (hdr == HDRBloomMode.Auto)
        {
            doHdr = (source.format == RenderTextureFormat.ARGBHalf) && GetComponent<Camera>().allowHDR;
        }
        else
        {
            doHdr = hdr == HDRBloomMode.On;
        }
        doHdr = doHdr && supportHDRTextures;
        BloomScreenBlendMode realBlendMode = screenBlendMode;
        if (doHdr)
        {
            realBlendMode = BloomScreenBlendMode.Add;
        }
        RenderTextureFormat rtFormat = doHdr ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.Default;
        RenderTexture halfRezColor = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, rtFormat);
        RenderTexture quarterRezColor = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, rtFormat);
        RenderTexture secondQuarterRezColor = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, rtFormat);
        RenderTexture thirdQuarterRezColor = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, rtFormat);
        float widthOverHeight = (1f * source.width) / (1f * source.height);
        float oneOverBaseSize = 1f / 512f;
        // downsample
        Graphics.Blit(source, halfRezColor, screenBlend, 2); // <- 2 is stable downsample
        Graphics.Blit(halfRezColor, quarterRezColor, screenBlend, 2); // <- 2 is stable downsample	
        RenderTexture.ReleaseTemporary(halfRezColor);
        // cut colors (threshholding)			
        BrightFilter(bloomThreshhold, useSrcAlphaAsMask, quarterRezColor, secondQuarterRezColor);
        // blurring
        if (bloomBlurIterations < 1)
        {
            bloomBlurIterations = 1;
        }
        int iter = 0;
        while (iter < bloomBlurIterations)
        {
            float spreadForPass = (1f + (iter * 0.5f)) * sepBlurSpread;
            separableBlurMaterial.SetVector("offsets", new Vector4(0f, spreadForPass * oneOverBaseSize, 0f, 0f));
            Graphics.Blit(iter == 0 ? secondQuarterRezColor : quarterRezColor, thirdQuarterRezColor, separableBlurMaterial);
            separableBlurMaterial.SetVector("offsets", new Vector4((spreadForPass / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
            Graphics.Blit(thirdQuarterRezColor, quarterRezColor, separableBlurMaterial);
            iter++;
        }
        // lens flares: ghosting, anamorphic or a combination 
        if (lensflares)
        {
            if (lensflareMode == (LensflareStyle34)0)
            {
                BrightFilter(lensflareThreshhold, 0f, quarterRezColor, thirdQuarterRezColor);
                // smooth a little, this needs to be resolution dependent
                /*				
				separableBlurMaterial.SetVector ("offsets", Vector4 (0.0f, (2.0f) / (1.0f * quarterRezColor.height), 0.0f, 0.0f));	
				Graphics.Blit (thirdQuarterRezColor, secondQuarterRezColor, separableBlurMaterial);				
				separableBlurMaterial.SetVector ("offsets", Vector4 ((2.0f) / (1.0f * quarterRezColor.width), 0.0f, 0.0f, 0.0f));	
				Graphics.Blit (secondQuarterRezColor, thirdQuarterRezColor, separableBlurMaterial); 
				*/
                // no ugly edges!
                Vignette(0.975f, thirdQuarterRezColor, secondQuarterRezColor);
                BlendFlares(secondQuarterRezColor, quarterRezColor);
            }
            else
            {
                // (b) hollywood/anamorphic flares?
                // thirdQuarter has the brightcut unblurred colors
                // quarterRezColor is the blurred, brightcut buffer that will end up as bloom
                hollywoodFlaresMaterial.SetVector("_Threshhold", new Vector4(lensflareThreshhold, 1f / (1f - lensflareThreshhold), 0f, 0f));
                hollywoodFlaresMaterial.SetVector("tintColor", (new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a) * lensflareIntensity);
                Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, hollywoodFlaresMaterial, 2);
                Graphics.Blit(secondQuarterRezColor, thirdQuarterRezColor, hollywoodFlaresMaterial, 3);
                hollywoodFlaresMaterial.SetVector("offsets", new Vector4(((sepBlurSpread * 1f) / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
                hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth);
                Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, hollywoodFlaresMaterial, 1);
                hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth * 2f);
                Graphics.Blit(secondQuarterRezColor, thirdQuarterRezColor, hollywoodFlaresMaterial, 1);
                hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth * 4f);
                Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, hollywoodFlaresMaterial, 1);
                if (lensflareMode == (LensflareStyle34)1)
                {
                    int itera = 0;
                    while (itera < hollywoodFlareBlurIterations)
                    {
                        separableBlurMaterial.SetVector("offsets", new Vector4(((hollyStretchWidth * 2f) / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
                        Graphics.Blit(secondQuarterRezColor, thirdQuarterRezColor, separableBlurMaterial);
                        separableBlurMaterial.SetVector("offsets", new Vector4(((hollyStretchWidth * 2f) / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
                        Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, separableBlurMaterial);
                        itera++;
                    }
                    AddTo(1f, secondQuarterRezColor, quarterRezColor);
                }
                else
                {
                    // (c) combined
                    int ix = 0;
                    while (ix < hollywoodFlareBlurIterations)
                    {
                        separableBlurMaterial.SetVector("offsets", new Vector4(((hollyStretchWidth * 2f) / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
                        Graphics.Blit(secondQuarterRezColor, thirdQuarterRezColor, separableBlurMaterial);
                        separableBlurMaterial.SetVector("offsets", new Vector4(((hollyStretchWidth * 2f) / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
                        Graphics.Blit(thirdQuarterRezColor, secondQuarterRezColor, separableBlurMaterial);
                        ix++;
                    }
                    Vignette(1f, secondQuarterRezColor, thirdQuarterRezColor);
                    BlendFlares(thirdQuarterRezColor, secondQuarterRezColor);
                    AddTo(1f, secondQuarterRezColor, quarterRezColor);
                }
            }
        }
        // screen blend bloom results to color buffer
        screenBlend.SetFloat("_Intensity", bloomIntensity);
        screenBlend.SetTexture("_ColorBuffer", source);
        Graphics.Blit(quarterRezColor, destination, screenBlend, (int)realBlendMode);
        RenderTexture.ReleaseTemporary(quarterRezColor);
        RenderTexture.ReleaseTemporary(secondQuarterRezColor);
        RenderTexture.ReleaseTemporary(thirdQuarterRezColor);
    }

    private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
    {
        addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity_);
        Graphics.Blit(from, to, addBrightStuffBlendOneOneMaterial);
    }

    private void BlendFlares(RenderTexture from, RenderTexture to)
    {
        lensFlareMaterial.SetVector("colorA", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * lensflareIntensity);
        lensFlareMaterial.SetVector("colorB", new Vector4(flareColorB.r, flareColorB.g, flareColorB.b, flareColorB.a) * lensflareIntensity);
        lensFlareMaterial.SetVector("colorC", new Vector4(flareColorC.r, flareColorC.g, flareColorC.b, flareColorC.a) * lensflareIntensity);
        lensFlareMaterial.SetVector("colorD", new Vector4(flareColorD.r, flareColorD.g, flareColorD.b, flareColorD.a) * lensflareIntensity);
        Graphics.Blit(from, to, lensFlareMaterial);
    }

    private void BrightFilter(float thresh, float useAlphaAsMask, RenderTexture from, RenderTexture to)
    {
        if (doHdr)
        {
            brightPassFilterMaterial.SetVector("threshhold", new Vector4(thresh, 1f, 0f, 0f));
        }
        else
        {
            brightPassFilterMaterial.SetVector("threshhold", new Vector4(thresh, 1f / (1f - thresh), 0f, 0f));
        }
        brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useAlphaAsMask);
        Graphics.Blit(from, to, brightPassFilterMaterial);
    }

    private void Vignette(float amount, RenderTexture from, RenderTexture to)
    {
        if (lensFlareVignetteMask)
        {
            screenBlend.SetTexture("_ColorBuffer", lensFlareVignetteMask);
            Graphics.Blit(from, to, screenBlend, 3);
        }
        else
        {
            vignetteMaterial.SetFloat("vignetteIntensity", amount);
            Graphics.Blit(from, to, vignetteMaterial);
        }
    }

    public BloomAndLensFlares()
    {
        screenBlendMode = BloomScreenBlendMode.Add;
        hdr = HDRBloomMode.Auto;
        sepBlurSpread = 1.5f;
        useSrcAlphaAsMask = 0.5f;
        bloomIntensity = 1f;
        bloomThreshhold = 0.5f;
        bloomBlurIterations = 2;
        hollywoodFlareBlurIterations = 2;
        lensflareMode = (LensflareStyle34)1;
        hollyStretchWidth = 3.5f;
        lensflareIntensity = 1f;
        lensflareThreshhold = 0.3f;
        flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
        flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
        flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
        flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);
        blurWidth = 1f;
    }

}