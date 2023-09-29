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
        this.CheckSupport(true);
        this.edgeDetectMaterial = this.CheckShaderAndCreateMaterial(this.edgeDetectShader, this.edgeDetectMaterial);
        if (!this.isSupported)
        {
            this.ReportAutoDisable();
        }
        return this.isSupported;
    }

    [UnityEngine.ImageEffectOpaque]
    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (this.CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        Vector2 sensitivity = new Vector2(this.sensitivityDepth, this.sensitivityNormals);
        source.filterMode = FilterMode.Point;
        this.edgeDetectMaterial.SetVector("sensitivity", new Vector4(sensitivity.x, sensitivity.y, 1f, sensitivity.y));
        this.edgeDetectMaterial.SetFloat("_BgFade", this.edgesOnly);
        Vector4 vecCol = this.edgesOnlyBgColor;
        this.edgeDetectMaterial.SetVector("_BgColor", vecCol);
        if (this.mode == EdgeDetectMode.Thin)
        {
            Graphics.Blit(source, destination, this.edgeDetectMaterial, 0);
        }
        else
        {
            Graphics.Blit(source, destination, this.edgeDetectMaterial, 1);
        }
    }

    public EdgeDetectEffectNormals()
    {
        this.mode = EdgeDetectMode.Thin;
        this.sensitivityDepth = 1f;
        this.sensitivityNormals = 1f;
        this.edgesOnlyBgColor = Color.white;
    }

}