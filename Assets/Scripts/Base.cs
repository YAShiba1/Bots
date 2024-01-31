using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private BotCollector _botPrefab;
    [SerializeField] private GoldSpawner _goldSpawner;
    [SerializeField] private ScoreViewer _scoreViewer;
    [SerializeField] private Transform[] _botsSpawnPoints;

    private List<BotCollector> _spawnedBots;
    private float _goldCount = 0;

    private void Start()
    {
        _spawnedBots = new List<BotCollector>();
        SpawnBots();
    }

    private void Update()
    {
        TrySendBotForResources();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Gold gold))
        {
            _goldCount++;
            _scoreViewer.SetScore(_goldCount);
            Destroy(gold.gameObject);
        }
    }

    private void SpawnBots()
    {
        for (int i = 0; i < _botsSpawnPoints.Length; i++)
        {
            BotCollector bot = Instantiate(_botPrefab, _botsSpawnPoints[i]);
            _spawnedBots.Add(bot);
        }
    }

    private void TrySendBotForResources()
    {
        for (int i = 0; i < _spawnedBots.Count; i++)
        {
            if (_spawnedBots[i].IsFree)
            {
                Transform goldPosition = _goldSpawner.TryGetGoldPosition();

                _spawnedBots[i].SetTarget(goldPosition);
            }

            if (_spawnedBots[i].IsPickedGold)
            {
                _spawnedBots[i].SetTarget(transform);
            }
        }
    }
}
