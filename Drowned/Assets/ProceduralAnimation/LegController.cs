using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField] GameObject tipPositionGameObject;
    [SerializeField] GameObject hintGameObject;
    [SerializeField] Transform rootLeg;

    [SerializeField] public Vector3 raycastDirection;

    [Header("Overrided by legsBrain :")]

    [Header("Trigger parameters")]
    [SerializeField] public float floorDistance = 1.0f;
    [SerializeField] public float maxThreshold = 1.0f;
    [SerializeField] public float threshold = 0.5f;
    [SerializeField] public float precision = 0.1f;

    [Header("Movement parameters")]
    [SerializeField] public float maxSpeed = 1.0f;

    [SerializeField] public LayerMask raycastMask;

    // Internal leg
    private Vector3 targetPosition;
    private Vector3 tipPosition;

    public bool moving = false;
    private Vector3 startVector;
    private float startTime;
    private float endTime;

    public bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        if (tipPositionGameObject != null) targetPosition = tipPositionGameObject.transform.position;
    }

    public void setHintPosition(Vector3 centerOfMass)
    {

        Debug.DrawLine(rootLeg.position, centerOfMass, Color.blue, 100f);

        hintGameObject.transform.position = centerOfMass + (rootLeg.position - centerOfMass).normalized * 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Avoid exceptions
        if (tipPositionGameObject == null || targetPosition == null) return;

        // Check if the leg need to move
        var distance = (targetPosition - tipPositionGameObject.transform.position).magnitude;

        if (distance < precision)
        {
            if (moving) tipPosition = tipPositionGameObject.transform.position;
            moving = false;
        }

        tipPositionGameObject.transform.position = tipPosition;

        // Update the next targetPosition
        RaycastHit hit;
        if (Physics.Raycast(rootLeg.position, raycastDirection, out hit, 100, raycastMask))
        {
            targetPosition = hit.point;
            Debug.DrawRay(rootLeg.position, raycastDirection * hit.distance, Color.yellow);
        }

        if (!moving && distance < threshold || distance < precision) return;

        // if the leg can't move and deosn't move, avoid updating the leg
        if (canMove && !moving) return;

        if (!moving)
        {
            startTime = Time.time;
            endTime = Time.time + distance / maxSpeed;
            startVector = tipPositionGameObject.transform.position;
            moving = true;
        }

        var alpha = Mathf.InverseLerp(startTime, endTime, Time.time);

        tipPositionGameObject.transform.position = Vector3.Lerp(startVector, targetPosition, alpha) + Vector3.up * alpha * (1 - alpha);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (targetPosition != null) Gizmos.DrawWireSphere(targetPosition, threshold);

        Gizmos.color = Color.yellow;
        if (rootLeg != null || raycastDirection != null) Gizmos.DrawLine(rootLeg.position, rootLeg.position + raycastDirection);
    }
}
