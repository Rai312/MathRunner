using UnityEngine;
using UnityEngine.Events;

public static class MoneyManager
{
    public const int StartMoney = 0;
    public const string Money = "Money";
    public static int MoneyDuringGame;
    public static int CurrentMoney = PlayerPrefs.GetInt(Money);
    public static event UnityAction<int> NumberOfCoinsChanged;

    public static void Pay(int money)
    {
        PlayerPrefs.SetInt(Money, CurrentMoney - money);
        PlayerPrefs.Save();
    }

    public static void TakeReward(int reward)
    {
        if (reward >= 0)
        {
            MoneyDuringGame += reward;

            PlayerPrefs.SetInt(Money, CurrentMoney + reward);
            CurrentMoney = PlayerPrefs.GetInt(Money);
            PlayerPrefs.Save();

            NumberOfCoinsChanged?.Invoke(MoneyDuringGame);
        }
    }  
}
