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
        this.CheckSupport(false);
        this.noiseMaterial = this.CheckShaderAndCreateMaterial(this.noiseShader, this.noiseMaterial);
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
        this.noiseMaterial.SetVector("_NoisePerChannel", new Vector3(this.redChannelNoise, this.greenChannelNoise, this.blueChannelNoise));
        this.noiseMaterial.SetVector("_NoiseTilingPerChannel", new Vector3(this.redChannelTiling, this.greenChannelTiling, this.blueChannelTiling));
        this.noiseMaterial.SetVector("_NoiseAmount", new Vector3(this.strength, this.blackIntensity, this.whiteIntensity));
        this.noiseMaterial.SetTexture("_NoiseTex", this.noiseTexture);
        this.noiseTexture.filterMode = this.filterMode;
        NoiseAndGrain.DrawNoiseQuadGrid(source, destination, this.noiseMaterial, this.noiseTexture, 0);
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
        this.strength = 1f;
        this.blackIntensity = 1f;
        this.whiteIntensity = 1f;
        this.redChannelNoise = 0.975f;
        this.greenChannelNoise = 0.875f;
        this.blueChannelNoise = 1.2f;
        this.redChannelTiling = 24f;
        this.greenChannelTiling = 28f;
        this.blueChannelTiling = 34f;
        this.filterMode = FilterMode.Bilinear;
    }

}