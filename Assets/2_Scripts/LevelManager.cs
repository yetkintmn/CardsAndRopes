public class LevelManager : Singleton<LevelManager>
{
    private int _level;

    public void SetLevel(int level)
    {
        if (SaveManager.Instance.IsFirstPlay)
        {
            _level = 1;
            return;
        }
        _level = level;
    }

    public int GetLevel()
    {
        return _level;
    }

    public void LevelUp()
    {
        _level ++;
    }
}
