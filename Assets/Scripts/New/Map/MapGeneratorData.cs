using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "MapGeneratorData", menuName = "Scriptable Objects/MapGeneratorData")]
public class MapGeneratorData : ScriptableObject
{
    public DifficultyLevel Difficulty;
    public List<MapSegments> Segments;

    [HideInInspector] public int maxChance = 100;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Segments == null) return;

        foreach (var segment in Segments)
        {
            NormalizeWeights(segment);
        }

        EditorUtility.SetDirty(this);
    }

    private void NormalizeWeights(MapSegments segment)
    {
        if (segment.PlatformData == null || segment.PlatformData.Count == 0)
            return;

        int total = 0;
        foreach (var data in segment.PlatformData)
            total += data.PlatformPercentage;

        if (total == 0) return;

        float factor = (float)maxChance / total;
        int newTotal = 0;

        for (int i = 0; i < segment.PlatformData.Count; i++)
        {
            var d = segment.PlatformData[i];
            d.PlatformPercentage = Mathf.RoundToInt(d.PlatformPercentage * factor);
            newTotal += d.PlatformPercentage;
        }

        int diff = maxChance - newTotal;
        int index = 0;

        while (diff != 0)
        {
            var d = segment.PlatformData[index];

            if (diff > 0)
            {
                d.PlatformPercentage++;
                diff--;
            }
            else if (diff < 0 && d.PlatformPercentage > 0)
            {
                d.PlatformPercentage--;
                diff++;
            }

            index = (index + 1) % segment.PlatformData.Count;
        }
    }
#endif
}


[System.Serializable]
public class MapSegments
{
    public int StartPosition = -4;
    public List<SegmentData> PlatformData;
    public int EndPosition;
}
[System.Serializable]
public class SegmentData
{
    public PlatformData PlatformData;

    [Range(0, 100)]
    public int PlatformPercentage;
}