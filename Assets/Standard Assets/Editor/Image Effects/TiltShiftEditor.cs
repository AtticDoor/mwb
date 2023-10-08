using UnityEditor;
using UnityEngine;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(TiltShift))]
public class TiltShiftEditor : Editor
{
    public SerializedObject serObj;
    public SerializedProperty focalPoint;
    public SerializedProperty smoothness;
    public SerializedProperty visualizeCoc;
    public SerializedProperty renderTextureDivider;
    public SerializedProperty blurIterations;
    public SerializedProperty foregroundBlurIterations;
    public SerializedProperty maxBlurSpread;
    public SerializedProperty enableForegroundBlur;
    public virtual void OnEnable()
    {
        serObj = new SerializedObject(target);
        focalPoint = serObj.FindProperty("focalPoint");
        smoothness = serObj.FindProperty("smoothness");
        visualizeCoc = serObj.FindProperty("visualizeCoc");
        renderTextureDivider = serObj.FindProperty("renderTextureDivider");
        blurIterations = serObj.FindProperty("blurIterations");
        foregroundBlurIterations = serObj.FindProperty("foregroundBlurIterations");
        maxBlurSpread = serObj.FindProperty("maxBlurSpread");
        enableForegroundBlur = serObj.FindProperty("enableForegroundBlur");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();
        GameObject go = (target as TiltShift).gameObject;
        if (!go)
        {
            return;
        }
        if (!go.GetComponent<Camera>())
        {
            return;
        }
        GUILayout.Label((((((("Current: " + go.GetComponent<Camera>().name) + ", near ") + go.GetComponent<Camera>().nearClipPlane) + ", far: ") + go.GetComponent<Camera>().farClipPlane) + ", focal: ") + focalPoint.floatValue, EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        GUILayout.Label("Focal Settings", EditorStyles.boldLabel, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(visualizeCoc, new GUIContent("Visualize"), new GUILayoutOption[] { });
        focalPoint.floatValue = EditorGUILayout.Slider("Distance", focalPoint.floatValue, go.GetComponent<Camera>().nearClipPlane, go.GetComponent<Camera>().farClipPlane, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(smoothness, new GUIContent("Smoothness"), new GUILayoutOption[] { });
        EditorGUILayout.Separator();
        GUILayout.Label("Background Blur", EditorStyles.boldLabel, new GUILayoutOption[] { });
        renderTextureDivider.intValue = (int)EditorGUILayout.Slider("Downsample", renderTextureDivider.intValue, 1, 3, new GUILayoutOption[] { });
        blurIterations.intValue = (int)EditorGUILayout.Slider("Iterations", blurIterations.intValue, 1, 4, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(maxBlurSpread, new GUIContent("Max blur spread"), new GUILayoutOption[] { });
        EditorGUILayout.Separator();
        GUILayout.Label("Foreground Blur", EditorStyles.boldLabel, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(enableForegroundBlur, new GUIContent("Enable"), new GUILayoutOption[] { });
        if (enableForegroundBlur.boolValue)
        {
            foregroundBlurIterations.intValue = (int)EditorGUILayout.Slider("Iterations", foregroundBlurIterations.intValue, 1, 4, new GUILayoutOption[] { });
        }
        //GUILayout.Label ("Background options");
        //edgesOnly.floatValue = EditorGUILayout.Slider ("Edges only", edgesOnly.floatValue, 0.0, 1.0);
        //EditorGUILayout.PropertyField (edgesOnlyBgColor, new GUIContent ("Background"));    		
        serObj.ApplyModifiedProperties();
    }

}