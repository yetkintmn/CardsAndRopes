using UnityEngine;

// Normalde kulland���m SaveManager'� eklemedim ��nk� �o�u k�sm� UDO i�erisinde geli�tirilmi�ti.
// Bu sebeple bu oyun �zelinde �al��acak basit bir manager yapt�m

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
        CurrencyManager.Instance.SetCurrency(PlayerPrefs.GetInt("currency"));
        LevelManager.Instance.SetLevel(PlayerPrefs.GetInt("level"));
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("isfirstplay", 0);
        PlayerPrefs.SetInt("currency", CurrencyManager.Instance.GetCurrency());
        PlayerPrefs.SetInt("level", LevelManager.Instance.GetLevel());
        PlayerPrefs.Save();
    }
}
