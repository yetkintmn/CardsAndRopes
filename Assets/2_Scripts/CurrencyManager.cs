using TMN;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private int _currency;
    public void SetCurrency(int currency)
    {
        if (SaveManager.Instance.IsFirstPlay)
        {
            _currency = 50;
            return;
        }
        _currency += currency;
        EventManager.Get<ChangeCurrency>().Execute();
    }

    public int GetCurrency()
    {
        return _currency;
    }
}
