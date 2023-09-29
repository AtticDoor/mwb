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
        this.serObj = new SerializedObject(this.target);
        this.mode = this.serObj.FindProperty("mode");
        this.sensitivityDepth = this.serObj.FindProperty("sensitivityDepth");
        this.sensitivityNormals = this.serObj.FindProperty("sensitivityNormals");
        this.edgesOnly = this.serObj.FindProperty("edgesOnly");
        this.edgesOnlyBgColor = this.serObj.FindProperty("edgesOnlyBgColor");
    }

    public override void OnInspectorGUI()
    {
        this.serObj.Update();
        EditorGUILayout.PropertyField(this.mode, new GUIContent("Mode"), new GUILayoutOption[] { });
        GUILayout.Label("Edge sensitivity", new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(this.sensitivityDepth, new GUIContent("Depth"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(this.sensitivityNormals, new GUIContent("Normals"), new GUILayoutOption[] { });
        EditorGUILayout.Separator();
        GUILayout.Label("Background options", new GUILayoutOption[] { });
        this.edgesOnly.floatValue = EditorGUILayout.Slider("Edges only", this.edgesOnly.floatValue, 0f, 1f, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(this.edgesOnlyBgColor, new GUIContent("Background"), new GUILayoutOption[] { });
        this.serObj.ApplyModifiedProperties();
    }

}