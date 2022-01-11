using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private UnityEvent _coinCollected;

    private int _startHealth = 2;
    private int _maxHealth = 5;
    private int _currentHealth;
    private int _money = 0;
    private List<Sound> _sounds = new List<Sound>();
    private int _moneyToPay;

    public int Money => _money;

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;
    public event UnityAction<Enemy> KilledEnemy;
    public event UnityAction<int> NumberOfCoinsChanged;

    private void Awake()
    {
        _currentHealth = _startHealth;
    }

    private void Start()
    {
        NumberOfCoinsChanged?.Invoke(_money);
        HealthChanged?.Invoke(_currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            KilledEnemy?.Invoke(enemy);
        }

        if (other.TryGetComponent<Coin>(out Coin coin))
        {
            _coinCollected?.Invoke();
            TakeReward(coin.Reward);
        }
    }

    public void TakeDamage(int damage)
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
        {
            Die();
        }
    }

    public void TakeHealth(int health)
    {
        if (health >= 0 && _currentHealth <= _maxHealth)
        {
            _currentHealth += health;
            HealthChanged?.Invoke(_currentHealth);
        }
    }

    private void TakeReward(int reward)
    {
        if (reward >= 0)
        {
            _money += reward;

            NumberOfCoinsChanged?.Invoke(_money);
        }
    }

    public void AddPurchasedSound(Sound sound)
    {
        _sounds.Add(sound);
    }

    public bool CheckSolvency(Sound sound)
    {
        _moneyToPay = sound.Price;

        if (_money >= _moneyToPay)
        {
            return true;
        }
        else
        {
            _moneyToPay = 0;
            return false;
        }
    }

    public void ToPay()
    {
        _money -= _moneyToPay;
    }

    public void ResetPlayer()
    {
        _money = 0;
        _currentHealth = _startHealth;
        HealthChanged?.Invoke(_currentHealth);
        _mover.ResetMover();
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
