using UnityEditor;
using UnityEngine;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(SunShafts))]
public class SunShaftsEditor : Editor
{
    public SerializedObject serObj;
    public SerializedProperty sunTransform;
    public SerializedProperty radialBlurIterations;
    public SerializedProperty sunColor;
    public SerializedProperty sunShaftBlurRadius;
    public SerializedProperty sunShaftIntensity;
    public SerializedProperty useSkyBoxAlpha;
    public SerializedProperty useDepthTexture;
    public SerializedProperty resolution;
    public SerializedProperty screenBlendMode;
    public SerializedProperty maxRadius;
    public virtual void OnEnable()
    {
        serObj = new SerializedObject(target);
        screenBlendMode = serObj.FindProperty("screenBlendMode");
        sunTransform = serObj.FindProperty("sunTransform");
        sunColor = serObj.FindProperty("sunColor");
        sunShaftBlurRadius = serObj.FindProperty("sunShaftBlurRadius");
        radialBlurIterations = serObj.FindProperty("radialBlurIterations");
        sunShaftIntensity = serObj.FindProperty("sunShaftIntensity");
        useSkyBoxAlpha = serObj.FindProperty("useSkyBoxAlpha");
        resolution = serObj.FindProperty("resolution");
        maxRadius = serObj.FindProperty("maxRadius");
        useDepthTexture = serObj.FindProperty("useDepthTexture");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();
        EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { });
        bool oldVal = useDepthTexture.boolValue;
        EditorGUILayout.PropertyField(useDepthTexture, new GUIContent("Rely on Z Buffer?"), new GUILayoutOption[] { });
        if ((target as SunShafts).GetComponent<Camera>())
        {
            GUILayout.Label("Current camera mode: " + (target as SunShafts).GetComponent<Camera>().depthTextureMode, EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        }
        EditorGUILayout.EndHorizontal();
        // depth buffer need
        /*
		var newVal : boolean = useDepthTexture.boolValue;
		if (newVal != oldVal) {
			if(newVal)
				(target as SunShafts).camera.depthTextureMode |= DepthTextureMode.Depth;
			else
				(target as SunShafts).camera.depthTextureMode &= ~DepthTextureMode.Depth;
		}
		*/
        EditorGUILayout.PropertyField(resolution, new GUIContent("Resolution"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(screenBlendMode, new GUIContent("Blend mode"), new GUILayoutOption[] { });
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(sunTransform, new GUIContent("Shafts caster", "Chose a transform that acts as a root point for the produced sun shafts"), new GUILayoutOption[] { });
        if ((target as SunShafts).sunTransform && (target as SunShafts).GetComponent<Camera>())
        {
            if (GUILayout.Button("Center on " + (target as SunShafts).GetComponent<Camera>().name, new GUILayoutOption[] { }))
            {
                if (EditorUtility.DisplayDialog("Move sun shafts source?", ((("The SunShafts caster named " + (target as SunShafts).sunTransform.name) + "\n will be centered along ") + (target as SunShafts).GetComponent<Camera>().name) + ". Are you sure? ", "Please do", "Don't"))
                {
                    Ray ray = (target as SunShafts).GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    (target as SunShafts).sunTransform.position = ray.origin + (ray.direction * 500f);
                    (target as SunShafts).sunTransform.LookAt((target as SunShafts).transform);
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(sunColor, new GUIContent("Shafts color"), new GUILayoutOption[] { });
        maxRadius.floatValue = 1f - EditorGUILayout.Slider("Distance falloff", 1f - maxRadius.floatValue, 0.1f, 1f, new GUILayoutOption[] { });
        EditorGUILayout.Separator();
        sunShaftBlurRadius.floatValue = EditorGUILayout.Slider("Blur size", sunShaftBlurRadius.floatValue, 1f, 10f, new GUILayoutOption[] { });
        radialBlurIterations.intValue = EditorGUILayout.IntSlider("Blur iterations", radialBlurIterations.intValue, 1, 3, new GUILayoutOption[] { });
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(sunShaftIntensity, new GUIContent("Intensity"), new GUILayoutOption[] { });
        useSkyBoxAlpha.floatValue = EditorGUILayout.Slider("Use alpha mask", useSkyBoxAlpha.floatValue, 0f, 1f, new GUILayoutOption[] { });
        serObj.ApplyModifiedProperties();
    }

}