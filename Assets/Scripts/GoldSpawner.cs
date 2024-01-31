using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] private Gold _goldPrefab;
    [SerializeField] private Vector3 _spawnAreaSize;
    [SerializeField][Range(0, 30)] private int _amount;
    [SerializeField][Min(0)] private float _delay;

    private Queue<Transform> _goldPositionsQueue;

    private void Start()
    {
        _goldPositionsQueue = new Queue<Transform>();
        StartCoroutine(SpawnGoldWithDelay());
    }

    private void Update()
    {
        RandomGoldSpawnByClick();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, _spawnAreaSize);
    }

    public Transform TryGetGoldPosition()
    {
        if (_goldPositionsQueue.Count > 0)
        {
            return _goldPositionsQueue.Dequeue();
        }
        else
        {
            return null;
        }
    }

    private IEnumerator SpawnGoldWithDelay()
    {
        var waitForeSeconds = new WaitForSeconds(_delay);

        while (_amount > 0)
        {
            yield return waitForeSeconds;

            RandomGoldSpawn();

            _amount--;
        }
    }

    private void RandomGoldSpawn()
    {
        const float Half = 0.5f;

        float randomX = Random.Range(transform.position.x - _spawnAreaSize.x * Half, transform.position.x + _spawnAreaSize.x * Half);

        float randomZ = Random.Range(transform.position.z - _spawnAreaSize.z * Half, transform.position.z + _spawnAreaSize.z * Half);

        Gold gold = Instantiate(_goldPrefab, new Vector3(randomX, _goldPrefab.transform.position.y, randomZ), Quaternion.identity);

        _goldPositionsQueue.Enqueue(gold.transform);
    }

    private void RandomGoldSpawnByClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RandomGoldSpawn();
        }
    }
}
