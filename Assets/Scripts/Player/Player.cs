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
    private int _moneyDuringGame;
    private List<SoundItem> _sounds = new List<SoundItem>();

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;
    public event UnityAction<Enemy> KilledEnemy;
    public event UnityAction<int> NumberOfCoinsChanged;

    public int MoneyDuringGame => _moneyDuringGame;

    private void Awake()
    {
        _currentHealth = _startHealth;
    }

    private void Start()
    {
        NumberOfCoinsChanged?.Invoke(PlayerPrefs.GetInt("Money"));
        HealthChanged?.Invoke(_currentHealth);
        _moneyDuringGame = MoneyManager.StartMoney;
        PlayerPrefs.SetInt("Money", 0);
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
            _moneyDuringGame += reward;

            NumberOfCoinsChanged?.Invoke(_moneyDuringGame);
        }
    }

    public void AddPurchasedSound(SoundItem sound)
    {
        _sounds.Add(sound);
    }

    public void ResetPlayer()
    {
        _moneyDuringGame = MoneyManager.StartMoney;
        _currentHealth = _startHealth;
        HealthChanged?.Invoke(_currentHealth);
        _mover.ResetMover();
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
