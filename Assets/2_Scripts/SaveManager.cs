using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Normalde kullandýðým SaveManager'ý eklemedim çünkü çoðu kýsmý UDO içerisinde geliþtirilmiþti.
// Bu sebeple bu oyun özelinde çalýþacak basit bir manager yaptým

public class SaveManager : Singleton<SaveManager>
{
    public override void Awake()
    {
        base.Awake();
        LoadData();
    }

    private void LoadData()
    {
        CurrencyManager.Instance.SetCurrency(PlayerPrefs.GetInt("currency"));
        LevelManager.Instance.SetLevel(PlayerPrefs.GetInt("level"));
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("currency", CurrencyManager.Instance.GetCurrency());
        PlayerPrefs.SetInt("level", LevelManager.Instance.GetLevel());
        PlayerPrefs.Save();
    }
}
