using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsBrain : MonoBehaviour
{
    [SerializeField] GameObject centerOfMass;
    [SerializeField] GameObject rootGameObjectLegs;

    [Header("Trigger parameters")]
    [SerializeField] float floorDistance = 1.0f;
    [SerializeField] float minThreshold = 0.5f;
    [SerializeField] float precision = 0.1f;

    [Header("Movement parameters")]
    [SerializeField] float maxSpeed = 1.0f;

    [SerializeField] LayerMask raycastMask;

    private LegController[] legsController;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (rootGameObjectLegs != null) legsController = rootGameObjectLegs?.GetComponentsInChildren<LegController>();
        else legsController = GetComponentsInChildren<LegController>();

        foreach (var legController in legsController)
        {
            legController.floorDistance = floorDistance;
            legController.threshold = minThreshold;
            legController.precision = precision;
            legController.maxSpeed = maxSpeed;
            legController.raycastMask = raycastMask;

            if (centerOfMass != null) legController.setHintPosition(centerOfMass.transform.position);
        }

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
