using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRopes : MonoBehaviour
{
    [SerializeField] private List<Rope> ropeList;

    [SerializeField] private List<Color> ropeLevelColorList;
    
    private void Start()
    {
        var ropeLevel = LevelManager.Instance.Level - 1;
        foreach (var rope in ropeList)
            rope.SetRopeLevel(ropeLevel, ropeLevelColorList[ropeLevel]);
        ropeLevel++;
        for (var i = 0; i < 3; i++)
            ropeList[Random.Range(0, ropeList.Count)].SetRopeLevel(ropeLevel, ropeLevelColorList[ropeLevel]);
    }
}
