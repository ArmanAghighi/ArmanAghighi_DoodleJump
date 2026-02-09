using System;
using UnityEngine;

public class NormalPlatformer : MonoBehaviour , IPlatformer
{
    public Action<float> OnPlatformJump;
    public bool IsBreakable => false;

    [SerializeField] private PlatformData _platformData;

    public void PushUp()
    {
        OnPlatformJump?.Invoke(_platformData.Force);
    }
}
