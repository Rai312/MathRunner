using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopScreen : Screen
{
    [SerializeField] private List<Sound> _sounds;
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

    private void AddItem(Sound sound)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(sound);
    }

    private void OnSellButtonClick(Sound sound, SoundView view)
    {
        TrySellSound(sound, view);
    }

    private void TrySellSound(Sound sound, SoundView view)
    {
        if (_player.CheckSolvency(sound))
        {
            _player.ToPay();
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
