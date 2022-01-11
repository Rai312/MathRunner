using UnityEngine;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;
    [SerializeField] protected Button ExitButton;

    private void OnEnable()
    {
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        ExitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected abstract void OnExitButtonClick();

    public abstract void Open();

    public abstract void Close();
}
