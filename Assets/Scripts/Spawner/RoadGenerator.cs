using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadGenerator))]
public class RoadGenerator : ObjectPool
{
    [SerializeField] private List<GameObject> _roads;
    [SerializeField] private int _maxCountRoad = 8;
    [SerializeField] private float _distanceBetweenRoad = 10;
    [SerializeField] private Player _player;

    private GameObject _lastRoad;
    private float _disablePointZ = -20f;
    private float _elapsedTime;
    private float _timeBetweenSpeedScaling = 5;

    private void Awake()
    {
        this.enabled = false;
    }

    private void Start()
    {
        Initialize(_roads);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _timeBetweenSpeedScaling)
        {
            ScalingSpeed += 2f;
            _elapsedTime = 0;
        }
    }

    private void FixedUpdate()
    {
        int numberOfActiveRoads = GetNumberOfActiveRoads();

        if (numberOfActiveRoads < _maxCountRoad)
        {
            if (TryGetObject(out GameObject road))
            {
                if (numberOfActiveRoads == 0 )
                {
                    SpawnRoad(road, Vector3.zero);
                    _lastRoad = road;
                }
                else
                {
                    if (Vector3.Distance(_player.transform.position, _lastRoad.transform.position) > DistanceBetweenPlayerAndItem)
                    {
                        SpawnRoad(road, _lastRoad.transform.position + new Vector3(transform.position.x, transform.position.y, _distanceBetweenRoad));
                        _lastRoad = road;
                    }
                }
            }
        }
        DisableObjectAbroadScreen(_disablePointZ);
        MoveActiveObject(ScalingSpeed);
    }

    private void SpawnRoad(GameObject road, Vector3 spawnPoint)
    {
        road.SetActive(true);
        road.transform.position = spawnPoint;
    }
}