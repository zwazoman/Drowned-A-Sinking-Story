using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] float _healAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Health health))
        {
            health.Heal(-_healAmount);
        }
    }
}
