using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class MathGameScreen : MonoBehaviour
{
    [SerializeField] private Button _firstOptionButton;
    [SerializeField] private Button _secondOptionButton;
    [SerializeField] private Button _thirdOptionButton;
    [SerializeField] private TMP_Text _task;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Slider _slider;
    
    private CanvasGroup _mathGameGroup;
    private bool _isRightAnswer;
    private int _rightAnswer;
    private readonly float _maxValue = 1f;
    private readonly float _minValue = 0f;
    private readonly char[] _mathSigns = { '*', '+', '-' };
    private readonly float _scalingTime = 0.1f;
    private readonly float _gameTime = 5f;
    private float _timeElapsed;
    private bool _isButtonPressed = false;

    private void Start()
    {
        _mathGameGroup = GetComponent<CanvasGroup>();
        _mathGameGroup.alpha = _minValue;
        _slider.value = _minValue;
    }

    private void OnEnable()
    {
        _player.KilledEnemy += OnKilledEnemy;

        _firstOptionButton.onClick.AddListener(OnClickFirstOptionButton);
        _secondOptionButton.onClick.AddListener(OnClickSecondOptionButton);
        _thirdOptionButton.onClick.AddListener(OnClickThirdOptionButton);
    }

    private void OnDisable()
    {
        _player.KilledEnemy -= OnKilledEnemy;

        _firstOptionButton.onClick.RemoveListener(OnClickFirstOptionButton);
        _secondOptionButton.onClick.RemoveListener(OnClickSecondOptionButton);
        _thirdOptionButton.onClick.RemoveListener(OnClickThirdOptionButton);
    }

    private void OnKilledEnemy(Enemy enemy)
    {
        StopCoroutine(FadeTimeGame(enemy));

        CreateTask();

        InitializeButtonText();

        StartCoroutine(FadeTimeGame(enemy));
    }

    private IEnumerator FadeTimeGame(Enemy enemy)
    {
        _slider.value = _slider.minValue;
        _mathGameGroup.alpha = _maxValue;
        
        Time.timeScale = _scalingTime;

        while (_timeElapsed < _gameTime)
        {
            _timeElapsed += Time.deltaTime / _scalingTime;
            StartCoroutine(FadeTime());
            if (_isButtonPressed)
            {
                if (_isRightAnswer)
                {
                    _timeElapsed = 0f;
                    _player.TakeHealth(enemy.HealthOfReward);
                    _mathGameGroup.alpha = _minValue;
                    Time.timeScale = _maxValue;
                    _isRightAnswer = false;
                    _isButtonPressed = false;

                    StopCoroutine(FadeTime());
                    yield break;
                }
                else
                {
                    _timeElapsed = 0f;

                    Time.timeScale = _maxValue;
                    _player.TakeDamage(enemy.Damage);
                    _mathGameGroup.alpha = _minValue;
                    _isButtonPressed = false;

                    StopCoroutine(FadeTime());
                    yield break;
                }
            }
            yield return null;
        }

        if (_isButtonPressed == false)
        {
            _timeElapsed = 0f;
            Time.timeScale = _maxValue;
            _player.TakeDamage(enemy.Damage);
            _mathGameGroup.alpha = _minValue;
        }
    }

    private IEnumerator FadeTime()
    {
        while (_slider.value != _gameTime)
        {
            _slider.value = Mathf.MoveTowards(_slider.minValue, _slider.maxValue, _timeElapsed / _gameTime);
            yield return null;
        }
    }

    private void CreateTask()
    {
        int minValue = 0;
        int maxValue = 10;

        char randomSign = _mathSigns[Random.Range(minValue, _mathSigns.Length)];
        int randomFirstNumber = Random.Range(minValue, maxValue);
        int randomSecondNumber = Random.Range(minValue, maxValue);
        _task.text = randomFirstNumber + randomSign.ToString() + randomSecondNumber;
        PerformMathematicalOperation(randomFirstNumber, randomSecondNumber, randomSign);
    }

    private void PerformMathematicalOperation(int firstNumber, int secondNumber, char sign)
    {
        int result = 0;

        if (sign == '*')
            result = firstNumber * secondNumber;
        else if (sign == '+')
            result = firstNumber + secondNumber;
        else if (sign == '-')
            result = firstNumber - secondNumber;

        _rightAnswer = result;
    }

    private void CompareAnswer(string textButton)
    {
        if (textButton == _rightAnswer.ToString())
            _isRightAnswer = true;
        else
            _isRightAnswer = false;
    }

    private void OnClickFirstOptionButton()
    {
        _isButtonPressed = true;
        CompareAnswer(_firstOptionButton.GetComponentInChildren<TMP_Text>().text);
    }

    private void OnClickSecondOptionButton()
    {
        _isButtonPressed = true;
        CompareAnswer(_secondOptionButton.GetComponentInChildren<TMP_Text>().text);
    }

    private void OnClickThirdOptionButton()
    {
        _isButtonPressed = true;
        CompareAnswer(_thirdOptionButton.GetComponentInChildren<TMP_Text>().text);
    }

    private void InitializeButtonText()
    {
        Button[] buttons = { _firstOptionButton, _secondOptionButton, _thirdOptionButton };
        List<int> numbers = new List<int>();
        int randomIndex = Random.Range(0, buttons.Length);

        numbers.Add(CreateUniqueRandomNumber(int.MinValue));

        for (int i = 1; i < buttons.Length; i++)
        {
            numbers.Add(CreateUniqueRandomNumber(numbers[i - 1]));
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TMP_Text>().text = numbers[i].ToString();
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == randomIndex)
            {
                buttons[i].GetComponentInChildren<TMP_Text>().text = _rightAnswer.ToString();
                return;
            }
        }
    }

    private int CreateUniqueRandomNumber(int lastNumber)
    {
        int minValue = -50;
        int maxValue = 51;

        int randomNumber = Random.Range(minValue, maxValue);
        if (randomNumber == lastNumber && randomNumber == _rightAnswer)
        {
            randomNumber = Random.Range(minValue, maxValue);
        }

        return randomNumber;
    }
}
