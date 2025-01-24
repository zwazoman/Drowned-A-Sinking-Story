using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour,IPoolable
{
    //Poolable Initiator
    PoolObject _poolObject;

    [HideInInspector] public Vector3 Target;

    [HideInInspector] public float Size;
    [HideInInspector] public float Speed;
    [HideInInspector] public float Damage;

    [SerializeField] float _bulletSpeed;

    Rigidbody _rb;

    private void Awake()
    {
        TryGetComponent(out _poolObject);
        TryGetComponent(out _rb);
    }

    private void Start()
    {
        _poolObject.OnPulledFromPool += OnPulledFromPool;

        transform.LookAt(Target);

        _rb.AddForce(transform.forward.normalized * _bulletSpeed);
    }

    public void ReturnToPool()
    {
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }

    public void OnPulledFromPool()
    {
        print("connard");
    } 


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health)) health.ApplyDamage(Damage);
        ReturnToPool();
    }
}
