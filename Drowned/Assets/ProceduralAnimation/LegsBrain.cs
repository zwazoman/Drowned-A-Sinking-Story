using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsBrain : MonoBehaviour
{
    [SerializeField] GameObject centerOfMass;
    [SerializeField] GameObject rootGameObjectLegs;

    [Header("Trigger parameters")]
    [SerializeField] float floorDistance = 1.0f;
    [SerializeField] float threshold = 0.5f;
    [SerializeField] float maxThreshold = 1.0f;
    [SerializeField] float precision = 0.1f;

    [Header("Movement parameters")]
    [SerializeField] float maxSpeed = 1.0f;

    [SerializeField] LayerMask raycastMask;

    private LegController[] legsController;
    private int currentBatchMoving = 0;

    private HashSet<int> hasMoved = new HashSet<int>();
    private bool batchSwitching = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (rootGameObjectLegs != null) legsController = rootGameObjectLegs?.GetComponentsInChildren<LegController>();
        else legsController = GetComponentsInChildren<LegController>();

        updateLegsController();
    }

    private void OnValidate()
    {
        updateLegsController();
    }

    void updateLegsController()
    {
        if (legsController == null) return;
        foreach (var legController in legsController)
        {
            legController.floorDistance = floorDistance;
            legController.threshold = threshold;
            legController.precision = precision;
            legController.maxSpeed = maxSpeed;
            legController.raycastMask = raycastMask;

            if (centerOfMass != null) legController.setHintPosition(centerOfMass.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Allow leg to move or not
        for (int i = 0; i < legsController.Length; i++)
        {
            if (legsController[i].moving) hasMoved.Add(i);
            else if (batchSwitching && !legsController[i].moving) hasMoved.Remove(i);
        }

        for (int i = 0; i < legsController.Length; i++)
        {
            legsController[i].canMove = i % 2 == currentBatchMoving;
        }

        if (hasMoved.Count == 0 && batchSwitching)
        {
            currentBatchMoving = (currentBatchMoving + 1) % 2;
            batchSwitching = false;
        }

        if (hasMoved.Count == 4) batchSwitching = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (centerOfMass != null) Gizmos.DrawSphere(centerOfMass.transform.position, 0.1f);
    }
}
