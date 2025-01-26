using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishDeath : MonoBehaviour
{
    SceneChanger changer;
    Health health;

    private void Awake()
    {
        TryGetComponent(out health);
        TryGetComponent(out changer);
    }

    private void Start()
    {
        health.OnDie += () => StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        Destroy(GetComponent<PlayerInput>());
        yield return new WaitForSeconds(5);
        changer.GameOver();
    }
}
