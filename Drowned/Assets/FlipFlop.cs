using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipFlop : MonoBehaviour
{
    [SerializeField] HingeJoint joint;

    [SerializeField] float force = 10;
    [SerializeField] float flipForceModifier = 1;

    bool flip = false;
    bool flop = false;

    public void Flip(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            flip = true;
            Debug.Log("flip");
        }
        else if (ctx.canceled)
        {
            flip = false;
        }
    }

    public void Flop(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            Debug.Log("flop");

            flop = true;            
        }
        else
        {
            flop= false;
        }
        
    }

    private void Update()
    {
        if(flip)
        {
            JointSpring hingeSpring = joint.spring;
            hingeSpring.targetPosition = force * flipForceModifier;
            joint.spring = hingeSpring;
        }
        else if(flop) 
        {
            JointSpring hingeSpring = joint.spring;
            hingeSpring.targetPosition = force * flipForceModifier * -1f;
            joint.spring = hingeSpring;
        }
        else
        {
            JointSpring hingeSpring = joint.spring;
            hingeSpring.targetPosition = 0;
            joint.spring = hingeSpring;
        }
    }
}
