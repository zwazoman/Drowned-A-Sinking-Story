using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CrabClaws : MonoBehaviour
{
    [SerializeField] float _pushForce = 1;
    [SerializeField] float _damages = 1;

    Collider _coll;

    private void Awake()
    {
        TryGetComponent(out _coll);
    }

    public void StartAttack()
    {
        _coll.enabled = true;
    }

    public void StopAttack()
    {
        _coll.enabled = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.root.TryGetComponent(out Health health))
        {
            print(health.gameObject);
            Rigidbody rb = health.GetComponentInChildren<Rigidbody>();

            rb.AddForce((rb.transform.position- transform.root.GetChild(0).position).normalized * _pushForce, ForceMode.Impulse);
            health.ApplyDamage(_damages);
            StopAttack();
        }
    }
}
