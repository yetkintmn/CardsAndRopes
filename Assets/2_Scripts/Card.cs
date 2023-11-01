using DG.Tweening;
using System;
using TMN;
using TMN.PoolManager;
using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour, IMoveable, IMergeable
{
    private int _level;
    public int Level { get { return _level; } set { _level = value; } }

    private int _hitCount;

    private bool _canMove;

    private MeshFilter _meshFilter;

    private Slot _slot;
    public Slot StandOnSlot { get { return _slot; } set { _slot = value; } }

    private Pools.Types _type = Pools.Types.Card;
    public Pools.Types Type { get { return _type; } set { _type = value; } }

    private void Awake()
    {
        _meshFilter = GetComponentInChildren<MeshFilter>();
    }

    private void OnEnable()
    {
        InitialSetMesh();

        EventManager.Get<ThrowCard>().AddListener(Throw);
    }

    private void OnDisable()
    {
        _hitCount = 0;
        _canMove = false;

        ResetMesh();
        ResetLevel();
        ResetEuler();

        EventManager.Get<ThrowCard>().RemoveListener(Throw);
    }

    private void Update()
    {
        if (_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 14), Time.deltaTime * 4f);
            transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 300);
        }
    }

    private void Throw()
    {
        _canMove = true;
    }

    private void TurnBack()
    {
        if (_hitCount > 1)
        {
            DisableCard();
            return;
        }

        _hitCount++;
        _canMove = false;
        transform.DOMoveZ(transform.position.z - 0.8f, 0.8f).OnComplete(Throw);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            if (collision.gameObject.TryGetComponent<Rope>(out Rope rope))
                if (rope.RopeLevel <= _level)
                    rope.gameObject.SetActive(false);
                else
                    TurnBack();
            else
                TurnBack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gift"))
            CollectGift();
        else if (other.CompareTag("LastWall"))
            DisableCard();
    }

    private void CollectGift()
    {
        EventManager.Get<CollectGift>().Execute();
        DisableCard();
    }

    public void InitialSetMesh()
    {
        _meshFilter.mesh = MergeManager.Instance.CardMeshList[Level];
    }

    public void ChangeMesh(Mesh mesh)
    {
        _meshFilter.mesh = mesh;
    }

    public void ResetMesh()
    {
        _meshFilter.mesh = MergeManager.Instance.CardMeshList[0];
    }

    public void ResetLevel()
    {
        _level = 0;
    }

    private void ResetEuler()
    {
        transform.eulerAngles = Vector3.zero;
    }

    public void DisableForMerge()
    {
        DisableCard(true);
    }

    private void DisableCard(bool isMerged = false)
    {
        PoolManager.Instance.Despawn(Pools.Types.Card, gameObject);
        EventManager.Get<CardDisabled>().Execute(this, isMerged);
    }
}
