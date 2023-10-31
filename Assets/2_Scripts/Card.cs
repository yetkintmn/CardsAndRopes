using DG.Tweening;
using TMN;
using UnityEngine;

public class Card : MonoBehaviour, IMoveable
{
    private bool _canMove;

    private MeshFilter _meshFilter;

    private Slot _slot;
    public Slot StandOnSlot { get { return _slot; } set { _slot = value; } }

    private void Awake()
    {
        _meshFilter = GetComponentInChildren<MeshFilter>();
    }

    private void OnEnable()
    {
        ResetMesh();

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

    [ContextMenu("Change Mesh")]
    public void ChangeMesh()
    {
        _meshFilter.mesh = MergeManager.Instance.CardMeshList[Random.Range(0, 7)];
    }

    private void ResetMesh()
    {
        _meshFilter.mesh = MergeManager.Instance.CardMeshList[0];
    }
}
