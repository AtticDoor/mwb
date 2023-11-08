using UnityEditor;
using UnityEngine;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(ColorCorrectionCurves))]
public class ColorCorrectionCurvesEditor : Editor
{
    public SerializedObject serObj;
    public SerializedProperty mode;
    public SerializedProperty redChannel;
    public SerializedProperty greenChannel;
    public SerializedProperty blueChannel;
    public SerializedProperty useDepthCorrection;
    public SerializedProperty depthRedChannel;
    public SerializedProperty depthGreenChannel;
    public SerializedProperty depthBlueChannel;
    public SerializedProperty zCurveChannel;
    public SerializedProperty selectiveCc;
    public SerializedProperty selectiveFromColor;
    public SerializedProperty selectiveToColor;
    private bool applyCurveChanges;
    public virtual void OnEnable()
    {
        serObj = new SerializedObject(target);
        mode = serObj.FindProperty("mode");
        redChannel = serObj.FindProperty("redChannel");
        greenChannel = serObj.FindProperty("greenChannel");
        blueChannel = serObj.FindProperty("blueChannel");
        useDepthCorrection = serObj.FindProperty("useDepthCorrection");
        zCurveChannel = serObj.FindProperty("zCurve");
        depthRedChannel = serObj.FindProperty("depthRedChannel");
        depthGreenChannel = serObj.FindProperty("depthGreenChannel");
        depthBlueChannel = serObj.FindProperty("depthBlueChannel");
        if (redChannel.animationCurveValue.length == 0)
        {
            redChannel.animationCurveValue = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0f, 1f, 1f), new Keyframe(1, 1f, 1f, 1f) });
        }
        if (greenChannel.animationCurveValue.length == 0)
        {
            greenChannel.animationCurveValue = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0f, 1f, 1f), new Keyframe(1, 1f, 1f, 1f) });
        }
        if (blueChannel.animationCurveValue.length == 0)
        {
            blueChannel.animationCurveValue = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0f, 1f, 1f), new Keyframe(1, 1f, 1f, 1f) });
        }
        if (depthRedChannel.animationCurveValue.length == 0)
        {
            depthRedChannel.animationCurveValue = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0f, 1f, 1f), new Keyframe(1, 1f, 1f, 1f) });
        }
        if (depthGreenChannel.animationCurveValue.length == 0)
        {
            depthGreenChannel.animationCurveValue = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0f, 1f, 1f), new Keyframe(1, 1f, 1f, 1f) });
        }
        if (depthBlueChannel.animationCurveValue.length == 0)
        {
            depthBlueChannel.animationCurveValue = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0f, 1f, 1f), new Keyframe(1, 1f, 1f, 1f) });
        }
        if (zCurveChannel.animationCurveValue.length == 0)
        {
            zCurveChannel.animationCurveValue = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0f, 1f, 1f), new Keyframe(1, 1f, 1f, 1f) });
        }
        serObj.ApplyModifiedProperties();
        selectiveCc = serObj.FindProperty("selectiveCc");
        selectiveFromColor = serObj.FindProperty("selectiveFromColor");
        selectiveToColor = serObj.FindProperty("selectiveToColor");
    }

    public virtual void CurveGui(string name, SerializedProperty animationCurve, Color color)
    {
        // @NOTE: EditorGUILayout.CurveField is buggy and flickers, using PropertyField for now
        //animationCurve.animationCurveValue = EditorGUILayout.CurveField (GUIContent (name), animationCurve.animationCurveValue, color, Rect (0.0,0.0,1.0,1.0));
        EditorGUILayout.PropertyField(animationCurve, new GUIContent(name), new GUILayoutOption[] { });
        if (GUI.changed)
        {
            applyCurveChanges = true;
        }
    }

    public virtual void BeginCurves()
    {
        applyCurveChanges = false;
    }

    public virtual void ApplyCurves()
    {
        if (applyCurveChanges)
        {
            serObj.ApplyModifiedProperties();
            (serObj.targetObject as ColorCorrectionCurves).gameObject.SendMessage("UpdateTextures");
        }
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();
        GUILayout.Label("Curves to tweak colors. Advanced separates fore- and background.", EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(mode, new GUIContent("Mode"), new GUILayoutOption[] { });
        GUILayout.Label("Curves", EditorStyles.boldLabel, new GUILayoutOption[] { });
        BeginCurves();
        CurveGui("Red", redChannel, Color.red);
        CurveGui("Blue", blueChannel, Color.blue);
        CurveGui("Green", greenChannel, Color.green);
        EditorGUILayout.Separator();
        if (mode.intValue > 0)
        {
            useDepthCorrection.boolValue = true;
        }
        else
        {
            useDepthCorrection.boolValue = false;
        }
        if (useDepthCorrection.boolValue)
        {
            CurveGui("Red (depth)", depthRedChannel, Color.red);
            CurveGui("Blue (depth)", depthBlueChannel, Color.blue);
            CurveGui("Green (depth)", depthGreenChannel, Color.green);
            EditorGUILayout.Separator();
            CurveGui("Blend Curve", zCurveChannel, Color.grey);
        }
        if (mode.intValue > 0)
        {
            EditorGUILayout.Separator();
            GUILayout.Label("Selective Color Correction", EditorStyles.boldLabel, new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(selectiveCc, new GUIContent("Enable"), new GUILayoutOption[] { });
            if (selectiveCc.boolValue)
            {
                EditorGUILayout.PropertyField(selectiveFromColor, new GUIContent("Key"), new GUILayoutOption[] { });
                EditorGUILayout.PropertyField(selectiveToColor, new GUIContent("Target"), new GUILayoutOption[] { });
            }
        }
        ApplyCurves();
        if (!applyCurveChanges)
        {
            serObj.ApplyModifiedProperties();
        }
    }

}