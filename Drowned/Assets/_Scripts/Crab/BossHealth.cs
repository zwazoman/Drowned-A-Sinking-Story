using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    int _crabHealth;

    [SerializeField] List<Health> _weakSpotsList = new List<Health>();

    private void Awake()
    {
        _crabHealth = _weakSpotsList.Count;

        foreach (Health weakSpot in _weakSpotsList)
        {
            weakSpot.OnDie += TakeDamage;
        }
    }

    void TakeDamage()
    {
        _crabHealth -= 1;
        if(_crabHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        print("singe");
    }
}
