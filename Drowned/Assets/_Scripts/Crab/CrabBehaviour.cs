using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrabBehaviour : MonoBehaviour
{
    GameObject _fishHead;
    [Header("References")]
    [SerializeField] private Animator _animator;

    [Header("parameters")]
    [SerializeField] private float _rangeAttackTreshold = 200;

    Vector3 targetPosition;

    bool _lookAtPlayer;

    string[] _attaquesDeLoin = new string[]
    {
        "atk_laser",
        "atk_shoot",
        "atk_shoot",
        //"atk_opportunity",
    };

    string[] _attaquesDePres = new string[]
    {
        "atk_d",
        "atk_d",
        "atk_g",
        "atk_g",
        "atk_f",
        //"atk_opportunity",
    };

    private void Awake()
    {
        if (_fishHead == null) _fishHead = GameObject.Find("head");
    }

    private void Start()
    {
        StartCoroutine(Play());
        BossHealth.OnBossDead += die;

        targetPosition = _fishHead.transform.position;
    }

    void die()
    {
        StopAllCoroutines();
        _animator.SetTrigger("Die");
        _lookAtPlayer = false;
        enabled = false;
    }

    public void OnDamageTaken()
    {
        Debug.LogWarning("trdcytfvugyiguotydfkcvihbojupijygufylkchvlpiyguycgtkvhju");
        StopAllCoroutines();
        foreach(string s in _attaquesDeLoin)
        {
            _animator.ResetTrigger(s);
        }
        foreach (string s in _attaquesDePres)
        {
            _animator.ResetTrigger(s);
        }
        _animator.SetTrigger("opportunity");
        StartCoroutine(Play(1));
    }

    IEnumerator Play(float a = 0)
    {
        yield return new WaitForSeconds(a);
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(Random.Range(2,5));

            _lookAtPlayer = false;
            if ((transform.position - FishController.Instance.rb1.position).sqrMagnitude < _rangeAttackTreshold * _rangeAttackTreshold)
            {
                _animator.SetTrigger(_attaquesDePres[Random.Range(0, _attaquesDePres.Length)]);
            }
            else
            {
                _animator.SetTrigger(_attaquesDeLoin[Random.Range(0, _attaquesDeLoin.Length)]);
            }
            yield return 0;
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length* _animator.GetCurrentAnimatorStateInfo(0).speed);
            _lookAtPlayer = true;
        }
    }

    private void Update()
    {
        Vector3 vel = Vector3.zero;
        if (_lookAtPlayer) targetPosition = Vector3.SmoothDamp(targetPosition, _fishHead.transform.position, ref vel, 0.08f); 
        transform.LookAt(targetPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,.5f,.5f,.1f);
        Gizmos.DrawSphere(transform.position,_rangeAttackTreshold);
    }
}
