using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    GameObject _currentBubble;

    [SerializeField] Transform _airBubbleSpawnSocket;

    [SerializeField] float _minWaitTime = 2;
    [SerializeField] float _maxWaitTime = 5;

    private void Start()
    {
        SummonBubble();
    }

    void SummonBubble()
    {
        _currentBubble = PoolManager.Instance.AccessPool(Pools.AirBubble).TakeFromPool(_airBubbleSpawnSocket.position, Quaternion.identity);
        _currentBubble.TryGetComponent(out PoolObject poolObject);
        poolObject.OnPushedToPool += Spawn;
    }

    void Spawn()
    {
        StartCoroutine(SpawnAirBubble());
    }

    IEnumerator SpawnAirBubble()
    {
        yield return new WaitForSeconds(Random.Range(_minWaitTime, _maxWaitTime));
        _currentBubble.TryGetComponent(out PoolObject poolObject);
        poolObject.OnPushedToPool -= Spawn;
        SummonBubble();
    }
}
