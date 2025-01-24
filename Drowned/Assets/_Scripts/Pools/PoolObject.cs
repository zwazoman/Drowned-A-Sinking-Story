using System;
using UnityEngine;

/// <summary>
/// Pool component
/// </summary>
public class PoolObject : MonoBehaviour
{
    public event Action OnPulledFromPool;

    public Pool OriginPool;

    public void PullFromPool()
    {
        OnPulledFromPool?.Invoke();
    }

    public void PushToPool()
    {
        if(OriginPool == null) Destroy(gameObject);
        OriginPool.ReturnToPool(gameObject);
    }
}
