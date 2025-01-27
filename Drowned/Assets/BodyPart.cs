using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float ForwardBonusForce = 1f;

    [SerializeField] bool ishead;

    private void Awake()
    {
        TryGetComponent(out rb);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(Time.time - FlipFlop.TimeSinceFlipFlop < .2f)
        {
            rb.AddForce(Vector3.ProjectOnPlane( transform.up,Vector3.up)*ForwardBonusForce,ForceMode.Impulse);
            //Debug.DrawRay(transform.position,Vector3.ProjectOnPlane(transform.up, Vector3.up) * ForwardBonusForce, Color.red,1f);
        }
    }
}
