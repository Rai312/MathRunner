using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class SoundView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _sellButton;

    private Sound _sound;

    public event UnityAction<Sound, SoundView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    private void TryLockItem()
    {
        if (_sound.IsBuyed)
            _sellButton.interactable = false;
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_sound, this);
    }

    public void Render(Sound sound)
    {
        _sound = sound;
        _label.text = sound.Label;
        _price.text = sound.Price.ToString();
    }
}
