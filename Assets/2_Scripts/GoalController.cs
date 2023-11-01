using TMN;

public class GoalController : MonoSingleton<GoalController>
{
    private int _goalLimit;
    public int GoalCount { get; private set; }

    private void OnEnable()
    {
        _goalLimit = GameDataManager.Instance.GiftGoalLimit;
        GoalCount = 0;

        EventManager.Get<CollectGift>().AddListener(CollectGift);
    }

    private void OnDisable()
    {
        EventManager.Get<CollectGift>().RemoveListener(CollectGift);
    }

    private void CollectGift()
    {
        GoalCount++;
        LevelManager.Instance.LevelUp();
    }

    public bool IsReachedGoal()
    {
        return GoalCount >= _goalLimit;
    }
}
