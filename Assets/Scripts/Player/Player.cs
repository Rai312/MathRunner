using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;

    private int _startHealth = 2;
    private int _maxHealth = 5;
    private int _currentHealth;
    private List<SoundItem> _sounds = new List<SoundItem>();

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;
    public event UnityAction<Enemy> KilledEnemy;

    private void Awake()
    {
        _currentHealth = _startHealth;
    }

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
            KilledEnemy?.Invoke(enemy);
    }

    public void Damage(int damage)
    {
        if (damage >= 0)
        {
            if (_currentHealth > 0 && _currentHealth >= damage)
            {
                _currentHealth -= damage;
                HealthChanged?.Invoke(_currentHealth);
            }
        }

        if (_currentHealth <= 0)
            Die();
    }

    public void Heal(int health)
    {
        if (health >= 0 && _currentHealth <= _maxHealth)
        {
            if (_currentHealth + health > _maxHealth)
                _currentHealth = _maxHealth;
            else
                _currentHealth += health;

            HealthChanged?.Invoke(_currentHealth);
        }
    }

    public void AddPurchasedSound(SoundItem sound)
    {
        _sounds.Add(sound);
    }

    public void Reset()
    {
        _currentHealth = _startHealth;
        HealthChanged?.Invoke(_currentHealth);
        _mover.Reset();
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
