using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject tipPositionGameObject;
    [SerializeField] GameObject hintGameObject;
    [SerializeField] Transform rootLeg;

    [Header("Overrided by legsBrain :")]

    [Header("Trigger parameters")]
    [SerializeField] public float floorDistance = 1.0f;
    [SerializeField] public float maxThreshold = 0.5f;
    [SerializeField] public float threshold = 0.5f;
    [SerializeField] public float precision = 0.1f;

    [Header("Movement parameters")]
    [SerializeField] public float maxSpeed = 1.0f;

    // Internal leg
    private Vector3 targetPosition;
    private Vector3 tipPosition;

    private bool moving = false;
    private Vector3 startVector;
    private float startTime;
    private float endTime;

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
    void Update()
    {
        // Avoid exceptions
        if (tipPositionGameObject == null || target == null) return;

        // Check if the leg need to move
        var distance = (targetPosition - tipPositionGameObject.transform.position).magnitude;

        // TEMPORARY 
        targetPosition = target.transform.position;
        // END TEMPORARY

        if (distance < precision)
        {
            if (moving) tipPosition = tipPositionGameObject.transform.position;
            moving = false;
        }

        tipPositionGameObject.transform.position = tipPosition;

        if (!moving && distance < threshold || distance < precision) return;

        if (!moving)
        {
            startTime = Time.time;
            endTime = Time.time + distance / maxSpeed;
            startVector = tipPositionGameObject.transform.position;
            targetPosition = target.transform.position;
            moving = true;
        }

        var alpha = Mathf.InverseLerp(startTime, endTime, Time.time);

        tipPositionGameObject.transform.position = Vector3.Lerp(startVector, targetPosition, alpha) + Vector3.up * alpha * (1 - alpha);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (target != null) Gizmos.DrawSphere(target.transform.position, 0.1f);

        Gizmos.color = Color.cyan;
        if (target != null) Gizmos.DrawSphere(target.transform.position, threshold);
    }
}
