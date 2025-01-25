using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FloatingFishController : MonoBehaviour
{
    [SerializeField] Transform _shootSocket;

    bool _isShooting = false;

    //Bullet parameters
    float _size = 1;
    float _speed = 1;
    float _damage = 1;


    private void Update()
    {
        if (_isShooting)
        {
            _size += 0.1f;
            _speed -= 20f;
            _damage += 0.1f;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isShooting = true;
        }
        if (context.canceled)
        {
            ResetBubble();
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 targetPos;
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));

        if(Physics.Raycast(ray, out hit))
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

        bubbleScript.Target = targetPos;
        bubbleScript.Size = _size;
        bubbleScript.Speed = _speed;
        bubbleScript.Damage = _damage;
    }

    void ResetBubble()
    {
        _isShooting = false;

        _size = 1;
        _speed = 1;
        _damage = 1;
    }

}
