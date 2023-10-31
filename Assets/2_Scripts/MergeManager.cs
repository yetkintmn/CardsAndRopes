using System.Collections.Generic;
using UnityEngine;

public class MergeManager : Singleton<MergeManager>
{
    [field: SerializeField] public List<Mesh> CardMeshList { get; private set; }
    [field: SerializeField] public List<Mesh> ChestMeshList { get; private set; }

}
