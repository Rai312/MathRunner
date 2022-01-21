using UnityEngine;
using UnityEngine.Events;


public class CoinCollector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UnityEvent _coinCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Coin>(out Coin coin))
        {
            _coinCollected?.Invoke();
            MoneyManager.TakeReward(coin.Reward);
        }
    }
}
