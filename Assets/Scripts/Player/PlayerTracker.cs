using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float smoothing = 30f;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - _player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 targetCameraPosition = _player.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
    }
}
