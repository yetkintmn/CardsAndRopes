using System.Collections;
using System.Collections.Generic;
using TMN;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private int _level;

    public void SetLevel(int level)
    {
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
