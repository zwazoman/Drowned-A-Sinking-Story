using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubble : MonoBehaviour
{
    [SerializeField] float _airAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out FloatingFishController controller)) controller.SetAir(_airAmount);
    }
}
