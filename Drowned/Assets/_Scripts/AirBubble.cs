using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubble : MonoBehaviour,IPoolable
{
    [SerializeField] float _airAmount;
    [SerializeField] float _MagnetTreshold = 25;



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
        if (other.gameObject.TryGetComponent(out FloatingFishController controller))
        {
            controller.SetAir(_airAmount);
        }
    }

    private void Update()
    {
        if((FishController.Instance.rb1.position-transform.position).sqrMagnitude< _MagnetTreshold* _MagnetTreshold)
        {
            Vector3 vel = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, FishController.Instance.rb1.position, ref vel, .35f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _MagnetTreshold);
    }
}
