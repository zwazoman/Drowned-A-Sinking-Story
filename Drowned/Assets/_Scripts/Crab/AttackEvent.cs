using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    [SerializeField] Transform _shootSocket;
    [SerializeField] Transform _shootSocketlautre;

    [SerializeField] List<CrabClaws> _claws = new List<CrabClaws>();

    public void Yes()
    {
        foreach(CrabClaws claw in _claws)
        {
            claw.StartAttack();
        }
    }

    public void No()
    {
        foreach (CrabClaws claw in _claws)
        {
            claw.StopAttack();
        }
    }

    public void ShootBubbles()
    {
        Vector3 target = FishController.Instance.gameObject.transform.position;
        Vector3 clawToTarget = target - _shootSocket.position;
        GameObject bubble = PoolManager.Instance.AccessPool(Pools.CrabBubble).TakeFromPool(-_shootSocket.position, Quaternion.identity);
        bubble.transform.forward = clawToTarget;
    }

    public void ShootBigRayonLaserDeLaMortHIHI()
    {

    }
}
