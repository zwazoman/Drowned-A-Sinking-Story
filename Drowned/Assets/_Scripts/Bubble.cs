using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Bubble : MonoBehaviour,IPoolable
{
    //Poolable Initiator
    PoolObject _poolObject;

    [HideInInspector] public float ScaleFactor;
    [HideInInspector] public float SpeedFactor;
    [HideInInspector] public float DamageFactor;

    [SerializeField] float _bulletSpeed;
    [SerializeField] float _initialDamages;

    [SerializeField] float _floatForce;

    Vector3 _initialScale;
    float _damages;

    Rigidbody _rb;

    VisualEffect _effect;

    private void Awake()
    {
        TryGetComponent(out _poolObject);
        TryGetComponent(out _rb);

        _initialScale = transform.localScale;

        _poolObject.OnPulledFromPool += OnPulledFromPool;

        _effect = GetComponentInChildren<VisualEffect>();
        _effect.transform.parent = null;
    }

    void Update()
    {
        _effect.transform.position = transform.position;
    }

    public void ReturnToPool()
    {
        _rb.velocity = Vector3.zero;

        _effect.Stop();
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }

    public void OnPulledFromPool()
    {
        _effect.Play();

        transform.localScale = _initialScale * ScaleFactor;
        _damages = _initialDamages * DamageFactor;

        _rb.AddForce(transform.forward * _bulletSpeed * SpeedFactor,ForceMode.Impulse);
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health health)) health.ApplyDamage(DamageFactor);

        ReturnToPool();
    }
}
