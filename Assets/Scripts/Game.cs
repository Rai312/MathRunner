using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private RoadGenerator _roadGenerator;
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private SettingScreen _settingScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private CoinDisplay _coinDisplay;

    private void OnEnable()
    {
        _startScreen.PlayButtonClick += OnPlayButtonClick;
        _startScreen.ShopButtonClick += OnShopButtonClick;
        _startScreen.SettingButtonClick += OnSettingButtonClick;
        _startScreen.ExitButtonClick += OnExitGameButtonClick;

        _settingScreen.ExitButtonClick += OnExitSettingButtonClick;

        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _gameOverScreen.ExitButtonClick += OnExitGameOverButtonClick;

        _shopScreen.ExitButtonClick += OnExitShopButtonClick;

        _player.Died += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonClick;
        _startScreen.ShopButtonClick -= OnShopButtonClick;
        _startScreen.SettingButtonClick -= OnSettingButtonClick;
        _startScreen.ExitButtonClick -= OnExitGameButtonClick;

        _settingScreen.ExitButtonClick -= OnExitSettingButtonClick;

        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _gameOverScreen.ExitButtonClick -= OnExitGameOverButtonClick;

        _shopScreen.ExitButtonClick -= OnExitShopButtonClick;

        _player.Died -= OnGameOver;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnShopButtonClick()
    {
        _startScreen.Close();
        _shopScreen.Open();
    }

    private void OnSettingButtonClick()
    {
        _startScreen.Close();
        _settingScreen.Open();
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        StartGame();
    }

    private void OnExitShopButtonClick()
    {
        _shopScreen.Close();
        _startScreen.Open();
    }

    private void OnExitSettingButtonClick()
    {
        _settingScreen.Close();
        _startScreen.Open();
    }

    private void OnExitGameOverButtonClick()
    {
        _gameOverScreen.Close();
        _startScreen.Open();
    }

    private void OnExitGameButtonClick()
    {
        Application.Quit();
    }
    
    private void StartGame()
    {
        MoneyManager.MoneyDuringGame = MoneyManager.StartMoney;
        Time.timeScale = 1;
        _player.Reset();

        _roadGenerator.enabled = true;
        _roadGenerator.Reset();

        _mapGenerator.enabled = true;
        _mapGenerator.Reset();
    }

    private void OnGameOver()
    {
        _coinDisplay.SetCurrentMoney();
        _gameOverScreen.Open();
        Time.timeScale = 0;
    }
}
