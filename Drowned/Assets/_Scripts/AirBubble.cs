using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubble : MonoBehaviour,IPoolable
{
    [SerializeField] float _airAmount;

    PoolObject _poolObject;

    private void Awake()
    {
        _poolObject.OnPulledFromPool += OnPulledFromPool;

        TryGetComponent( out _poolObject);
    }

    public void OnPulledFromPool()
    {
        throw new System.NotImplementedException();
    }

    public void ReturnToPool()
    {
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FloatingFishController controller)) controller.SetAir(_airAmount);
    }
}
