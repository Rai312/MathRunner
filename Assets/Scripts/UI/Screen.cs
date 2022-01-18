using UnityEngine;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;
    [SerializeField] protected Button ExitButton;

    protected virtual void OnEnable()
    {
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    protected virtual void OnDisable()
    {
        ExitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected abstract void OnExitButtonClick();

    public abstract void Open();

    public abstract void Close();
}
