using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipFlop : MonoBehaviour
{
    [SerializeField] HingeJoint joint;

    [SerializeField] Rigidbody head;
    [SerializeField] Rigidbody tail;


    [SerializeField] float force = 10;
    [SerializeField] float flipForceModifier = 1;


    public static bool Fliping;
    public static bool Floping;

    bool flip = false;
    bool flop = false;

    public static float TimeSinceFlipFlop;

    public void Flip(InputAction.CallbackContext ctx)
    {
        if (!enabled)
        {
            flip = false; return;
        }

        if (ctx.performed)
        {
            flip = true;
            Debug.Log("flip");
        }
        else if (ctx.canceled)
        {
            flip = false;
            if (!flop) TimeSinceFlipFlop = Time.time;
        }
    }

    public void Flop(InputAction.CallbackContext ctx)
    {
        if (!enabled)
        {
            flop = false;return;
        }
        if(ctx.performed)
        {
            Debug.Log("flop");

            flop = true;            
        }
        else
        {
            flop= false;
            if(!flip) { TimeSinceFlipFlop = Time.time;}
        }
        
    }

    private void FixedUpdate()
    {
        if(flip)
        {
            JointSpring hingeSpring = joint.spring;
            hingeSpring.targetPosition = force * flipForceModifier;
            joint.spring = hingeSpring;
            Fliping = true;
            Floping = false;
            //head.constraints = RigidbodyConstraints.FreezeRotation;
            //tail.constraints = RigidbodyConstraints.None;
        }
        else if(flop) 
        {
            //tail.constraints = RigidbodyConstraints.FreezeRotation;
            //head.constraints = RigidbodyConstraints.None;
            Floping = true;
            Fliping = false;
            JointSpring hingeSpring = joint.spring;
            hingeSpring.targetPosition = force * flipForceModifier * -1f;
            joint.spring = hingeSpring;
        }
        else
        {
            Floping = false;
            Fliping = false;
            JointSpring hingeSpring = joint.spring;
            hingeSpring.targetPosition = 0;
            joint.spring = hingeSpring;

            //head.constraints = RigidbodyConstraints.None;
            //tail.constraints = RigidbodyConstraints.None;


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.contacts[0].thisCollider.gameObject.name);
        //if(tail && collision.contacts[0].thisCollider)
    }
}
