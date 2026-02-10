using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField] protected PlatformData _platformData;

    public PlatformData GetPlatformData => _platformData;

    protected virtual void PlayAudio()
    {
        
    }    
}
