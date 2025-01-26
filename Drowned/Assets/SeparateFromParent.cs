using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparateFromParent : MonoBehaviour
{
    Transform p;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        p = transform.parent;
        transform.parent = null;

        offset = transform.position - p.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(p != null) { Destroy(gameObject);return; }
        transform.position = p.position + offset;
    }
}
