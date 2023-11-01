using DG.Tweening;
using TMN;
using TMN.PoolManager;
using UnityEngine;

public class Card : MonoBehaviour, IMoveable, IMergeable
{
    private bool _canMove;

    private MeshFilter _meshFilter;

    private Slot _slot;
    public Slot StandOnSlot { get { return _slot; } set { _slot = value; } }

    private int _level;
    public int Level { get { return _level; } set { _level = value; } }

    private Pools.Types _type = Pools.Types.Card;
    public Pools.Types Type { get { return _type; } set { _type = value; } }

    private void Awake()
    {
        _meshFilter = GetComponentInChildren<MeshFilter>();
    }

    private void OnEnable()
    {
        ResetMesh();
        ResetLevel();

        EventManager.Get<ThrowCard>().AddListener(Throw);
    }

    private void OnDisable()
    {
        EventManager.Get<ThrowCard>().RemoveListener(Throw);
    }

    private void Update()
    {
        if (_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 14), Time.deltaTime * 2.5f);
            transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime * 10);
        }
    }

    private void Throw()
    {
        _canMove = true;
    }

    private void TurnBack()
    {
        _canMove = false;
        transform.DOMoveZ(transform.position.z - 1, 1f).OnComplete(Throw);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
            TurnBack();
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
}
