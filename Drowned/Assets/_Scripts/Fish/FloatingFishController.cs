  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FloatingFishController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] FishController _fishController;
    [SerializeField] Transform _shootSocket;

    [Header("parameters")]

    [SerializeField] float _maxChargeTime = 1f;

    [SerializeField] float _lookSensitivity = 100;

    bool _isShooting = false;

    //Bullet parameters
    float _size = 1;
    float _speed = 1;
    float _damage = 1;

    float _timer = 0f;

    Vector2 camVector;

    MeshRenderer MR;

    private void Awake()
    {
        TryGetComponent(out MR);
    }


    private void Update()
    {
        if (!enabled) return;
        _fishController.AlignToCamera();

        if (_isShooting)
        {
            _timer += Time.deltaTime;
            if (_timer < _maxChargeTime)
            {
                _size += 1f * Time.deltaTime;
                _speed -= 0.25f * Time.deltaTime;
                _damage += 1.2f * Time.deltaTime;
                //Color singe = new Color(1 + _timer * 5, 1, 1);
                //MR.material.color = singe;
            }
        }
        else
        {
            //MR.material.color = Color.white;
            _timer = 0f;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!enabled) return;
        if (context.performed)
        {
            _isShooting = true;
        }
        if (context.canceled)
        {
            Shoot();
            ResetBubble();
        }
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (!enabled) return;
        /*if (ctx.performed)
        {
            camVector = ctx.ReadValue<Vector2>() * -_lookSensitivity;
            camVector.x *= -1;
        }
        else if (ctx.canceled)
        {
            camVector = Vector2.zero;
        }*/
    }

    void Shoot()
    {
        if (!enabled) return;
        Vector3 targetPos;
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,Mathf.Infinity))
        {
            targetPos = hit.point;
        }
        else
        {
            print("bizarre");
            targetPos = Vector3.zero; 
        }

        GameObject bubble = PoolManager.Instance.AccessPool(Pools.Bubble).TakeFromPool(_shootSocket.position, Quaternion.identity);

        bubble.TryGetComponent<Bubble>(out Bubble bubbleScript);

        bubbleScript.TargetPos = targetPos;
        bubbleScript.ScaleFactor = _size;
        bubbleScript.SpeedFactor = _speed;
        bubbleScript.DamageFactor = _damage;
    }

    void ResetBubble()
    {
        _isShooting = false;

        _size = 1;
        _speed = 1;
        _damage = 1;
    }



}
