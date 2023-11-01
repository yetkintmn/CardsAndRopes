using TMN;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private int _currency;

    private void OnEnable()
    {
        //EventManager.Get<BuyCard>().AddListener(SetCurrency);
    }

    private void OnDisable()
    {
        //EventManager.Get<BuyCard>().RemoveListener(SetCurrency);
    }

    public void LoadCurrency(int currency)
    {
        if (SaveManager.Instance.IsFirstPlay)
        {
            _currency = 50;
            return;
        }
        _currency = currency;
        EventManager.Get<ChangeCurrency>().Execute();
    }

    public void SetCurrency(int currency)
    {
        _currency += currency;
        EventManager.Get<ChangeCurrency>().Execute();
    }

    public int GetCurrency()
    {
        return _currency;
    }
}
