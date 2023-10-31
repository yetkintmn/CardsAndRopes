using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool IsFull { get; private set; }
    public IMoveable Moveable { get; private set; }

    public Vector3 StandV3 { get; private set; }

    private void Start()
    {
        StandV3 = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    public void FillSlot(IMoveable moveable)
    {
        Moveable = moveable;
        moveable.StandOnSlot = this;
        IsFull = true;
    }

    public void SetEmptySlot()
    {
        Moveable = null;
        IsFull = false;
    }
}
