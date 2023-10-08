using UnityEngine;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Fisheye")]
public partial class Fisheye : PostEffectsBase
{
    public float strengthX;
    public float strengthY;
    public Shader fishEyeShader;
    private Material fisheyeMaterial;
    public override bool CheckResources()
    {
        CheckSupport(false);
        fisheyeMaterial = CheckShaderAndCreateMaterial(fishEyeShader, fisheyeMaterial);
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
        float oneOverBaseSize = 80f / 512f; // to keep values more like in the old version of fisheye
        float ar = (source.width * 1f) / (source.height * 1f);
        fisheyeMaterial.SetVector("intensity", new Vector4((strengthX * ar) * oneOverBaseSize, strengthY * oneOverBaseSize, (strengthX * ar) * oneOverBaseSize, strengthY * oneOverBaseSize));
        Graphics.Blit(source, destination, fisheyeMaterial);
    }

    public Fisheye()
    {
        strengthX = 0.05f;
        strengthY = 0.05f;
    }

}