using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Bubble : MonoBehaviour,IPoolable
{
    //Poolable Initiator
    PoolObject _poolObject;

    public GameObject bubbleExplosionVFX;
    [HideInInspector] public float ScaleFactor;
    [HideInInspector] public float SpeedFactor;
    [HideInInspector] public float DamageFactor;

    [SerializeField] float _bulletSpeed;
    [SerializeField] float _initialDamages;

    [SerializeField] Vector3 _initialScale;
    float _damages;

    Rigidbody _rb;

    VisualEffect _effect;

    private void Awake()
    {
        ScaleFactor = 1;
        SpeedFactor = 1;
        DamageFactor = 1;

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

        Destroy(GameObject.Instantiate(bubbleExplosionVFX,transform.position,Quaternion.identity,null),5);
        

        _rb.velocity = Vector3.zero;

        _effect.Stop();

        StopCoroutine(DestroyCoroutine());
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }

    public void OnPulledFromPool()
    {
        StartCoroutine(DestroyCoroutine());

        //print("fonce");
        _effect.Play();

        transform.localScale = _initialScale * ScaleFactor;
        //print(ScaleFactor);
        //print(_initialScale);
        //print(_initialScale * ScaleFactor);Z
        _damages = _initialDamages * DamageFactor;

        _rb.AddForce(transform.forward * _bulletSpeed * SpeedFactor,ForceMode.Impulse);
    } 

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(10);
        ReturnToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name);

        if (other.gameObject.TryGetComponent<Health>(out Health health)) 
        {
            health.ApplyDamage(_damages); 
        }
        else if (other.gameObject.layer == 6)
        {
            //print("fish hit");
            FishController.Instance.GetComponent<Health>().ApplyDamage(_damages);
            FishController.Instance.rb1.AddForce(Vector3.ProjectOnPlane( transform.forward,Vector3.up).normalized*140,ForceMode.Impulse);
        }

        ReturnToPool();
    }
}
