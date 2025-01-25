using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBehaviour : MonoBehaviour
{
    GameObject _fishHead;
    [Header("References")]
    [SerializeField] private Animator _animator;
    private void Awake()
    {
        if (_fishHead == null) _fishHead = GameObject.Find("head");
    }

    private void Start()
    {
        StartCoroutine(Play());
    }


    IEnumerator Play()
    {
        while (isActiveAndEnabled)
        {
            Debug.Log("putaaaaaaaaaaaaaaaaaaaaain");
            yield return new WaitForSeconds(Random.Range(1, 2));

            _animator.SetTrigger(Random.Range(0, 6));
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
            
        }
    }

    private void Update()
    {
        transform.LookAt(_fishHead.transform);
    }
}
