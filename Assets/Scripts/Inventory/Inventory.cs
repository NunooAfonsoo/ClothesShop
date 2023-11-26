using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public List<ItemSO> Items { get; private set; }

    public Inventory()
    {
        Items = new List<ItemSO>();
    }

    public void SetItems(List<ItemSO> items)
    {
        this.Items = items;
    }

    public void AddItem(ItemSO item)
    {
        Items.Add(item);

        Items.Sort();
    }

    public void RemoveItem(ItemSO item)
    {
        Items.Remove(item);
    }
}