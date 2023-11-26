using System.Collections.Generic;
using UnityEngine;

public class Shop : Building
{
    public Inventory Inventory { get; private set; }

    [SerializeField] private List<ItemSO> items;

    private void Awake()
    {
        Inventory = new Inventory();
        Inventory.SetItems(items);
    }

    public void ItemSold(ItemSO item)
    {
        Character.Instance.Inventory.RemoveItem(item);
        Inventory.AddItem(item);
    }

    public void ItemBought(ItemSO item)
    {
        Inventory.RemoveItem(item);
        Character.Instance.Inventory.AddItem(item);
    }

    public override string GetInteractableText()
    {
        return "Press E to Enter the Shop";
    }
}
