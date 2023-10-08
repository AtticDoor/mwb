using UnityEngine;

public enum ColorCorrectionMode
{
    Simple = 0,
    Advanced = 1
}

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.AddComponentMenu("Image Effects/Color Correction")]
public partial class ColorCorrectionCurves : PostEffectsBase
{
    public AnimationCurve redChannel;
    public AnimationCurve greenChannel;
    public AnimationCurve blueChannel;
    public bool useDepthCorrection;
    public AnimationCurve zCurve;
    public AnimationCurve depthRedChannel;
    public AnimationCurve depthGreenChannel;
    public AnimationCurve depthBlueChannel;
    private Material ccMaterial;
    private Material ccDepthMaterial;
    private Material selectiveCcMaterial;
    private Texture2D rgbChannelTex;
    private Texture2D rgbDepthChannelTex;
    private Texture2D zCurveTex;
    public bool selectiveCc;
    public Color selectiveFromColor;
    public Color selectiveToColor;
    public ColorCorrectionMode mode;
    public bool updateTextures;
    public Shader colorCorrectionCurvesShader;
    public Shader simpleColorCorrectionCurvesShader;
    public Shader colorCorrectionSelectiveShader;
    private bool updateTexturesOnStartup;
    public override void Start()
    {
        base.Start();
        updateTexturesOnStartup = true;
    }

    public virtual void Awake()
    {
    }

    public override bool CheckResources()
    {
        CheckSupport(mode == ColorCorrectionMode.Advanced);
        ccMaterial = CheckShaderAndCreateMaterial(simpleColorCorrectionCurvesShader, ccMaterial);
        ccDepthMaterial = CheckShaderAndCreateMaterial(colorCorrectionCurvesShader, ccDepthMaterial);
        selectiveCcMaterial = CheckShaderAndCreateMaterial(colorCorrectionSelectiveShader, selectiveCcMaterial);
        if (!rgbChannelTex)
        {
            rgbChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
        }
        if (!rgbDepthChannelTex)
        {
            rgbDepthChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
        }
        if (!zCurveTex)
        {
            zCurveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
        }
        rgbChannelTex.hideFlags = HideFlags.DontSave;
        rgbDepthChannelTex.hideFlags = HideFlags.DontSave;
        zCurveTex.hideFlags = HideFlags.DontSave;
        rgbChannelTex.wrapMode = TextureWrapMode.Clamp;
        rgbDepthChannelTex.wrapMode = TextureWrapMode.Clamp;
        zCurveTex.wrapMode = TextureWrapMode.Clamp;
        if (!isSupported)
        {
            ReportAutoDisable();
        }
        return isSupported;
    }

    public virtual void UpdateParameters()
    {
        if (((redChannel != null) && (greenChannel != null)) && (blueChannel != null))
        {
            float i = 0f;
            while (i <= 1f)
            {
                float rCh = Mathf.Clamp(redChannel.Evaluate(i), 0f, 1f);
                float gCh = Mathf.Clamp(greenChannel.Evaluate(i), 0f, 1f);
                float bCh = Mathf.Clamp(blueChannel.Evaluate(i), 0f, 1f);
                rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255f), 0, new Color(rCh, rCh, rCh));
                rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255f), 1, new Color(gCh, gCh, gCh));
                rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255f), 2, new Color(bCh, bCh, bCh));
                float zC = Mathf.Clamp(zCurve.Evaluate(i), 0f, 1f);
                zCurveTex.SetPixel((int)Mathf.Floor(i * 255f), 0, new Color(zC, zC, zC));
                rCh = Mathf.Clamp(depthRedChannel.Evaluate(i), 0f, 1f);
                gCh = Mathf.Clamp(depthGreenChannel.Evaluate(i), 0f, 1f);
                bCh = Mathf.Clamp(depthBlueChannel.Evaluate(i), 0f, 1f);
                rgbDepthChannelTex.SetPixel((int)Mathf.Floor(i * 255f), 0, new Color(rCh, rCh, rCh));
                rgbDepthChannelTex.SetPixel((int)Mathf.Floor(i * 255f), 1, new Color(gCh, gCh, gCh));
                rgbDepthChannelTex.SetPixel((int)Mathf.Floor(i * 255f), 2, new Color(bCh, bCh, bCh));
                i = i + (1f / 255f);
            }
            rgbChannelTex.Apply();
            rgbDepthChannelTex.Apply();
            zCurveTex.Apply();
        }
    }

    public virtual void UpdateTextures()
    {
        UpdateParameters();
    }

    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        if (updateTexturesOnStartup)
        {
            UpdateParameters();
            updateTexturesOnStartup = false;
        }
        if (useDepthCorrection)
        {
            GetComponent<Camera>().depthTextureMode = GetComponent<Camera>().depthTextureMode | DepthTextureMode.Depth;
        }
        RenderTexture renderTarget2Use = destination;
        if (selectiveCc)
        {
            renderTarget2Use = RenderTexture.GetTemporary(source.width, source.height);
        }
        if (useDepthCorrection)
        {
            ccDepthMaterial.SetTexture("_RgbTex", rgbChannelTex);
            ccDepthMaterial.SetTexture("_ZCurve", zCurveTex);
            ccDepthMaterial.SetTexture("_RgbDepthTex", rgbDepthChannelTex);
            Graphics.Blit(source, renderTarget2Use, ccDepthMaterial);
        }
        else
        {
            ccMaterial.SetTexture("_RgbTex", rgbChannelTex);
            Graphics.Blit(source, renderTarget2Use, ccMaterial);
        }
        if (selectiveCc)
        {
            selectiveCcMaterial.SetColor("selColor", selectiveFromColor);
            selectiveCcMaterial.SetColor("targetColor", selectiveToColor);
            Graphics.Blit(renderTarget2Use, destination, selectiveCcMaterial);
            RenderTexture.ReleaseTemporary(renderTarget2Use);
        }
    }

    public ColorCorrectionCurves()
    {
        selectiveFromColor = Color.white;
        selectiveToColor = Color.white;
        updateTextures = true;
        updateTexturesOnStartup = true;
    }

}