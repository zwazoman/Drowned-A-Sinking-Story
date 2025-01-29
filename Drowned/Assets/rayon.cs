using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayon : MonoBehaviour
{
    Vector3 TargetPose;
    Vector3 vel;
    [SerializeField] float _smoothTime = 0.2f;
    [SerializeField] GameObject _impact ;

    private void OnEnable()
    {
        vel = Vector3.zero;
        TargetPose = FishController.Instance.rb1.transform.position + Random.insideUnitSphere.normalized * 20 + Vector3.up*40;
    }
    private void Update()
    {
        TargetPose = Vector3.SmoothDamp(TargetPose, FishController.Instance.rb1.position, ref vel, _smoothTime);
        transform.LookAt(TargetPose);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo)) 
        {
            _impact.transform.position = hitInfo.point + hitInfo.normal * 5;
            _impact.transform.up = hitInfo.normal;
        }
    }


}
