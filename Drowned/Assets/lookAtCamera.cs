using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCamera : MonoBehaviour
{

    // Update is called once per frame
    void Awake()
    {

        transform.LookAt(Camera.main.transform.position);
    }
}
