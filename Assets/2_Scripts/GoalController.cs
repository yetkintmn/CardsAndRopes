using DG.Tweening;
using TMN;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    private int _goalLimit;
    private int _goalCount;

    private void OnEnable()
    {
        _goalLimit = GameDataManager.Instance.GiftGoalLimit;
        _goalCount = 0;

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
        _goalCount++;
        EventManager.Get<GiftCollected>().Execute(_goalCount);
    }

    private bool IsReachedGoal()
    {
        return _goalCount >= _goalLimit;
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
