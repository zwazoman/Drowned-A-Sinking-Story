using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _fishHead;

    private void Awake()
    {
        if (_fishHead == null) _fishHead = GameObject.Find("head");
    }

    private void Update()
    {
        transform.LookAt(_fishHead.transform);
    }
}
