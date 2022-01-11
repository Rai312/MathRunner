using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _healthOfReward;

    public int Damage => _damage;
    public int HealthOfReward => _healthOfReward;

    private void OnTriggerEnter(Collider other)
    {
        Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
