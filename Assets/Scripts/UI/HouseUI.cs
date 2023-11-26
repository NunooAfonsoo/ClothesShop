using UnityEngine;

public class HouseUI : BuildingUI
{
    private void SetUpPlayerInventory()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemSO itemSO in Character.Instance.Inventory.Items)
        {
            Item item = new Item(itemSO, Character.Instance.Inventory);

            IItemUI itemUI = Instantiate(itemPrefab, content).GetComponent<IItemUI>();

            bool isCharacterWearingItem = Character.Instance.IsWearingItem(itemSO);
            string buttonText = isCharacterWearingItem ? Constants.UNEQUIP : Constants.EQUIP;

            itemUI.SetUp(item, buttonText, false);

            if (isCharacterWearingItem)
            {
                itemUI.ItemAction.onClick.AddListener(() => UnequipItem(itemUI, itemSO));
            }
            else
            {
                itemUI.ItemAction.onClick.AddListener(() => EquipItem(itemUI, itemSO));
            }
        }
    }

    private void UnequipItem(IItemUI itemUI, ItemSO itemSO)
    {
        Character.Instance.EquipDefaultItem(itemSO.EquipLocation);
        itemUI.ChangeButtonText(Constants.EQUIP);

        itemUI.ItemAction.onClick.RemoveAllListeners();
        itemUI.ItemAction.onClick.AddListener(() => EquipItem(itemUI, itemSO));
    }

    private void EquipItem(IItemUI itemUI, ItemSO itemSO)
    {
        Character.Instance.EquipItem(itemSO, itemSO.EquipLocation);
        itemUI.ChangeButtonText(Constants.UNEQUIP);

        itemUI.ItemAction.onClick.RemoveAllListeners();
        itemUI.ItemAction.onClick.AddListener(() => UnequipItem(itemUI, itemSO));

        foreach (Transform child in content)
        {
            IItemUI childItemUI = child.GetComponent<IItemUI>();
            if (childItemUI != itemUI && childItemUI.Item.ItemSO.EquipLocation == itemUI.Item.ItemSO.EquipLocation)
            {
                childItemUI.ItemAction.onClick.RemoveAllListeners();
                childItemUI.ItemAction.onClick.AddListener(() => EquipItem(childItemUI, childItemUI.Item.ItemSO));
                childItemUI.ChangeButtonText(Constants.EQUIP);
            }
        }
    }

    protected override void ShowBuildingUI()
    {
        SetUpPlayerInventory();

        base.ShowBuildingUI();
    }
}
