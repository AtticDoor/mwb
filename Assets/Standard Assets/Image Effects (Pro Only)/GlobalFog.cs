using UnityEngine;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Global Fog")]
public partial class GlobalFog : PostEffectsBase
{
    public enum FogMode
    {
        AbsoluteYAndDistance = 0,
        AbsoluteY = 1,
        Distance = 2,
        RelativeYAndDistance = 3
    }


    public GlobalFog.FogMode fogMode;
    private float CAMERA_NEAR;
    private float CAMERA_FAR;
    private float CAMERA_FOV;
    private float CAMERA_ASPECT_RATIO;
    public float startDistance;
    public float globalDensity;
    public float heightScale;
    public float height;
    public Color globalFogColor;
    public Shader fogShader;
    private Material fogMaterial;
    public override bool CheckResources()
    {
        CheckSupport(true);
        fogMaterial = CheckShaderAndCreateMaterial(fogShader, fogMaterial);
        if (!isSupported)
        {
            ReportAutoDisable();
        }
        return isSupported;
    }

    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Vector4 vec = default(Vector4);
        Vector3 corner = default(Vector3);
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        CAMERA_NEAR = GetComponent<Camera>().nearClipPlane;
        CAMERA_FAR = GetComponent<Camera>().farClipPlane;
        CAMERA_FOV = GetComponent<Camera>().fieldOfView;
        CAMERA_ASPECT_RATIO = GetComponent<Camera>().aspect;
        Matrix4x4 frustumCorners = Matrix4x4.identity;
        float fovWHalf = CAMERA_FOV * 0.5f;
        Vector3 toRight = ((GetComponent<Camera>().transform.right * CAMERA_NEAR) * Mathf.Tan(fovWHalf * Mathf.Deg2Rad)) * CAMERA_ASPECT_RATIO;
        Vector3 toTop = (GetComponent<Camera>().transform.up * CAMERA_NEAR) * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);
        Vector3 topLeft = ((GetComponent<Camera>().transform.forward * CAMERA_NEAR) - toRight) + toTop;
        float CAMERA_SCALE = (topLeft.magnitude * CAMERA_FAR) / CAMERA_NEAR;
        topLeft.Normalize();
        topLeft = topLeft * CAMERA_SCALE;
        Vector3 topRight = ((GetComponent<Camera>().transform.forward * CAMERA_NEAR) + toRight) + toTop;
        topRight.Normalize();
        topRight = topRight * CAMERA_SCALE;
        Vector3 bottomRight = ((GetComponent<Camera>().transform.forward * CAMERA_NEAR) + toRight) - toTop;
        bottomRight.Normalize();
        bottomRight = bottomRight * CAMERA_SCALE;
        Vector3 bottomLeft = ((GetComponent<Camera>().transform.forward * CAMERA_NEAR) - toRight) - toTop;
        bottomLeft.Normalize();
        bottomLeft = bottomLeft * CAMERA_SCALE;
        frustumCorners.SetRow(0, topLeft);
        frustumCorners.SetRow(1, topRight);
        frustumCorners.SetRow(2, bottomRight);
        frustumCorners.SetRow(3, bottomLeft);
        fogMaterial.SetMatrix("_FrustumCornersWS", frustumCorners);
        fogMaterial.SetVector("_CameraWS", GetComponent<Camera>().transform.position);
        fogMaterial.SetVector("_StartDistance", new Vector4(1f / startDistance, CAMERA_SCALE - startDistance));
        fogMaterial.SetVector("_Y", new Vector4(height, 1f / heightScale));
        fogMaterial.SetFloat("_GlobalDensity", globalDensity * 0.01f);
        fogMaterial.SetColor("_FogColor", globalFogColor);
        GlobalFog.CustomGraphicsBlit(source, destination, fogMaterial, (int)fogMode);
    }

    public static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
    {
        RenderTexture.active = dest;
        fxMaterial.SetTexture("_MainTex", source);
        GL.PushMatrix();
        GL.LoadOrtho();
        fxMaterial.SetPass(passNr);
        GL.Begin(GL.QUADS);
        GL.MultiTexCoord2(0, 0f, 0f);
        GL.Vertex3(0f, 0f, 3f); // BL
        GL.MultiTexCoord2(0, 1f, 0f);
        GL.Vertex3(1f, 0f, 2f); // BR
        GL.MultiTexCoord2(0, 1f, 1f);
        GL.Vertex3(1f, 1f, 1f); // TR
        GL.MultiTexCoord2(0, 0f, 1f);
        GL.Vertex3(0f, 1f, 0f); // TL
        GL.End();
        GL.PopMatrix();
    }

    public GlobalFog()
    {
        fogMode = FogMode.AbsoluteYAndDistance;
        CAMERA_NEAR = 0.5f;
        CAMERA_FAR = 50f;
        CAMERA_FOV = 60f;
        CAMERA_ASPECT_RATIO = 1.333333f;
        startDistance = 200f;
        globalDensity = 1f;
        heightScale = 100f;
        globalFogColor = Color.grey;
    }

}