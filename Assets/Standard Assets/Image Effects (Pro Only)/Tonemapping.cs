using UnityEngine;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Tonemapping")]
public partial class Tonemapping : PostEffectsBase
{
    public enum TonemapperType
    {
        SimpleReinhard = 0,
        UserCurve = 1,
        Hable = 2,
        Photographic = 3,
        OptimizedHejiDawson = 4,
        AdaptiveReinhard = 5,
        AdaptiveReinhardAutoWhite = 6
    }


    public enum AdaptiveTexSize
    {
        Square16 = 16,
        Square32 = 32,
        Square64 = 64,
        Square128 = 128,
        Square256 = 256,
        Square512 = 512,
        Square1024 = 1024
    }


    public Tonemapping.TonemapperType type;
    public Tonemapping.AdaptiveTexSize adaptiveTextureSize;
    // CURVE parameter
    public AnimationCurve remapCurve;
    private Texture2D curveTex;
    // UNCHARTED parameter
    public float exposureAdjustment;
    // REINHARD parameter
    public float middleGrey;
    public float white;
    public float adaptionSpeed;
    // usual & internal stuff
    public Shader tonemapper;
    public bool validRenderTextureFormat;
    private Material tonemapMaterial;
    private RenderTexture rt;
    public override bool CheckResources()
    {
        CheckSupport(false, true);
        tonemapMaterial = CheckShaderAndCreateMaterial(tonemapper, tonemapMaterial);
        if (!curveTex && (type == TonemapperType.UserCurve))
        {
            curveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
            curveTex.filterMode = FilterMode.Bilinear;
            curveTex.wrapMode = TextureWrapMode.Clamp;
            curveTex.hideFlags = HideFlags.DontSave;
        }
        if (!isSupported)
        {
            ReportAutoDisable();
        }
        return isSupported;
    }

