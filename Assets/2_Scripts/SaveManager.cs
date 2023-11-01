using UnityEngine;

// Normalde kullandýðým SaveManager'ý eklemedim çünkü çoðu kýsmý UDO içerisinde geliþtirilmiþti.
// Bu sebeple bu oyun özelinde çalýþacak basit bir manager yaptým

public class SaveManager : Singleton<SaveManager>
{
    public bool IsFirstPlay {  get; private set; }

    public override void Awake()
    {
        base.Awake();
        LoadData();
    }

    private void LoadData()
    {
        IsFirstPlay = true;
        IsFirstPlay = !PlayerPrefs.HasKey("isfirstplay");
        CurrencyManager.Instance.LoadCurrency(PlayerPrefs.GetInt("currency"));
        LevelManager.Instance.SetLevel(PlayerPrefs.GetInt("level"));
    }

    public void SaveData()
    {
        IsFirstPlay = false;
        PlayerPrefs.SetInt("isfirstplay", 0);
        PlayerPrefs.SetInt("currency", CurrencyManager.Instance.GetCurrency());
        PlayerPrefs.SetInt("level", LevelManager.Instance.Level);
        PlayerPrefs.Save();
    }
}
