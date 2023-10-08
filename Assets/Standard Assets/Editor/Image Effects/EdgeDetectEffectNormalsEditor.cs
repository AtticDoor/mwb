using UnityEditor;
using UnityEngine;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(EdgeDetectEffectNormals))]
[UnityEngine.ExecuteInEditMode]
public partial class EdgeDetectEffectNormalsEditor : Editor
{
    public SerializedObject serObj;
    public SerializedProperty mode;
    public SerializedProperty sensitivityDepth;
    public SerializedProperty sensitivityNormals;
    public SerializedProperty edgesOnly;
    public SerializedProperty edgesOnlyBgColor;
    public virtual void OnEnable()
    {
        serObj = new SerializedObject(target);
        mode = serObj.FindProperty("mode");
        sensitivityDepth = serObj.FindProperty("sensitivityDepth");
        sensitivityNormals = serObj.FindProperty("sensitivityNormals");
        edgesOnly = serObj.FindProperty("edgesOnly");
        edgesOnlyBgColor = serObj.FindProperty("edgesOnlyBgColor");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();
        EditorGUILayout.PropertyField(mode, new GUIContent("Mode"), new GUILayoutOption[] { });
        GUILayout.Label("Edge sensitivity", new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(sensitivityDepth, new GUIContent("Depth"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(sensitivityNormals, new GUIContent("Normals"), new GUILayoutOption[] { });
        EditorGUILayout.Separator();
        GUILayout.Label("Background options", new GUILayoutOption[] { });
        edgesOnly.floatValue = EditorGUILayout.Slider("Edges only", edgesOnly.floatValue, 0f, 1f, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(edgesOnlyBgColor, new GUIContent("Background"), new GUILayoutOption[] { });
        serObj.ApplyModifiedProperties();
    }

}