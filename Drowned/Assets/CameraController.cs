using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Transform _target;

    [Header("parameters")]
    [SerializeField] float _smoothTime;
    [SerializeField] float _sensibility;
    Vector3 vel;

    Vector2 cameraInput;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref vel, _smoothTime);

        transform.Rotate(Vector3.up, cameraInput.x * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, cameraInput.y * Time.deltaTime, Space.Self);
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            cameraInput = ctx.ReadValue<Vector2>() * - _sensibility;
            cameraInput.x *= -1;
        }else if ( ctx.canceled)
        {
            cameraInput = Vector2.zero;
        }
    }


}
