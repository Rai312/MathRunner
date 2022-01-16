using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _trackingSpeed = 15f;
    private Vector3 _distanceBetweenPlayerAndCamera;

    void Start()
    {
        _distanceBetweenPlayerAndCamera = transform.position - _player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 targetCameraPosition = _player.transform.position + _distanceBetweenPlayerAndCamera;

        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, _trackingSpeed * Time.deltaTime);
    }
}
