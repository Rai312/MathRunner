using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class MapGenerator : ObjectPool
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private Player _player;
    [SerializeField] private int _distanceBetweenMap = 10;
    [SerializeField] private int _maxCountMap;
    [SerializeField] private float _stepScalingSpeed = 2f;
    [SerializeField] private float _mapPozitionZ = -20;

    private List<GameObject> _maps = new List<GameObject>();
    private GameObject _lastMap;
    private int _itemCountInMap = 6;
    private int _coinsCountInItem = 4;
    private float _coinsHeight = 0.5f;
    private enum TrackPosition { Left = -1, Middle = 0, Right = 1};
    private int _mapSize;
    private float _disablePointZ = -40f;
    private float _elapsedTime;
    private float _timeBetweenSpeedScaling = 5;

    private void Awake()
    {
        this.enabled = false;

        MakeMap(ref _maps);
        _mapSize = _itemCountInMap * _distanceBetweenMap;

    }

    private void Start()
    {
        Initialize(_maps);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _timeBetweenSpeedScaling)
        {
            ScalingSpeed += _stepScalingSpeed;
            _elapsedTime = 0;
        }
    }

    private void FixedUpdate()
    {
        int numberOfActiveRoads = GetNumberOfActiveRoads();

        if (numberOfActiveRoads < _maxCountMap)
        {
            if (TryGetObject(out GameObject map))
            {
                if (numberOfActiveRoads == 0)
                {
                    SpawnMap(map, new Vector3(transform.position.x, transform.position.y, _mapPozitionZ));
                    _lastMap = map;
                }
                else
                {
                    if (Vector3.Distance(_player.transform.position, _lastMap.transform.position) > DistanceBetweenPlayerAndItem)
                    {
                        SpawnMap(map, _lastMap.transform.position + new Vector3(transform.position.x, transform.position.y, _mapSize));
                        _lastMap = map;
                    }
                }
            }
        }
        DisableObjectAbroadScreen(_disablePointZ);
        MoveActiveObject(ScalingSpeed);
    }

    private void MakeMap(ref List<GameObject> maps)
    {
        GameObject result = new GameObject("Map");
        for (int i = 0; i < _itemCountInMap; i++)
        {
            GameObject obstacle = null;
            TrackPosition trackPosition = TrackPosition.Middle;

            if (i == 2)
            {
                trackPosition = TrackPosition.Left;
                obstacle = _obstaclePrefab;
            }
            else if (i == 3)
            {
                trackPosition = TrackPosition.Middle;
                obstacle = _obstaclePrefab;
            }
            else if (i == 4)
            {
                trackPosition = TrackPosition.Right;
                obstacle = _obstaclePrefab;
            }

            Vector3 obstaclePosition = new Vector3((int)trackPosition, 0, i * _distanceBetweenMap);
            CreateCoins(obstaclePosition, result);

            if (obstacle != null)
            {
                GameObject newObstacle = Instantiate(obstacle, obstaclePosition, Quaternion.identity);
                newObstacle.transform.SetParent(result.transform);
            }
        }
        result.SetActive(false);
        maps.Add(result);
    }

    private void CreateCoins(Vector3 obstaclePosition, GameObject parentObject)
    {
        Vector3 coinPosition = Vector3.zero;
        for (int i = -_coinsCountInItem / 2; i < _coinsCountInItem / 2; i++)
        {
            coinPosition.y = _coinsHeight;
            coinPosition.z = i * ((float)_distanceBetweenMap / _coinsCountInItem);
            GameObject coin = Instantiate(_coinPrefab, coinPosition + obstaclePosition, Quaternion.identity);
            coin.transform.SetParent(parentObject.transform);
        }
    }

    private void SpawnMap(GameObject map, Vector3 spawnPoint)
    {
        map.SetActive(true);
        map.transform.position = spawnPoint;

        for (int i = 0; i < map.transform.childCount; i++)
        {
            if (map.transform.GetChild(i).gameObject.activeSelf == false)
            {
                map.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
