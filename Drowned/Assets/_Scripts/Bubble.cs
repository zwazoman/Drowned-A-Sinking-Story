using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour,IPoolable
{
    //Poolable Initiator
    PoolObject _poolObject;

    [HideInInspector] public Vector3 TargetPos;

    [HideInInspector] public float ScaleFactor;
    [HideInInspector] public float SpeedFactor;
    [HideInInspector] public float DamageFactor;

    [SerializeField] float _bulletSpeed;
    [SerializeField] float _initialDamages;

    [SerializeField] float _floatForce;

    Vector3 _initialScale;
    float _damages;

    Rigidbody _rb;

    private void Awake()
    {
        TryGetComponent(out _poolObject);
        TryGetComponent(out _rb);

        _initialScale = transform.localScale;

        _poolObject.OnPulledFromPool += OnPulledFromPool;
    }

    public void ReturnToPool()
    {
        _rb.velocity = Vector3.zero;


        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }

    public void OnPulledFromPool()
    {
        //demander a Nathan

        _rb.AddForce(Vector3.up * _floatForce);

        transform.localScale = _initialScale * ScaleFactor;
        _damages = _initialDamages * DamageFactor;

        _rb.AddForce((TargetPos - transform.position).normalized * _bulletSpeed * SpeedFactor);
    } 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health)) health.ApplyDamage(DamageFactor);
        print(_damages);
        ReturnToPool();
    }
}
