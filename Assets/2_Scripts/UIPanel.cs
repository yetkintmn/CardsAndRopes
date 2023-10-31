using TMN;
using TMN.PoolManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoSingleton<UIPanel>
{
    [SerializeField] private Button buyCardBtn;
    [SerializeField] private Button throwCardBtn;

    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private TextMeshProUGUI goalTxt;
    [SerializeField] private TextMeshProUGUI levelTxt;

    private void OnEnable()
    {
        buyCardBtn.onClick.AddListener(BuyCard);
        throwCardBtn.onClick.AddListener(ThrowCard);

        EventManager.Get<ChangeCurrency>().AddListener(ChangeCurrency);
    }

    private void OnDisable()
    {
        buyCardBtn.onClick.RemoveListener(BuyCard);
        throwCardBtn.onClick.RemoveListener(ThrowCard);

        EventManager.Get<ChangeCurrency>().RemoveListener(ChangeCurrency);
    }

    private void BuyCard()
    {
        EventManager.Get<BuyCard>().Execute();
    }

    private void ThrowCard()
    {
        EventManager.Get<ThrowCard>().Execute();
    }

    private void ChangeCurrency()
    {
        var currency = CurrencyManager.Instance.GetCurrency();
        buyCardBtn.interactable = currency >= GameDataManager.Instance.CardPrice;
        moneyTxt.text = currency.ToString();
    }
}