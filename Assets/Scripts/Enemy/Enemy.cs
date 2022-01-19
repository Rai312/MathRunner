using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;

    public int Damage => _damage;
    public int Reward => _reward;

    private void OnTriggerEnter(Collider other)
    {
        Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
