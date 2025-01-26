using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDie;
    public event Action<float> OnDamageTaken;

    [SerializeField]public int _maxHP;

    public float _currentHP;

    private void Awake()
    {
        _currentHP = _maxHP;
    }

    public void ApplyDamage(float amount)
    {
        _currentHP -= amount;
        OnDamageTaken?.Invoke(_currentHP);
        if (_currentHP <= 0) Die();
    }

    public void Heal(float amount)
    {
        _currentHP += amount;
        if (_currentHP >= _maxHP) _currentHP = _maxHP;
    }

    void Die()
    {
        OnDie?.Invoke();
    }
}
