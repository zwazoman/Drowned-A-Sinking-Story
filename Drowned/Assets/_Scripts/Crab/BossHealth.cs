using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    int _crabHealth;

    [SerializeField] Slider _healthSlider;

    [SerializeField] List<Health> _weakSpotsList = new List<Health>();

    public static event Action OnBossDead;
    AudioSource _audioSource;

    private void Awake()
    {
        GameObject.Find("Voices").GetComponent<AudioSource>().volume = 1;

        _crabHealth = _weakSpotsList.Count;

        foreach (Health weakSpot in _weakSpotsList)
        {
            weakSpot.OnDie += TakeDamage;
        }

        if(OnBossDead!= null)
        {
            foreach (Delegate d in OnBossDead.GetInvocationList())
            {
                OnBossDead -= (Action)d;
            }
        }
        
    }

    private void Start()
    {
        _healthSlider.maxValue = _weakSpotsList.Count;
        _healthSlider.value = _crabHealth;
    }

    void TakeDamage()
    {
        Debug.LogWarning("boss damage taken");
        _crabHealth -= 1;
        _healthSlider.value = _crabHealth;
        GetComponentInChildren<CrabBehaviour>().OnDamageTaken();

        if( _crabHealth == 2)
        {

        }
        if (_crabHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnBossDead?.Invoke();
    }
}
