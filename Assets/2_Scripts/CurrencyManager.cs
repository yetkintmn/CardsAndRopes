using UnityEngine;
using TMN;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private int _currency;
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
