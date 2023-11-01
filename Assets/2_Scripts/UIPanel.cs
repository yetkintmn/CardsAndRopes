using TMN;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private Button buyCardBtn;
    [SerializeField] private Button throwCardBtn;

    [SerializeField] private TextMeshProUGUI buyCardTxt;
    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private TextMeshProUGUI goalTxt;
    [SerializeField] private TextMeshProUGUI levelTxt;

    private void OnEnable()
    {
        buyCardBtn.onClick.AddListener(BuyCard);
        throwCardBtn.onClick.AddListener(ThrowCard);

        EventManager.Get<ChangeCurrency>().AddListener(SetCurrencyText);
        EventManager.Get<GiftCollected>().AddListener(SetGoalText);
    }

    private void OnDisable()
    {
        buyCardBtn.onClick.RemoveListener(BuyCard);
        throwCardBtn.onClick.RemoveListener(ThrowCard);

        EventManager.Get<ChangeCurrency>().RemoveListener(SetCurrencyText);
        EventManager.Get<GiftCollected>().RemoveListener(SetGoalText);
    }

    private void Start()
    {
        SetCurrencyText();
        SetGoalText();
        SetLevelText();
    }

    private void BuyCard()
    {
        EventManager.Get<BuyCard>().Execute(GameDataManager.Instance.CardPrice);
    }

    private void ThrowCard()
    {
        EventManager.Get<ThrowCard>().Execute();
    }

    private void SetCurrencyText()
    {
        var currency = CurrencyManager.Instance.GetCurrency();
        buyCardBtn.interactable = currency >= GameDataManager.Instance.CardPrice;
        moneyTxt.text = currency.ToString();
    }

    private void SetGoalText(int goalCount = 0)
    {
        goalTxt.text = goalCount + "/" + GameDataManager.Instance.GiftGoalLimit;
    }

    private void SetLevelText()
    {
        levelTxt.text = "Level " + LevelManager.Instance.Level;
    }
}