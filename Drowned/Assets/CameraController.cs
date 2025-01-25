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

    float rotX;
    float rotY;

    float salope;

    private void Awake()
    {
        salope = Camera.main.transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref vel, _smoothTime);

        rotY = Mathf.Clamp(rotY, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotY, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotX, 0);

        //transform.Rotate(Vector3.up, cameraInput.x * Time.deltaTime, Space.World);
        //transform.Rotate(Vector3.right, cameraInput.y * Time.deltaTime, Space.Self);

        HandleClipping();
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            //cameraInput = ctx.ReadValue<Vector2>() * - _sensibility;
            //cameraInput.x *= -1;
            rotX += ctx.ReadValue<Vector2>().x * _sensibility;
            rotY += -ctx.ReadValue<Vector2>().y * _sensibility;
        }else if ( ctx.canceled)
        {
            cameraInput = Vector2.zero;

        }
    }

    void HandleClipping()
    {
        RaycastHit hit;

        if(Physics.Raycast(_target.position, -transform.forward, out hit, (Camera.main.transform.position - -_target.position).magnitude,LayerMask.GetMask("Wall")))
        {
            Camera.main.transform.position = hit.point;
            print("suu");
        }
        else
        {
            Camera.main.transform.localPosition = Vector3.forward * salope;
        }

    }


}
