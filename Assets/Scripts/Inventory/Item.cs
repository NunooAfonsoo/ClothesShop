using System;
using UnityEngine;

[Serializable]
public class Item
{
    public ItemSO ItemSO { get; private set; }
    public Inventory Inventory { get; private set; }

    [SerializeField] private ItemEventSO itemCreatedEvent;
    [SerializeField] private ItemEventSO itemEquippedEvent;

    public Item(ItemSO itemSO, Inventory inventory)
    {
        ItemSO = itemSO;
        Inventory = inventory;
    }
}
