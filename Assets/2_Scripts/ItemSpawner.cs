using System.Collections.Generic;
using TMN;
using TMN.PoolManager;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<Slot> slotList;

    private List<Card> _spawnedCardList = new List<Card>();

    private int _disabledCardCount;

    private void OnEnable()
    {
        EventManager.Get<BuyCard>().AddListener(SpawnCard);
        EventManager.Get<CardDisabled>().AddListener(CardDisabled);
    }

    private void OnDisable()
    {
        EventManager.Get<BuyCard>().RemoveListener(SpawnCard);
        EventManager.Get<CardDisabled>().RemoveListener(CardDisabled);
    }

    private void SpawnCard(int currency)
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
        _spawnedCardList.Add(newCard.GetComponent<Card>());
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

    private void CardDisabled(Card card, bool merged)
    {
        if (merged)
            _spawnedCardList.Remove(card);
        else
            _disabledCardCount++;

        Debug.Log(_disabledCardCount);
        if (_disabledCardCount >= _spawnedCardList.Count)
            EventManager.Get<AllCardsDisabled>().Execute();
    }
}
