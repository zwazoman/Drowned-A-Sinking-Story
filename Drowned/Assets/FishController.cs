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
        //rb1.MoveRotation()
        rb1.transform.Rotate(Vector3.up, movementInput.x * Time.deltaTime, Space.World);
        rb1.transform.Rotate(Camera.main.transform.right, movementInput.y * Time.deltaTime, Space.Self);

        rb2.transform.Rotate(Vector3.up, movementInput.x * Time.deltaTime, Space.World);
        rb2.transform.Rotate(Camera.main.transform.right, movementInput.y * Time.deltaTime, Space.Self);
    }
}
