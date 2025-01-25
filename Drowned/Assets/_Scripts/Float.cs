using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    Rigidbody _rb;

    private void Awake()
    {
        TryGetComponent(out _rb);

        
    }
}
