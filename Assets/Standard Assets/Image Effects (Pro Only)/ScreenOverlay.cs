using UnityEngine;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Screen Overlay")]
public partial class ScreenOverlay : PostEffectsBase
{
    public enum OverlayBlendMode
    {
        AddSub = 0,
        ScreenBlend = 1,
        Multiply = 2,
        Overlay = 3,
        AlphaBlend = 4
    }


    public ScreenOverlay.OverlayBlendMode blendMode;
    public float intensity;
    public Texture2D texture;
    public Shader overlayShader;
    private Material overlayMaterial;
    public override bool CheckResources()
    {
        CheckSupport(false);
        overlayMaterial = CheckShaderAndCreateMaterial(overlayShader, overlayMaterial);
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
        overlayMaterial.SetFloat("_Intensity", intensity);
        overlayMaterial.SetTexture("_Overlay", texture);
        Graphics.Blit(source, destination, overlayMaterial, (int)blendMode);
    }

    public ScreenOverlay()
    {
        blendMode = OverlayBlendMode.Overlay;
        intensity = 1f;
    }

}