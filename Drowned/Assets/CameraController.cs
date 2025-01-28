using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _target;
    [SerializeField] Transform _cameraHolder;

    [Header("parameters")]
    [SerializeField] float _smoothTime;
    [SerializeField] float _sensibility;
    Vector3 vel;

    Vector2 cameraInput;

    float rotX;
    float rotY;

    
    float salope;
    Animator animator;



    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        salope = (Camera.main.transform.position - transform.position).magnitude;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddImpulse()
    {
        animator.SetTrigger("Play");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target.position + Vector3.up*5, ref vel, _smoothTime);

        rotX += cameraInput.x * _sensibility;
        rotY += -cameraInput.y * _sensibility;

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
            cameraInput = ctx.ReadValue<Vector2>() * - _sensibility;            
            //cameraInput.x *= -1;
            
        }else if ( ctx.canceled)
        {
            cameraInput = Vector2.zero;

        }
    }

    void HandleClipping()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.forward * 100,Color.red);
        if(Physics.Raycast(transform.position, -transform.forward, out hit,salope,LayerMask.GetMask("Wall")))
        {
            Debug.Log("putain");
            _cameraHolder.transform.position = hit.point - transform.forward*3;
        }
        else
        {
            _cameraHolder.transform.localPosition = Vector3.forward * - (salope);
        }

    }


}
