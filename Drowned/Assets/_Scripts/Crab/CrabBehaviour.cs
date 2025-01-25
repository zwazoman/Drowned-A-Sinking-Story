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
        //Vector3 CrabToFish = _fishHead.transform.position - transform.position;
        //float engule = Mathf.Atan2(CrabToFish.z, -CrabToFish.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(-90, engule, transform.rotation.z);
        transform.LookAt(_fishHead.transform);
    }
}
