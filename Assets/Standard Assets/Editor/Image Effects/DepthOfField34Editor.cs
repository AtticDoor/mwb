using UnityEditor;
using UnityEngine;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(DepthOfField34))]
public class DepthOfField34Editor : Editor
{
    public SerializedObject serObj;
    public SerializedProperty simpleTweakMode;
    public SerializedProperty focalPoint;
    public SerializedProperty smoothness;
    public SerializedProperty focalSize;
    public SerializedProperty focalZDistance;
    public SerializedProperty focalStartCurve;
    public SerializedProperty focalEndCurve;
    public SerializedProperty visualizeCoc;
    public SerializedProperty resolution;
    public SerializedProperty quality;
    public SerializedProperty objectFocus;
    public SerializedProperty bokeh;
    public SerializedProperty bokehScale;
    public SerializedProperty bokehIntensity;
    public SerializedProperty bokehThreshholdLuminance;
    public SerializedProperty bokehThreshholdContrast;
    public SerializedProperty bokehDownsample;
    public SerializedProperty bokehTexture;
    public SerializedProperty bokehDestination;
    public SerializedProperty bluriness;
    public SerializedProperty maxBlurSpread;
    public SerializedProperty foregroundBlurExtrude;
    public virtual void OnEnable()
    {
        serObj = new SerializedObject(target);
        simpleTweakMode = serObj.FindProperty("simpleTweakMode");
        // simple tweak mode
        focalPoint = serObj.FindProperty("focalPoint");
        smoothness = serObj.FindProperty("smoothness");
        // complex tweak mode
        focalZDistance = serObj.FindProperty("focalZDistance");
        focalStartCurve = serObj.FindProperty("focalZStartCurve");
        focalEndCurve = serObj.FindProperty("focalZEndCurve");
        focalSize = serObj.FindProperty("focalSize");
        visualizeCoc = serObj.FindProperty("visualize");
        objectFocus = serObj.FindProperty("objectFocus");
        resolution = serObj.FindProperty("resolution");
        quality = serObj.FindProperty("quality");
        bokehThreshholdContrast = serObj.FindProperty("bokehThreshholdContrast");
        bokehThreshholdLuminance = serObj.FindProperty("bokehThreshholdLuminance");
        bokeh = serObj.FindProperty("bokeh");
        bokehScale = serObj.FindProperty("bokehScale");
        bokehIntensity = serObj.FindProperty("bokehIntensity");
        bokehDownsample = serObj.FindProperty("bokehDownsample");
        bokehTexture = serObj.FindProperty("bokehTexture");
        bokehDestination = serObj.FindProperty("bokehDestination");
        bluriness = serObj.FindProperty("bluriness");
        maxBlurSpread = serObj.FindProperty("maxBlurSpread");
        foregroundBlurExtrude = serObj.FindProperty("foregroundBlurExtrude");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();
        GameObject go = (target as DepthOfField34).gameObject;
        if (!go)
        {
            return;
        }
        if (!go.GetComponent<Camera>())
        {
            return;
        }
        if (simpleTweakMode.boolValue)
        {
            GUILayout.Label((((((("Current: " + go.GetComponent<Camera>().name) + ", near ") + go.GetComponent<Camera>().nearClipPlane) + ", far: ") + go.GetComponent<Camera>().farClipPlane) + ", focal: ") + focalPoint.floatValue, EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        }
        else
        {
            GUILayout.Label((((((("Current: " + go.GetComponent<Camera>().name) + ", near ") + go.GetComponent<Camera>().nearClipPlane) + ", far: ") + go.GetComponent<Camera>().farClipPlane) + ", focal: ") + focalZDistance.floatValue, EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        }
        EditorGUILayout.PropertyField(resolution, new GUIContent("Resolution"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(quality, new GUIContent("Quality"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(simpleTweakMode, new GUIContent("Simple tweak"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(visualizeCoc, new GUIContent("Visualize focus"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(bokeh, new GUIContent("Enable bokeh"), new GUILayoutOption[] { });
        EditorGUILayout.Separator();
        GUILayout.Label("Focal Settings", EditorStyles.boldLabel, new GUILayoutOption[] { });
        if (simpleTweakMode.boolValue)
        {
            focalPoint.floatValue = EditorGUILayout.Slider("Focal distance", focalPoint.floatValue, go.GetComponent<Camera>().nearClipPlane, go.GetComponent<Camera>().farClipPlane, new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(objectFocus, new GUIContent("Transform"), new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(smoothness, new GUIContent("Smoothness"), new GUILayoutOption[] { });
            focalSize.floatValue = EditorGUILayout.Slider("Focal size", focalSize.floatValue, 0f, go.GetComponent<Camera>().farClipPlane - go.GetComponent<Camera>().nearClipPlane, new GUILayoutOption[] { });
        }
        else
        {
            focalZDistance.floatValue = EditorGUILayout.Slider("Distance", focalZDistance.floatValue, go.GetComponent<Camera>().nearClipPlane, go.GetComponent<Camera>().farClipPlane, new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(objectFocus, new GUIContent("Transform"), new GUILayoutOption[] { });
            focalSize.floatValue = EditorGUILayout.Slider("Size", focalSize.floatValue, 0f, go.GetComponent<Camera>().farClipPlane - go.GetComponent<Camera>().nearClipPlane, new GUILayoutOption[] { });
            focalStartCurve.floatValue = EditorGUILayout.Slider("Start curve", focalStartCurve.floatValue, 0.05f, 20f, new GUILayoutOption[] { });
            focalEndCurve.floatValue = EditorGUILayout.Slider("End curve", focalEndCurve.floatValue, 0.05f, 20f, new GUILayoutOption[] { });
        }
        EditorGUILayout.Separator();
        GUILayout.Label("Blur (Fore- and Background)", EditorStyles.boldLabel, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(bluriness, new GUIContent("Blurriness"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(maxBlurSpread, new GUIContent("Blur spread"), new GUILayoutOption[] { });
        if (quality.enumValueIndex > 0)
        {
            EditorGUILayout.PropertyField(foregroundBlurExtrude, new GUIContent("Foreground size"), new GUILayoutOption[] { });
        }
        EditorGUILayout.Separator();
        if (bokeh.boolValue)
        {
            EditorGUILayout.Separator();
            GUILayout.Label("Bokeh Settings", EditorStyles.boldLabel, new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(bokehDestination, new GUIContent("Destination"), new GUILayoutOption[] { });
            bokehIntensity.floatValue = EditorGUILayout.Slider("Intensity", bokehIntensity.floatValue, 0f, 1f, new GUILayoutOption[] { });
            bokehThreshholdLuminance.floatValue = EditorGUILayout.Slider("Min luminance", bokehThreshholdLuminance.floatValue, 0f, 0.99f, new GUILayoutOption[] { });
            bokehThreshholdContrast.floatValue = EditorGUILayout.Slider("Min contrast", bokehThreshholdContrast.floatValue, 0f, 0.25f, new GUILayoutOption[] { });
            bokehDownsample.intValue = EditorGUILayout.IntSlider("Downsample", bokehDownsample.intValue, 1, 3, new GUILayoutOption[] { });
            bokehScale.floatValue = EditorGUILayout.Slider("Size scale", bokehScale.floatValue, 0f, 20f, new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(bokehTexture, new GUIContent("Texture mask"), new GUILayoutOption[] { });
        }
        serObj.ApplyModifiedProperties();
    }

}