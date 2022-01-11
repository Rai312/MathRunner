using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinDisplay;

    private void OnEnable()
    {
        _player.NumberOfCoinsChanged += OnNumberOfCoinsChanged;
    }

    private void OnDisable()
    {
        _player.NumberOfCoinsChanged -= OnNumberOfCoinsChanged;
    }

    private void OnNumberOfCoinsChanged(int coins)
    {
        _coinDisplay.text = coins.ToString();
    }
}
