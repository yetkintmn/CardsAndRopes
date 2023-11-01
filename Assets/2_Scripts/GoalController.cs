using DG.Tweening;
using TMN;
using UnityEngine.SceneManagement;

public class GoalController : MonoSingleton<GoalController>
{
    private int _goalLimit;
    public int GoalCount { get; private set; }

    private void OnEnable()
    {
        _goalLimit = GameDataManager.Instance.GiftGoalLimit;
        GoalCount = 0;

        EventManager.Get<CollectGift>().AddListener(CollectGift);
        EventManager.Get<AllCardsDisabled>().AddListener(ControlGoal);
    }

    private void OnDisable()
    {
        EventManager.Get<CollectGift>().RemoveListener(CollectGift);
        EventManager.Get<AllCardsDisabled>().RemoveListener(ControlGoal);
    }

    private void CollectGift()
    {
        GoalCount++;
    }

    private bool IsReachedGoal()
    {
        return GoalCount >= _goalLimit;
    }

    private void ControlGoal()
    {
        if (IsReachedGoal())
        {
            LevelManager.Instance.LevelUp();
            SaveManager.Instance.SaveData();
        }

        DOVirtual.DelayedCall(1f, () => SceneManager.LoadScene(0));
    }
}
