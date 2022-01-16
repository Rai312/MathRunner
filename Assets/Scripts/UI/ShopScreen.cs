using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopScreen : Screen
{
    [SerializeField] private List<SoundItem> _sounds;
    [SerializeField] private Player _player;
    [SerializeField] private SoundView _template;
    [SerializeField] private GameObject _itemContainer;

    public event UnityAction ExitButtonClick;

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        ExitButton.interactable = true;
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        ExitButton.interactable = false;
    }

    private void Start()
    {
        for (int i = 0; i < _sounds.Count; i++)
        {
            AddItem(_sounds[i]);
        }
    }

    private void AddItem(SoundItem sound)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(sound);
    }

    private void OnSellButtonClick(SoundItem sound, SoundView view)
    {
        TrySellSound(sound, view);
    }

    private bool CheckSolvency(SoundItem sound)
    {
        if (PlayerPrefs.GetInt("Money") >= sound.Price)
            return true;
        else
            return false;
    }

    private void TrySellSound(SoundItem sound, SoundView view)
    {
        if (CheckSolvency(sound))
        {
            MoneyManager.Pay(sound.Price);
            _player.AddPurchasedSound(sound);
            sound.IsBuyed = true;
            view.SellButtonClick -= OnSellButtonClick;
        }
    }

    protected override void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }
}
