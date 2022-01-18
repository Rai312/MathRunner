using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : Screen
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingButton;

    public event UnityAction PlayButtonClick;
    public event UnityAction ShopButtonClick;
    public event UnityAction SettingButtonClick;
    public event UnityAction ExitButtonClick;

    protected override void OnEnable()
    {
        base.OnEnable();
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
        _settingButton.onClick.AddListener(OnSettingButtonClick);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _shopButton.onClick.RemoveListener(OnShopButtonClick);
        _settingButton.onClick.RemoveListener(OnSettingButtonClick);
    }

    protected override void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }

    private void OnShopButtonClick()
    {
        ShopButtonClick?.Invoke();
    }

    private void OnPlayButtonClick()
    {
        PlayButtonClick?.Invoke();
    }

    private void OnSettingButtonClick()
    {
        SettingButtonClick?.Invoke();
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        _playButton.interactable = false;
        _shopButton.interactable = false;
        ExitButton.interactable = false;
        _settingButton.interactable = false;
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        _playButton.interactable = true;
        _shopButton.interactable = true;
        ExitButton.interactable = true;
        _settingButton.interactable = true;
    }
}
