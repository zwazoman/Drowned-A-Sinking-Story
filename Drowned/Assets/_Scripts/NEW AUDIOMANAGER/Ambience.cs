using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    [SerializeField] Sounds[] AmbientSounds;

    [SerializeField] float _minTimer;
    [SerializeField] float _maxTimer;

    [SerializeField] float _sphereSize;

    float _timer;

    GameObject _fishHead;

    private void Awake()
    {
        _fishHead = GameObject.Find("head");
        _timer = Random.Range(_minTimer, _maxTimer);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            Vector3 spawnPoint = _fishHead.transform.position + Random.insideUnitSphere * _sphereSize;
            AudioManager.Instance.PlaySFXClipAtPosition(AmbientSounds[Random.Range(0, AmbientSounds.Length)], spawnPoint);

            GameObject test = new GameObject();
            test.transform.position = spawnPoint;

            _timer = Random.Range(_minTimer, _maxTimer);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_fishHead.transform.position, _sphereSize);
    }


}
