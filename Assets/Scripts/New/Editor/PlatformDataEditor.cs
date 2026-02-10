using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformData))]
public class PlatformDataEditor : Editor
{
    PlatformData data;

    private void OnEnable()
    {
        data = (PlatformData)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.LabelField("Platform GameObject",EditorStyles.boldLabel);
        data.Prefab = (GameObject)EditorGUILayout.ObjectField(
            new GUIContent("Prefab" , "Prefab of represented platform"),
            data.Prefab,
            typeof(GameObject),
            false
        );
        
        EditorGUILayout.LabelField("Platform Visuals",EditorStyles.boldLabel);
        data.PlatformSprite = (Sprite)EditorGUILayout.ObjectField(
            new GUIContent("Platform Sprite","Sprite that represents the platform in the game."),
            data.PlatformSprite,
            typeof(Sprite),
            false
        );
        
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Platform Properties", EditorStyles.boldLabel);
        data.IsBreakable = EditorGUILayout.Toggle(
            new GUIContent("Is Breakable", "Determines if the platform can break when interacted with."),
            data.IsBreakable
        );


        EditorGUILayout.Space();


        data.HasAbility = EditorGUILayout.Toggle(
            new GUIContent("Has Ability", "Enable this if the platform has a special ability."),
            data.HasAbility
        );

        if (!data.HasAbility)
        {
            data.PlatformAbility = AbilityEnum.None;
        }

        EditorGUI.BeginDisabledGroup(!data.HasAbility);
        data.PlatformAbility = (AbilityEnum)EditorGUILayout.EnumPopup(
            new GUIContent("Platform Ability", "Select the platform's ability. Only selectable if HasAbility is enabled."),
            data.PlatformAbility
        );

        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Physics", EditorStyles.boldLabel);
        data.Force = EditorGUILayout.Slider(
            new GUIContent("Force", "Force applied by the platform. Valid range: 1 to 5."),
            data.Force,
            1f,
            5f
        );
    }

}
