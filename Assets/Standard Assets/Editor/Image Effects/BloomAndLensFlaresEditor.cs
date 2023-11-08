using UnityEditor;
using UnityEngine;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(BloomAndLensFlares))]
public class BloomAndLensFlaresEditor : Editor
{
    public SerializedProperty tweakMode;
    public SerializedProperty screenBlendMode;
    public SerializedObject serObj;
    public SerializedProperty hdr;
    public SerializedProperty sepBlurSpread;
    public SerializedProperty useSrcAlphaAsMask;
    public SerializedProperty bloomIntensity;
    public SerializedProperty bloomThreshhold;
    public SerializedProperty bloomBlurIterations;
    public SerializedProperty lensflares;
    public SerializedProperty hollywoodFlareBlurIterations;
    public SerializedProperty lensflareMode;
    public SerializedProperty hollyStretchWidth;
    public SerializedProperty lensflareIntensity;
    public SerializedProperty lensflareThreshhold;
    public SerializedProperty flareColorA;
    public SerializedProperty flareColorB;
    public SerializedProperty flareColorC;
    public SerializedProperty flareColorD;
    public SerializedProperty blurWidth;
    public SerializedProperty lensFlareVignetteMask;
    public virtual void OnEnable()
    {
        serObj = new SerializedObject(target);
        screenBlendMode = serObj.FindProperty("screenBlendMode");
        hdr = serObj.FindProperty("hdr");
        sepBlurSpread = serObj.FindProperty("sepBlurSpread");
        useSrcAlphaAsMask = serObj.FindProperty("useSrcAlphaAsMask");
        bloomIntensity = serObj.FindProperty("bloomIntensity");
        bloomThreshhold = serObj.FindProperty("bloomThreshhold");
        bloomBlurIterations = serObj.FindProperty("bloomBlurIterations");
        lensflares = serObj.FindProperty("lensflares");
        lensflareMode = serObj.FindProperty("lensflareMode");
        hollywoodFlareBlurIterations = serObj.FindProperty("hollywoodFlareBlurIterations");
        hollyStretchWidth = serObj.FindProperty("hollyStretchWidth");
        lensflareIntensity = serObj.FindProperty("lensflareIntensity");
        lensflareThreshhold = serObj.FindProperty("lensflareThreshhold");
        flareColorA = serObj.FindProperty("flareColorA");
        flareColorB = serObj.FindProperty("flareColorB");
        flareColorC = serObj.FindProperty("flareColorC");
        flareColorD = serObj.FindProperty("flareColorD");
        blurWidth = serObj.FindProperty("blurWidth");
        lensFlareVignetteMask = serObj.FindProperty("lensFlareVignetteMask");
        tweakMode = serObj.FindProperty("tweakMode");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();
        GUILayout.Label(("HDR " + (hdr.enumValueIndex == 0 ? "auto detected, " : (hdr.enumValueIndex == 1 ? "forced on, " : "disabled, "))) + (useSrcAlphaAsMask.floatValue < 0.1f ? " ignoring alpha channel glow information" : " using alpha channel glow information"), EditorStyles.miniBoldLabel, new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(tweakMode, new GUIContent("Tweak mode"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(screenBlendMode, new GUIContent("Blend mode"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(hdr, new GUIContent("HDR"), new GUILayoutOption[] { });
        // display info text when screen blend mode cannot be used
        Camera cam = (target as BloomAndLensFlares).GetComponent<Camera>();
        if (cam != null)
        {
            if ((screenBlendMode.enumValueIndex == 0) && ((cam.allowHDR && (hdr.enumValueIndex == 0)) || (hdr.enumValueIndex == 1)))
            {
                EditorGUILayout.HelpBox("Screen blend is not supported in HDR. Using 'Add' instead.", MessageType.Info);
            }
        }
        if (1 == tweakMode.intValue)
        {
            EditorGUILayout.PropertyField(lensflares, new GUIContent("Cast lens flares"), new GUILayoutOption[] { });
        }
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(bloomIntensity, new GUIContent("Intensity"), new GUILayoutOption[] { });
        bloomThreshhold.floatValue = EditorGUILayout.Slider("Threshhold", bloomThreshhold.floatValue, -0.05f, 4f, new GUILayoutOption[] { });
        bloomBlurIterations.intValue = EditorGUILayout.IntSlider("Blur iterations", bloomBlurIterations.intValue, 1, 4, new GUILayoutOption[] { });
        sepBlurSpread.floatValue = EditorGUILayout.Slider("Blur spread", sepBlurSpread.floatValue, 0.1f, 10f, new GUILayoutOption[] { });
        if (1 == tweakMode.intValue)
        {
            useSrcAlphaAsMask.floatValue = EditorGUILayout.Slider(new GUIContent("Use alpha mask", "Make alpha channel define glowiness"), useSrcAlphaAsMask.floatValue, 0f, 1f, new GUILayoutOption[] { });
        }
        else
        {
            useSrcAlphaAsMask.floatValue = 0f;
        }
        if (1 == tweakMode.intValue)
        {
            EditorGUILayout.Separator();
            if (lensflares.boolValue)
            {
                // further lens flare tweakings
                if (0 != tweakMode.intValue)
                {
                    EditorGUILayout.PropertyField(lensflareMode, new GUIContent("Lens flare mode"), new GUILayoutOption[] { });
                }
                else
                {
                    lensflareMode.enumValueIndex = 0;
                }
                EditorGUILayout.PropertyField(lensFlareVignetteMask, new GUIContent("Lens flare mask", "This mask is needed to prevent lens flare artifacts"), new GUILayoutOption[] { });
                EditorGUILayout.PropertyField(lensflareIntensity, new GUIContent("Local intensity"), new GUILayoutOption[] { });
                lensflareThreshhold.floatValue = EditorGUILayout.Slider("Local threshhold", lensflareThreshhold.floatValue, 0f, 1f, new GUILayoutOption[] { });
                if (lensflareMode.intValue == 0)
                {
                    // ghosting	
                    EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { });
                    EditorGUILayout.PropertyField(flareColorA, new GUIContent("1st Color"), new GUILayoutOption[] { });
                    EditorGUILayout.PropertyField(flareColorB, new GUIContent("2nd Color"), new GUILayoutOption[] { });
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { });
                    EditorGUILayout.PropertyField(flareColorC, new GUIContent("3rd Color"), new GUILayoutOption[] { });
                    EditorGUILayout.PropertyField(flareColorD, new GUIContent("4th Color"), new GUILayoutOption[] { });
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    if (lensflareMode.intValue == 1)
                    {
                        // hollywood
                        EditorGUILayout.PropertyField(hollyStretchWidth, new GUIContent("Stretch width"), new GUILayoutOption[] { });
                        hollywoodFlareBlurIterations.intValue = EditorGUILayout.IntSlider("Blur iterations", hollywoodFlareBlurIterations.intValue, 1, 4, new GUILayoutOption[] { });
                        EditorGUILayout.PropertyField(flareColorA, new GUIContent("Tint Color"), new GUILayoutOption[] { });
                    }
                    else
                    {
                        if (lensflareMode.intValue == 2)
                        {
                            // both
                            EditorGUILayout.PropertyField(hollyStretchWidth, new GUIContent("Stretch width"), new GUILayoutOption[] { });
                            hollywoodFlareBlurIterations.intValue = EditorGUILayout.IntSlider("Blur iterations", hollywoodFlareBlurIterations.intValue, 1, 4, new GUILayoutOption[] { });
                            EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { });
                            EditorGUILayout.PropertyField(flareColorA, new GUIContent("1st Color"), new GUILayoutOption[] { });
                            EditorGUILayout.PropertyField(flareColorB, new GUIContent("2nd Color"), new GUILayoutOption[] { });
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { });
                            EditorGUILayout.PropertyField(flareColorC, new GUIContent("3rd Color"), new GUILayoutOption[] { });
                            EditorGUILayout.PropertyField(flareColorD, new GUIContent("4th Color"), new GUILayoutOption[] { });
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }
            }
        }
        else
        {
            lensflares.boolValue = false; // disable lens flares in simple tweak mode
        }
        serObj.ApplyModifiedProperties();
    }

}