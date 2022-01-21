using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinDisplay;

    private void OnEnable()
    {
        MoneyManager.NumberOfCoinsChanged += OnNumberOfCoinsChanged;
    }

    private void OnDisable()
    {
        MoneyManager.NumberOfCoinsChanged -= OnNumberOfCoinsChanged;
    }

    private void Start()
    {
        _coinDisplay.text = MoneyManager.CurrentMoney.ToString();
    }

    private void OnNumberOfCoinsChanged(int coins)
    {
        _coinDisplay.text = coins.ToString();
    }

    public void SetCurrentMoney()
    {
        _coinDisplay.text = MoneyManager.CurrentMoney.ToString();
    }
}
