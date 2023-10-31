using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    [field: SerializeField] public int CardPrice { get; private set; }
    [field: SerializeField] public int PrizeGoalLimit { get; private set; }
}
