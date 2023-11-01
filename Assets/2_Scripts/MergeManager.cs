using System.Collections.Generic;
using TMN.PoolManager;
using UnityEngine;

public class MergeManager : Singleton<MergeManager>
{
    [field: SerializeField] public List<Mesh> CardMeshList { get; private set; }
    [field: SerializeField] public List<Mesh> ChestMeshList { get; private set; }

    public bool CanMerge(IMergeable mergeable1, IMergeable mergeable2)
    {
        if (mergeable1.Type == mergeable2.Type && mergeable1.Level == mergeable2.Level)
        {
            Merge(mergeable1);
            return true;
        }
        return false;
    }

    private void Merge(IMergeable mergeable)
    {
        mergeable.Level++;
        switch (mergeable.Type)
        {
            case Pools.Types.Card:
                mergeable.ChangeMesh(CardMeshList[mergeable.Level]);
                break;
            case Pools.Types.Chest:
                mergeable.ChangeMesh(ChestMeshList[mergeable.Level]);
                break;
            default:
                break;
        }
    }
}
