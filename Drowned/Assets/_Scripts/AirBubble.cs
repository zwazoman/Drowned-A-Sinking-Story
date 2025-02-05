using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubble : MonoBehaviour,IPoolable
{
    [HideInInspector] public Geyser Jeyser;

    [SerializeField] float _airAmount;
    [SerializeField] float _MagnetTreshold = 25;
    public GameObject bubbleExplosionVFX;

    PoolObject _poolObject;

    private void Awake()
    {
        TryGetComponent(out _poolObject);

        _poolObject.OnPulledFromPool += OnPulledFromPool;
    }

    public void OnPulledFromPool()
    {
        
    }

    public void ReturnToPool()
    {
        Jeyser.Spawn();

        Destroy(GameObject.Instantiate(bubbleExplosionVFX, transform.position, Quaternion.identity, null), 5);

        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            FishController.Instance.gameObject.TryGetComponent(out FloatingFishController floatingFish);
            floatingFish.SetAir(_airAmount);
            ReturnToPool();
        }
    }

    private void Update()
    {
        if((FishController.Instance.rb1.position-transform.position).sqrMagnitude< _MagnetTreshold* _MagnetTreshold)
        {
            Vector3 vel = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, FishController.Instance.rb1.position, ref vel, .05f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _MagnetTreshold);
    }
}
