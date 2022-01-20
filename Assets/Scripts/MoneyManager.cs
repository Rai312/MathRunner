using UnityEngine;

public static class MoneyManager
{
    public const int StartMoney = 0;
    public const string Money = "Money";

    public static void Pay(int money)
    {
        PlayerPrefs.SetInt(Money, PlayerPrefs.GetInt(Money) - money);
        PlayerPrefs.Save();
    }
}
