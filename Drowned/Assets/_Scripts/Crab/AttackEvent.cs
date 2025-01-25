using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
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
}
