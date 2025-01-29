using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayonCollider : MonoBehaviour
{
    [SerializeField] float _damages = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.layer == 6)
        {
            FishController.Instance.GetComponent<Health>().ApplyDamage(_damages);
            FishController.Instance.rb1.AddForce(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * 170, ForceMode.Impulse);
        }
    }
}
