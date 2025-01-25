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
    [SerializeField] float _maxAir;

    [SerializeField] float _floatY = 10;
    [SerializeField] float _MaxFloatAcceleration = 10;
    [SerializeField] float _buoyancy = 10;

    bool _isShooting = false;

    [HideInInspector] public float Air;

    //Bullet parameters
    float _size = 1;
    float _speed = 1;
    float _damage = 1;

    float _timer = 0f;

    bool _shouldShoot = true;

    Vector2 camVector;

    MeshRenderer MR;

    private void Awake()
    {
        TryGetComponent(out MR);

        Air = _maxAir;
    }

    private void FixedUpdate()
    {
        _fishController.FloatTo(_floatY,_MaxFloatAcceleration, _buoyancy);
    }

    private void Update()
    {
        if (!enabled) return;
        _fishController.AlignToCamera();

        if (_isShooting)
        {
            _timer += Time.deltaTime;
            if (_timer < _maxChargeTime && Air != 0)
            {
                _size += 1f * Time.deltaTime;
                _speed -= 0.25f * Time.deltaTime;
                _damage += 1.2f * Time.deltaTime;
                SetAir(-1f * Time.deltaTime);
                
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
            print(Air);
            if (Air == 0)
            {
                _shouldShoot = false;
                return;
            }
            _shouldShoot = true;
            _isShooting = true;
        }
        if (context.canceled)
        {
            if (!_shouldShoot) return;
            Shoot();
            ResetBubble();
        }
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

    public void SetAir(float amount)
    {
        Air += amount;
        Air = Mathf.Clamp(Air, 0, _maxAir);
    }

    void ResetBubble()
    {
        _isShooting = false;

        _size = 1;
        _speed = 1;
        _damage = 1;
    }
}
