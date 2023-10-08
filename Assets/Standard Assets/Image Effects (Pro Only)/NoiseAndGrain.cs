using UnityEngine;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Noise And Grain (Overlay)")]
public partial class NoiseAndGrain : PostEffectsBase
{
    public float strength;
    public float blackIntensity;
    public float whiteIntensity;
    public float redChannelNoise;
    public float greenChannelNoise;
    public float blueChannelNoise;
    public float redChannelTiling;
    public float greenChannelTiling;
    public float blueChannelTiling;
    public FilterMode filterMode;
    public Shader noiseShader;
    public Texture2D noiseTexture;
    private Material noiseMaterial;
    public override bool CheckResources()
    {
        CheckSupport(false);
        noiseMaterial = CheckShaderAndCreateMaterial(noiseShader, noiseMaterial);
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
        noiseMaterial.SetVector("_NoisePerChannel", new Vector3(redChannelNoise, greenChannelNoise, blueChannelNoise));
        noiseMaterial.SetVector("_NoiseTilingPerChannel", new Vector3(redChannelTiling, greenChannelTiling, blueChannelTiling));
        noiseMaterial.SetVector("_NoiseAmount", new Vector3(strength, blackIntensity, whiteIntensity));
        noiseMaterial.SetTexture("_NoiseTex", noiseTexture);
        noiseTexture.filterMode = filterMode;
        NoiseAndGrain.DrawNoiseQuadGrid(source, destination, noiseMaterial, noiseTexture, 0);
    }

    public static void DrawNoiseQuadGrid(RenderTexture source, RenderTexture dest, Material fxMaterial, Texture2D noise, int passNr)
    {
        RenderTexture.active = dest;
        float noiseSize = noise.width * 1f;
        float tileSize = noiseSize;
        float subDs = (1f * source.width) / tileSize;
        fxMaterial.SetTexture("_MainTex", source);
        GL.PushMatrix();
        GL.LoadOrtho();
        float aspectCorrection = (1f * source.width) / (1f * source.height);
        float stepSizeX = 1f / subDs;
        float stepSizeY = stepSizeX * aspectCorrection;
        float texTile = tileSize / (noise.width * 1f);
        fxMaterial.SetPass(passNr);
        GL.Begin(GL.QUADS);
        float x1 = 0f;
        while (x1 < 1f)
        {
            float y1 = 0f;
            while (y1 < 1f)
            {
                float tcXStart = Random.Range(0f, 1f);
                float tcYStart = Random.Range(0f, 1f);
                tcXStart = Mathf.Floor(tcXStart * noiseSize) / noiseSize;
                tcYStart = Mathf.Floor(tcYStart * noiseSize) / noiseSize;
                //var texTileMod : float = Mathf.Sign (Random.Range (-1.0f, 1.0f));
                float texTileMod = 1f / noiseSize;
                GL.MultiTexCoord2(0, tcXStart, tcYStart);
                GL.MultiTexCoord2(1, 0f, 0f);
                GL.Vertex3(x1, y1, 0.1f);
                GL.MultiTexCoord2(0, tcXStart + (texTile * texTileMod), tcYStart);
                GL.MultiTexCoord2(1, 1f, 0f);
                GL.Vertex3(x1 + stepSizeX, y1, 0.1f);
                GL.MultiTexCoord2(0, tcXStart + (texTile * texTileMod), tcYStart + (texTile * texTileMod));
                GL.MultiTexCoord2(1, 1f, 1f);
                GL.Vertex3(x1 + stepSizeX, y1 + stepSizeY, 0.1f);
                GL.MultiTexCoord2(0, tcXStart, tcYStart + (texTile * texTileMod));
                GL.MultiTexCoord2(1, 0f, 1f);
                GL.Vertex3(x1, y1 + stepSizeY, 0.1f);
                y1 = y1 + stepSizeY;
            }
            x1 = x1 + stepSizeX;
        }
        GL.End();
        GL.PopMatrix();
    }

    public NoiseAndGrain()
    {
        strength = 1f;
        blackIntensity = 1f;
        whiteIntensity = 1f;
        redChannelNoise = 0.975f;
        greenChannelNoise = 0.875f;
        blueChannelNoise = 1.2f;
        redChannelTiling = 24f;
        greenChannelTiling = 28f;
        blueChannelTiling = 34f;
        filterMode = FilterMode.Bilinear;
    }

}