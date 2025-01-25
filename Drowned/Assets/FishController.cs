using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class FishController : MonoBehaviour
{
    Vector2 movementInput;

    [Header("references")]
    [SerializeField] Rigidbody rb1;
    [SerializeField] Rigidbody rb2;
    [SerializeField] FloatingFishController _aimingControls;
    [SerializeField] FlipFlop _flipflop;
    [SerializeField] FishVisuals _visuals;
    
    [Header("Parameters")]
    [SerializeField] float Sensitivity;
    [SerializeField] float gravity;
    public void Rotate(InputAction.CallbackContext ctx)
    {
        if (!enabled) return;

        if (ctx.performed)
        {
            movementInput = ctx.ReadValue<Vector2>() * Sensitivity;
        }
        else if (ctx.canceled)
        {
            movementInput = Vector2.zero;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb1.AddTorque(Vector3.ProjectOnPlane(Vector3.up * movementInput.x , rb1.transform.up), ForceMode.Force);
        rb1.AddTorque(Vector3.Project( Camera.main.transform.right * movementInput.y,rb1.transform.forward) , ForceMode.Force);

        rb2.AddTorque(Vector3.ProjectOnPlane(Vector3.up * movementInput.x,rb2.transform.up), ForceMode.Force);
        rb2.AddTorque(Vector3.Project(Camera.main.transform.right * movementInput.y, rb2.transform.forward), ForceMode.Force);

        /*rb2.MoveRotation(Quaternion.Euler(Vector3.up * movementInput.x * Time.deltaTime) * rb2.rotation);
        rb2.MoveRotation(Quaternion.Euler(Camera.main.transform.right* movementInput.y * Time.deltaTime) * rb2.rotation);

        rb1.MoveRotation(Quaternion.Euler(Vector3.up * movementInput.x * Time.deltaTime) * rb1.rotation);
        rb1.MoveRotation(Quaternion.Euler(Camera.main.transform.right * movementInput.y * Time.deltaTime) * rb1.rotation);
        */
        rb1.AddForce(Vector3.down * gravity);
        rb2.AddForce(Vector3.down * gravity);
    }

    public void AlignToCamera()
    {
        //rb1.MoveRotation(Quaternion.LookRotation(Camera.main.transform.up,Vector3.up));
        //rb2.MoveRotation(Quaternion.LookRotation(-Camera.main.transform.up,Vector3.up));

        rb1.transform.rotation = Quaternion.LookRotation(Vector3.up, Camera.main.transform.forward);
        //rb1.transform.up = Camera.main.transform.forward;
        //rb2.transform.up = -Camera.main.transform.forward;
    }

    public void SwitchToAim(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _aimingControls.enabled = true;
            _flipflop.enabled = false;
            enabled = false;
            _visuals.targetFatness = 100;
        }
        else if (ctx.canceled)
        {
            _aimingControls.enabled = false;
            _flipflop.enabled = true;
            enabled = true;
            _visuals.targetFatness = 0;
        }
    }

    public void FloatTo ( float targetY,float acceleration,float buoyancy)
    {
        float force = Mathf.Max( targetY-rb1.transform.position.y,0);
        force = Mathf.Clamp(force , -acceleration, acceleration)* buoyancy;
        rb1.AddForce(force*Vector3.up);
        rb2.AddForce(force*Vector3.up);
    }

}
