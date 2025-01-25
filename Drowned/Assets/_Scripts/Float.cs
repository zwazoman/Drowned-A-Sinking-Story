using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    [SerializeField] float _floatForce;

    Rigidbody _rb;

    private void Awake()
    {
        TryGetComponent(out _rb);

        _rb.velocity = new Vector3(_rb.velocity.x,_floatForce,_rb.velocity.z);
    }
}
