using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private GameObject _container;
    [SerializeField] private float _startSpeed;
    
    private List<GameObject> _pool = new List<GameObject>();
    private Camera _camera;
    protected float DistanceBetweenPlayerAndItem = -1;

    protected float ScalingSpeed { get; set; }

    protected void Initialize(List<GameObject> prefabs)
    {
        _camera = Camera.main;

        for (int i = 0; i < _capacity; i++)
        {
            int randomIndexRoad = Random.Range(0, prefabs.Count);
            GameObject spawned = Instantiate(prefabs[randomIndexRoad], _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        var results = _pool.Where(p => p.activeSelf == false);

        int randomNumberOfObject = Random.Range(0, results.Count());

        result = results.ElementAtOrDefault(randomNumberOfObject);

        return result != null;
    }

    protected int GetNumberOfActiveRoads()
    {
        int result = _pool.Count(p => p.activeSelf == true);

        return result;
    }

    protected void DisableObjectAbroadScreen(float disablePointZ)
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector3(0, 0, disablePointZ));

        foreach (var item in _pool)
        {
            if (item.activeSelf == true)
            {
                if (item.transform.position.z < disablePoint.z)
                {
                    item.SetActive(false);
                }
            }
        }
    }

    protected void MoveActiveObject(float scalingSpeed)
    {
        foreach (GameObject gameobject in _pool)
        {
            if (gameObject.activeSelf == true)
            {
                gameobject.transform.position -= new Vector3(0f, 0f, scalingSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            if (item.activeSelf == true)
            {
                item.SetActive(false);
            }
        }
        ScalingSpeed = _startSpeed;
    }
}
