using System;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField] protected PlatformData _platformData;
    [SerializeField , Range(1,5)] protected int _platformLifeTime;

    public PlatformData GetPlatformData => _platformData;

    protected virtual void PlayAudio()
    {
        
    }    
}
