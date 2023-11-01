using System;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [field: SerializeField] public int RopeLevel { get; private set; }

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetRopeLevel(int ropeDifficulty, Color color)
    {
        RopeLevel = ropeDifficulty;
        _meshRenderer.material.color = color;
    }
}