    public virtual float UpdateCurve()
    {
        float range = 1f;
        if (remapCurve == null)
        {
            remapCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(2, 1) });
        }
        if (remapCurve != null)
        {
            if (remapCurve.length != 0)
            {
                range = remapCurve[remapCurve.length - 1].time;
            }
            float i = 0f;
            while (i <= 1f)
            {
                float c = remapCurve.Evaluate((i * 1f) * range);
                curveTex.SetPixel((int)Mathf.Floor(i * 255f), 0, new Color(c, c, c));
                i = i + (1f / 255f);
            }
            curveTex.Apply();
        }
        return 1f / range;
    }

    public virtual bool CreateInternalRenderTexture()
    {
        if (rt)
        {
            return false;
        }
        rt = new RenderTexture(1, 1, 0, RenderTextureFormat.ARGBHalf);
        RenderTexture oldrt = RenderTexture.active;
        RenderTexture.active = rt;
        GL.Clear(false, true, Color.white);
        rt.hideFlags = HideFlags.DontSave;
        RenderTexture.active = oldrt;
        return true;
    }

    // a new attribute we introduced in 3.5 indicating that the image filter chain will continue in LDR
    [UnityEngine.ImageEffectTransformsToLDR]
    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        // clamp some values to not go out of a valid range
        exposureAdjustment = exposureAdjustment < 0.001f ? 0.001f : exposureAdjustment;
        // SimpleReinhard tonemappers (local, non adaptive)
        if (type == TonemapperType.UserCurve)
        {
            float rangeScale = UpdateCurve();
            tonemapMaterial.SetFloat("_RangeScale", rangeScale);
            tonemapMaterial.SetTexture("_Curve", curveTex);
            Graphics.Blit(source, destination, tonemapMaterial, 4);
            return;
        }
        if (type == TonemapperType.SimpleReinhard)
        {
            tonemapMaterial.SetFloat("_ExposureAdjustment", exposureAdjustment);
            Graphics.Blit(source, destination, tonemapMaterial, 6);
            return;
        }
        if (type == TonemapperType.Hable)
        {
            tonemapMaterial.SetFloat("_ExposureAdjustment", exposureAdjustment);
            Graphics.Blit(source, destination, tonemapMaterial, 5);
            return;
        }
        if (type == TonemapperType.Photographic)
        {
            tonemapMaterial.SetFloat("_ExposureAdjustment", exposureAdjustment);
            Graphics.Blit(source, destination, tonemapMaterial, 8);
            return;
        }
        if (type == TonemapperType.OptimizedHejiDawson)
        {
            tonemapMaterial.SetFloat("_ExposureAdjustment", 0.5f * exposureAdjustment);
            Graphics.Blit(source, destination, tonemapMaterial, 7);
            return;
        }
        // still here? 
        // => more complex adaptive tone mapping:
        // builds an average log luminance, tonemaps according to 
        // middle grey and white values (user controlled)
        // AdaptiveReinhardAutoWhite will calculate white value automagically
        bool freshlyBrewedInternalRt = CreateInternalRenderTexture();
        RenderTexture rtSquared = RenderTexture.GetTemporary((int)adaptiveTextureSize, (int)adaptiveTextureSize, 0, RenderTextureFormat.ARGBHalf);
        Graphics.Blit(source, rtSquared);
        int downsample = (int)Mathf.Log(rtSquared.width * 1f, 2);
        int div = 2;
        RenderTexture[] rts = new RenderTexture[downsample];
        int i = 0;
        while (i < downsample)
        {
            rts[i] = RenderTexture.GetTemporary(rtSquared.width / div, rtSquared.width / div, 0, RenderTextureFormat.ARGBHalf);
            div = div * 2;
            i++;
        }
        float ar = (source.width * 1f) / (source.height * 1f);
        // downsample pyramid
        RenderTexture lumRt = rts[downsample - 1];
        Graphics.Blit(rtSquared, rts[0], tonemapMaterial, 1);
        if (type == TonemapperType.AdaptiveReinhardAutoWhite)
        {
            i = 0;
            while (i < (downsample - 1))
            {
                Graphics.Blit(rts[i], rts[i + 1], tonemapMaterial, 9);
                lumRt = rts[i + 1];
                i++;
            }
        }
        else
        {
            if (type == TonemapperType.AdaptiveReinhard)
            {
                i = 0;
                while (i < (downsample - 1))
                {
                    Graphics.Blit(rts[i], rts[i + 1]);
                    lumRt = rts[i + 1];
                    i++;
                }
            }
        }
        // we have the needed values, let's apply adaptive tonemapping
        adaptionSpeed = adaptionSpeed < 0.001f ? 0.001f : adaptionSpeed;
        tonemapMaterial.SetFloat("_AdaptionSpeed", adaptionSpeed);
        Graphics.Blit(lumRt, rt, tonemapMaterial, 2);
        middleGrey = middleGrey < 0.001f ? 0.001f : middleGrey;
        tonemapMaterial.SetVector("_HdrParams", new Vector4(middleGrey, middleGrey, middleGrey, white * white));
        tonemapMaterial.SetTexture("_SmallTex", rt);
        if (type == TonemapperType.AdaptiveReinhard)
        {
            Graphics.Blit(source, destination, tonemapMaterial, 0);
        }
        else
        {
            if (type == TonemapperType.AdaptiveReinhardAutoWhite)
            {
                Graphics.Blit(source, destination, tonemapMaterial, 10);
            }
            else
            {
                Debug.LogError("No valid adaptive tonemapper type found!");
                Graphics.Blit(source, destination); // at least we get the TransformToLDR effect
            }
        }
        // cleanup for adaptive
        i = 0;
        while (i < downsample)
        {
            RenderTexture.ReleaseTemporary(rts[i]);
            i++;
        }
        RenderTexture.ReleaseTemporary(rtSquared);
    }

    public Tonemapping()
    {
        type = TonemapperType.SimpleReinhard;
        adaptiveTextureSize = AdaptiveTexSize.Square256;
        exposureAdjustment = 1.5f;
        middleGrey = 0.4f;
        white = 2f;
        adaptionSpeed = 1.5f;
        validRenderTextureFormat = true;
    }

}