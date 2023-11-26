using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Inventory/Item")]
public class ItemSO : ScriptableObject, IComparable<ItemSO>
{
    public string ItemName = "New Item";
    public Sprite ItemIcon;
    public int ItemPrice;
    public EquipLocation EquipLocation;

    public int CompareTo(ItemSO other)
    {
        return ItemName.CompareTo(other.ItemName);
    }
}