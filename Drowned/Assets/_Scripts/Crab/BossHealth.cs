using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    int _crabHealth;

    [SerializeField] Slider _healthSlider;

    [SerializeField] List<Health> _weakSpotsList = new List<Health>();

    private void Awake()
    {
        _crabHealth = _weakSpotsList.Count;

        foreach (Health weakSpot in _weakSpotsList)
        {
            weakSpot.OnDie += TakeDamage;
        }
    }

    private void Start()
    {
        _healthSlider.maxValue = _weakSpotsList.Count;
        _healthSlider.value = _crabHealth;
    }

    void TakeDamage()
    {
        _crabHealth -= 1;
        _healthSlider.value = _crabHealth;
        if (_crabHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        print("singe");
    }
}
