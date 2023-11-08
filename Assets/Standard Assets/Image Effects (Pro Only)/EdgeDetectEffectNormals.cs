using UnityEngine;

public enum EdgeDetectMode
{
    Thin = 0,
    Thick = 1
}

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Edge Detection (Geometry)")]
public partial class EdgeDetectEffectNormals : PostEffectsBase
{
    public EdgeDetectMode mode;
    public float sensitivityDepth;
    public float sensitivityNormals;
    public float edgesOnly;
    public Color edgesOnlyBgColor;
    public Shader edgeDetectShader;
    private Material edgeDetectMaterial;
    public override bool CheckResources()
    {
        CheckSupport(true);
        edgeDetectMaterial = CheckShaderAndCreateMaterial(edgeDetectShader, edgeDetectMaterial);
        if (!isSupported)
        {
            ReportAutoDisable();
        }
        return isSupported;
    }

    [UnityEngine.ImageEffectOpaque]
    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        Vector2 sensitivity = new Vector2(sensitivityDepth, sensitivityNormals);
        source.filterMode = FilterMode.Point;
        edgeDetectMaterial.SetVector("sensitivity", new Vector4(sensitivity.x, sensitivity.y, 1f, sensitivity.y));
        edgeDetectMaterial.SetFloat("_BgFade", edgesOnly);
        Vector4 vecCol = edgesOnlyBgColor;
        edgeDetectMaterial.SetVector("_BgColor", vecCol);
        if (mode == EdgeDetectMode.Thin)
        {
            Graphics.Blit(source, destination, edgeDetectMaterial, 0);
        }
        else
        {
            Graphics.Blit(source, destination, edgeDetectMaterial, 1);
        }
    }

    public EdgeDetectEffectNormals()
    {
        mode = EdgeDetectMode.Thin;
        sensitivityDepth = 1f;
        sensitivityNormals = 1f;
        edgesOnlyBgColor = Color.white;
    }

}