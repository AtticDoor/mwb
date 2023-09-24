using UnityEngine;
using UnityEditor;
using System.Collections;

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
        this.serObj = new SerializedObject(this.target);
        this.focalPoint = this.serObj.FindProperty("focalPoint");
        this.smoothness = this.serObj.FindProperty("smoothness");
        this.visualizeCoc = this.serObj.FindProperty("visualizeCoc");
        this.renderTextureDivider = this.serObj.FindProperty("renderTextureDivider");
        this.blurIterations = this.serObj.FindProperty("blurIterations");
        this.foregroundBlurIterations = this.serObj.FindProperty("foregroundBlurIterations");
        this.maxBlurSpread = this.serObj.FindProperty("maxBlurSpread");
        this.enableForegroundBlur = this.serObj.FindProperty("enableForegroundBlur");
    }

    public override void OnInspectorGUI()
    {
        this.serObj.Update();
        GameObject go = (this.target as TiltShift).gameObject;
        if (!go)
        {
            return;
        }
        if (!go.GetComponent<Camera>())
        {
            return;
        }
        GUILayout.Label((((((("Current: " + go.GetComponent<Camera>().name) + ", near ") + go.GetComponent<Camera>().nearClipPlane) + ", far: ") + go.GetComponent<Camera>().farClipPlane) + ", focal: ") + this.focalPoint.floatValue, EditorStyles.miniBoldLabel, new GUILayoutOption[] {});
        GUILayout.Label("Focal Settings", EditorStyles.boldLabel, new GUILayoutOption[] {});
        EditorGUILayout.PropertyField(this.visualizeCoc, new GUIContent("Visualize"), new GUILayoutOption[] {});
        this.focalPoint.floatValue = EditorGUILayout.Slider("Distance", this.focalPoint.floatValue, go.GetComponent<Camera>().nearClipPlane, go.GetComponent<Camera>().farClipPlane, new GUILayoutOption[] {});
        EditorGUILayout.PropertyField(this.smoothness, new GUIContent("Smoothness"), new GUILayoutOption[] {});
        EditorGUILayout.Separator();
        GUILayout.Label("Background Blur", EditorStyles.boldLabel, new GUILayoutOption[] {});
        this.renderTextureDivider.intValue = (int) EditorGUILayout.Slider("Downsample", this.renderTextureDivider.intValue, 1, 3, new GUILayoutOption[] {});
        this.blurIterations.intValue = (int) EditorGUILayout.Slider("Iterations", this.blurIterations.intValue, 1, 4, new GUILayoutOption[] {});
        EditorGUILayout.PropertyField(this.maxBlurSpread, new GUIContent("Max blur spread"), new GUILayoutOption[] {});
        EditorGUILayout.Separator();
        GUILayout.Label("Foreground Blur", EditorStyles.boldLabel, new GUILayoutOption[] {});
        EditorGUILayout.PropertyField(this.enableForegroundBlur, new GUIContent("Enable"), new GUILayoutOption[] {});
        if (this.enableForegroundBlur.boolValue)
        {
            this.foregroundBlurIterations.intValue = (int) EditorGUILayout.Slider("Iterations", this.foregroundBlurIterations.intValue, 1, 4, new GUILayoutOption[] {});
        }
        //GUILayout.Label ("Background options");
        //edgesOnly.floatValue = EditorGUILayout.Slider ("Edges only", edgesOnly.floatValue, 0.0, 1.0);
        //EditorGUILayout.PropertyField (edgesOnlyBgColor, new GUIContent ("Background"));    		
        this.serObj.ApplyModifiedProperties();
    }

}