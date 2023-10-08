using UnityEngine;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Tilt shift")]
public partial class TiltShift : PostEffectsBase
{
    public Shader tiltShiftShader;
    private Material tiltShiftMaterial;
    public int renderTextureDivider;
    public int blurIterations;
    public bool enableForegroundBlur;
    public int foregroundBlurIterations;
    public float maxBlurSpread;
    public float focalPoint;
    public float smoothness;
    public bool visualizeCoc;
    // these values will be automatically determined
    private float start01;
    private float distance01;
    private float end01;
    private float curve;
    public override bool CheckResources()
    {
        CheckSupport(true);
        tiltShiftMaterial = CheckShaderAndCreateMaterial(tiltShiftShader, tiltShiftMaterial);
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
        // clamp some values
        renderTextureDivider = renderTextureDivider < 1 ? 1 : renderTextureDivider;
        renderTextureDivider = renderTextureDivider > 4 ? 4 : renderTextureDivider;
        blurIterations = blurIterations < 1 ? 0 : blurIterations;
        blurIterations = blurIterations > 4 ? 4 : blurIterations;
        // automagically calculate parameters based on focalPoint
        float focalPoint01 = GetComponent<Camera>().WorldToViewportPoint((focalPoint * GetComponent<Camera>().transform.forward) + GetComponent<Camera>().transform.position).z / GetComponent<Camera>().farClipPlane;
        distance01 = focalPoint01;
        start01 = 0f;
        end01 = 1f;
        start01 = Mathf.Min(focalPoint01 - Mathf.Epsilon, start01);
        end01 = Mathf.Max(focalPoint01 + Mathf.Epsilon, end01);
        curve = smoothness * distance01;
        // resources
        RenderTexture cocTex = RenderTexture.GetTemporary(source.width, source.height, 0);
        RenderTexture cocTex2 = RenderTexture.GetTemporary(source.width, source.height, 0);
        RenderTexture lrTex1 = RenderTexture.GetTemporary(source.width / renderTextureDivider, source.height / renderTextureDivider, 0);
        RenderTexture lrTex2 = RenderTexture.GetTemporary(source.width / renderTextureDivider, source.height / renderTextureDivider, 0);
        // coc		
        tiltShiftMaterial.SetVector("_SimpleDofParams", new Vector4(start01, distance01, end01, curve));
        tiltShiftMaterial.SetTexture("_Coc", cocTex);
        if (enableForegroundBlur)
        {
            Graphics.Blit(source, cocTex, tiltShiftMaterial, 0);
            Graphics.Blit(cocTex, lrTex1); // downwards (only really needed if lrTex resolution is different)
            int fgBlurIter = 0;
            while (fgBlurIter < foregroundBlurIterations)
            {
                tiltShiftMaterial.SetVector("offsets", new Vector4(0f, (maxBlurSpread * 0.75f) * oneOverBaseSize, 0f, 0f));
                Graphics.Blit(lrTex1, lrTex2, tiltShiftMaterial, 3);
                tiltShiftMaterial.SetVector("offsets", new Vector4(((maxBlurSpread * 0.75f) / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
                Graphics.Blit(lrTex2, lrTex1, tiltShiftMaterial, 3);
                fgBlurIter++;
            }
            Graphics.Blit(lrTex1, cocTex2, tiltShiftMaterial, 7); // upwards (only really needed if lrTex resolution is different)
            tiltShiftMaterial.SetTexture("_Coc", cocTex2);
        }
        else
        {
            RenderTexture.active = cocTex;
            GL.Clear(false, true, Color.black);
        }
        // combine coc's
        Graphics.Blit(source, cocTex, tiltShiftMaterial, 5);
        tiltShiftMaterial.SetTexture("_Coc", cocTex);
        // downsample & blur
        Graphics.Blit(source, lrTex2);
        int iter = 0;
        while (iter < blurIterations)
        {
            tiltShiftMaterial.SetVector("offsets", new Vector4(0f, (maxBlurSpread * 1f) * oneOverBaseSize, 0f, 0f));
            Graphics.Blit(lrTex2, lrTex1, tiltShiftMaterial, 6);
            tiltShiftMaterial.SetVector("offsets", new Vector4(((maxBlurSpread * 1f) / widthOverHeight) * oneOverBaseSize, 0f, 0f, 0f));
            Graphics.Blit(lrTex1, lrTex2, tiltShiftMaterial, 6);
            iter++;
        }
        tiltShiftMaterial.SetTexture("_Blurred", lrTex2);
        Graphics.Blit(source, destination, tiltShiftMaterial, visualizeCoc ? 4 : 1);
        RenderTexture.ReleaseTemporary(cocTex);
        RenderTexture.ReleaseTemporary(cocTex2);
        RenderTexture.ReleaseTemporary(lrTex1);
        RenderTexture.ReleaseTemporary(lrTex2);
    }

    public TiltShift()
    {
        renderTextureDivider = 2;
        blurIterations = 2;
        enableForegroundBlur = true;
        foregroundBlurIterations = 2;
        maxBlurSpread = 1.5f;
        focalPoint = 30f;
        smoothness = 1.65f;
        distance01 = 0.2f;
        end01 = 1f;
        curve = 1f;
    }

}