using TMN.PoolManager;
using UnityEngine;

public interface IMergeable
{
    public Pools.Types Type { get; set; }
    public int Level { get; set; }
    public void InitialSetMesh();
    public void ChangeMesh(Mesh mesh);
    public void ResetMesh();
    public void ResetLevel();
    public void DisableForMerge();
}
