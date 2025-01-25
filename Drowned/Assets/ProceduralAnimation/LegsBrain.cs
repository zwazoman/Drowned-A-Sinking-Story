using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsBrain : MonoBehaviour
{
    [SerializeField] GameObject centerOfMass;
    [SerializeField] GameObject rootGameObjectLegs;

    private LegController[] legsController;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (rootGameObjectLegs != null) legsController = rootGameObjectLegs?.GetComponentsInChildren<LegController>();
        else legsController = GetComponentsInChildren<LegController>();

        //if (rootGameObjectLegs != null) rootGameObjectLegs.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (centerOfMass != null) Gizmos.DrawSphere(centerOfMass.transform.position, 0.1f);
    }
}
