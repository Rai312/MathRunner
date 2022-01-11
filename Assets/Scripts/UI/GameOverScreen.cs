using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : Screen
{
    [SerializeField] private Button _restartButton;

    public event UnityAction RestartButtonClick;
    public event UnityAction ExitButtonClick;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        _restartButton.interactable = false;
        ExitButton.interactable = false;
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        _restartButton.interactable = true;
        ExitButton.interactable = true;
    }

    private void OnRestartButtonClick()
    {
        RestartButtonClick?.Invoke();
    }

    protected override void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }
}
