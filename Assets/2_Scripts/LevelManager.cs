public class LevelManager : Singleton<LevelManager>
{
    public int Level { get; private set; }

    public void SetLevel(int level)
    {
        if (SaveManager.Instance.IsFirstPlay)
        {
            Level = 1;
            return;
        }
        Level = level;
    }

    public void LevelUp()
    {
        Level ++;
    }
}
