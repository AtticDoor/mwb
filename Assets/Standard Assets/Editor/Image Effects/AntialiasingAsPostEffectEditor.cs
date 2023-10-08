using UnityEditor;
using UnityEngine;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(AntialiasingAsPostEffect))]
public class AntialiasingAsPostEffectEditor : Editor
{
    public SerializedObject serObj;
    public SerializedProperty mode;
    public SerializedProperty showGeneratedNormals;
    public SerializedProperty offsetScale;
    public SerializedProperty blurRadius;
    public SerializedProperty dlaaSharp;
    public SerializedProperty edgeThresholdMin;
    public SerializedProperty edgeThreshold;
    public SerializedProperty edgeSharpness;
    public virtual void OnEnable()
    {
        serObj = new SerializedObject(target);
        mode = serObj.FindProperty("mode");
        showGeneratedNormals = serObj.FindProperty("showGeneratedNormals");
        offsetScale = serObj.FindProperty("offsetScale");
        blurRadius = serObj.FindProperty("blurRadius");
        dlaaSharp = serObj.FindProperty("dlaaSharp");
        edgeThresholdMin = serObj.FindProperty("edgeThresholdMin");
        edgeThreshold = serObj.FindProperty("edgeThreshold");
        edgeSharpness = serObj.FindProperty("edgeSharpness");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();
        GUILayout.Label("Various luminance based fullscreen Antialiasing techniques", EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(mode, new GUIContent("AA Technique"), new GUILayoutOption[] { });
        Material mat = (target as AntialiasingAsPostEffect).CurrentAAMaterial();
        if (null == mat)
        {
            EditorGUILayout.HelpBox("This AA technique is currently not supported. Choose a different technique or disable the effect and use MSAA instead.", MessageType.Warning);
        }
        if (((AAMode)mode.enumValueIndex) == AAMode.NFAA)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(offsetScale, new GUIContent("Edge Detect Ofs"), new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(blurRadius, new GUIContent("Blur Radius"), new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(showGeneratedNormals, new GUIContent("Show Normals"), new GUILayoutOption[] { });
        }
        else
        {
            if (((AAMode)mode.enumValueIndex) == AAMode.DLAA)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(dlaaSharp, new GUIContent("Sharp"), new GUILayoutOption[] { });
            }
            else
            {
                if (((AAMode)mode.enumValueIndex) == AAMode.FXAA3Console)
                {
                    EditorGUILayout.Separator();
                    EditorGUILayout.PropertyField(edgeThresholdMin, new GUIContent("Edge Min Threshhold"), new GUILayoutOption[] { });
                    EditorGUILayout.PropertyField(edgeThreshold, new GUIContent("Edge Threshhold"), new GUILayoutOption[] { });
                    EditorGUILayout.PropertyField(edgeSharpness, new GUIContent("Edge Sharpness"), new GUILayoutOption[] { });
                }
            }
        }
        serObj.ApplyModifiedProperties();
    }

}