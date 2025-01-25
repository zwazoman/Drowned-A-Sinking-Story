using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishController : MonoBehaviour
{
    Vector2 movementInput;
    [SerializeField] Rigidbody rb1;
    [SerializeField] Rigidbody rb2;
    [SerializeField] float Sensitivity;
    [SerializeField] float gravity;
    public void Rotate(InputAction.CallbackContext ctx)
    {
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
}