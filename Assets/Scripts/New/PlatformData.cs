using UnityEngine;

[CreateAssetMenu(fileName = "PlatformData", menuName = "Scriptable Objects/PlatformData")]
public class PlatformData : ScriptableObject
{
    public Sprite PlatformSprite;
    public bool IsBreakable;

    [Tooltip("Enable this if the platform has a special ability.")]
    public bool HasAbility;

    [Tooltip("Select the platform's ability. Only selectable if HasAbility is enabled.")]
    public Ability PlatformAbility;
    
    [Range(1f, 5f)]
    public float Force = 1f;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!HasAbility)
            PlatformAbility = Ability.None;
    }
#endif
}
