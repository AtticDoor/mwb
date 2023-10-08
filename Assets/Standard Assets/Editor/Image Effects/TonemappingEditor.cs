using UnityEditor;
using UnityEngine;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(Tonemapping))]
public class TonemappingEditor : Editor
{
    public SerializedObject serObj;
    public SerializedProperty type;
    // CURVE specific parameter
    public SerializedProperty remapCurve;
    public SerializedProperty exposureAdjustment;
    // REINHARD specific parameter
    public SerializedProperty middleGrey;
    public SerializedProperty white;
    public SerializedProperty adaptionSpeed;
    public SerializedProperty adaptiveTextureSize;
    public virtual void OnEnable()
    {
        serObj = new SerializedObject(target);
        type = serObj.FindProperty("type");
        remapCurve = serObj.FindProperty("remapCurve");
        exposureAdjustment = serObj.FindProperty("exposureAdjustment");
        middleGrey = serObj.FindProperty("middleGrey");
        white = serObj.FindProperty("white");
        adaptionSpeed = serObj.FindProperty("adaptionSpeed");
        adaptiveTextureSize = serObj.FindProperty("adaptiveTextureSize");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();
        GUILayout.Label("Mapping HDR to LDR ranges since 1982", EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        Camera cam = (target as Tonemapping).GetComponent<Camera>();
        if (cam != null)
        {
            if (!cam.allowHDR)
            {
                EditorGUILayout.HelpBox("The camera is not HDR enabled. This will likely break the Tonemapper.", MessageType.Warning);
            }
            else
            {
                if (!(target as Tonemapping).validRenderTextureFormat)
                {
                    EditorGUILayout.HelpBox("The input to Tonemapper is not in HDR. Make sure that all effects prior to this are executed in HDR.", MessageType.Warning);
                }
            }
        }
        EditorGUILayout.PropertyField(type, new GUIContent("Technique"), new GUILayoutOption[] { });
        if (((Tonemapping.TonemapperType)type.enumValueIndex) == Tonemapping.TonemapperType.UserCurve)
        {
            EditorGUILayout.PropertyField(remapCurve, new GUIContent("Remap curve", "Specify the mapping of luminances yourself"), new GUILayoutOption[] { });
        }
        else
        {
            if (((Tonemapping.TonemapperType)type.enumValueIndex) == Tonemapping.TonemapperType.SimpleReinhard)
            {
                EditorGUILayout.PropertyField(exposureAdjustment, new GUIContent("Exposure", "Exposure adjustment"), new GUILayoutOption[] { });
            }
            else
            {
                if (((Tonemapping.TonemapperType)type.enumValueIndex) == Tonemapping.TonemapperType.Hable)
                {
                    EditorGUILayout.PropertyField(exposureAdjustment, new GUIContent("Exposure", "Exposure adjustment"), new GUILayoutOption[] { });
                }
                else
                {
                    if (((Tonemapping.TonemapperType)type.enumValueIndex) == Tonemapping.TonemapperType.Photographic)
                    {
                        EditorGUILayout.PropertyField(exposureAdjustment, new GUIContent("Exposure", "Exposure adjustment"), new GUILayoutOption[] { });
                    }
                    else
                    {
                        if (((Tonemapping.TonemapperType)type.enumValueIndex) == Tonemapping.TonemapperType.OptimizedHejiDawson)
                        {
                            EditorGUILayout.PropertyField(exposureAdjustment, new GUIContent("Exposure", "Exposure adjustment"), new GUILayoutOption[] { });
                        }
                        else
                        {
                            if (((Tonemapping.TonemapperType)type.enumValueIndex) == Tonemapping.TonemapperType.AdaptiveReinhard)
                            {
                                EditorGUILayout.PropertyField(middleGrey, new GUIContent("Middle grey", "Middle grey defines the average luminance thus brightening or darkening the entire image."), new GUILayoutOption[] { });
                                EditorGUILayout.PropertyField(white, new GUIContent("White", "Smallest luminance value that will be mapped to white"), new GUILayoutOption[] { });
                                EditorGUILayout.PropertyField(adaptionSpeed, new GUIContent("Adaption Speed", "Speed modifier for the automatic adaption"), new GUILayoutOption[] { });
                                EditorGUILayout.PropertyField(adaptiveTextureSize, new GUIContent("Texture size", "Defines the amount of downsamples needed."), new GUILayoutOption[] { });
                            }
                            else
                            {
                                if (((Tonemapping.TonemapperType)type.enumValueIndex) == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
                                {
                                    EditorGUILayout.PropertyField(middleGrey, new GUIContent("Middle grey", "Middle grey defines the average luminance thus brightening or darkening the entire image."), new GUILayoutOption[] { });
                                    EditorGUILayout.PropertyField(adaptionSpeed, new GUIContent("Adaption Speed", "Speed modifier for the automatic adaption"), new GUILayoutOption[] { });
                                    EditorGUILayout.PropertyField(adaptiveTextureSize, new GUIContent("Texture size", "Defines the amount of downsamples needed."), new GUILayoutOption[] { });
                                }
                            }
                        }
                    }
                }
            }
        }
        GUILayout.Label("All following effects will use LDR color buffers", EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        serObj.ApplyModifiedProperties();
    }

}