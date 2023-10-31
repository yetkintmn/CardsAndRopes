using System.Collections.Generic;
using TMN;
using TMN.PoolManager;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<Slot> slotList;

    private void OnEnable()
    {
        EventManager.Get<BuyCard>().AddListener(SpawnCard);
    }

    private void OnDisable()
    {
        EventManager.Get<BuyCard>().RemoveListener(SpawnCard);
    }

    private void SpawnCard()
    {
        Debug.Log("SpawnCard");
        Slot emptySlot = null;
        foreach (Slot slot in slotList)
        {
            if (!slot.IsFull)
            {
                emptySlot = slot;
                break;
            }
        }

        if (emptySlot == null)
            return;
        var newCard = PoolManager.Instance.Spawn(Pools.Types.Card);
        newCard.transform.position = emptySlot.StandV3;
        var moveable = newCard.GetComponent<IMoveable>();
        emptySlot.FillSlot(moveable);
    }

    private void SpawnChest()
    {
        Slot emptySlot = null;
        foreach (Slot slot in slotList)
        {
            if (slot.IsFull)
            {
                emptySlot = slot;
                break;
            }
        }

        if (emptySlot == null)
            return;
        var newChest = PoolManager.Instance.Spawn(Pools.Types.Chest);
        newChest.transform.position = emptySlot.StandV3;
        emptySlot.FillSlot(newChest.GetComponent<IMoveable>());
    }
}
