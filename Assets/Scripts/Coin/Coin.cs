using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _reward;
    [SerializeField] private float _speedRotation;

    public int Reward => _reward;

    private void OnTriggerEnter(Collider other)
    {
        Die();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, _speedRotation * Time.deltaTime, 0));
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
