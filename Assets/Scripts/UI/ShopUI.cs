using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BuildingUI
{
    [SerializeField] private Shop shop;

    [SerializeField] private Button buy;
    [SerializeField] private Button sell;

    [SerializeField] private GameObject buyTab;
    [SerializeField] private GameObject sellTab;

    [SerializeField] private Transform sellTabContent;

    [SerializeField] private IntEventSO itemBoughtEvent;
    [SerializeField] private IntEventSO itemSoldEvent;

    protected override void Awake()
    {
        base.Awake();

        buy.onClick.AddListener(OpenBuyTab);
        sell.onClick.AddListener(OpenSellTab);
    }

    private void OpenBuyTab()
    {
        buy.interactable = false;
        sell.interactable = true;

        buyTab.SetActive(true);
        sellTab.SetActive(false);

        SetUpShopInventory();
    }

    private void OpenSellTab()
    {
        sell.interactable = false;
        buy.interactable = true;

        sellTab.SetActive(true);
        buyTab.SetActive(false);

        SetUpPlayerInventory();
    }

    /// <summary>
    /// Sets up the ui for the player inventory and adds listeners to the items' buttons
    /// </summary>
    private void SetUpPlayerInventory()
    {
        foreach (Transform child in sellTabContent)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemSO itemSO in Character.Instance.Inventory.Items)
        {
            Item item = new Item(itemSO, Character.Instance.Inventory);

            IItemUI itemUI = Instantiate(itemPrefab, sellTabContent).GetComponent<IItemUI>();
            itemUI.SetUp(item, Constants.SELL, false);

            itemUI.ItemAction.interactable = !Character.Instance.IsWearingItem(itemSO);

            itemUI.ItemAction.onClick.AddListener(() => ItemSold(itemUI, itemSO));
        }
    }

    private void ItemSold(IItemUI itemUI, ItemSO itemSO)
    {
        shop.Inventory.AddItem(itemSO);
        Character.Instance.Inventory.RemoveItem(itemSO);
        int moneyGain = (int)(itemSO.ItemPrice * 0.80f);
        itemSoldEvent.RaiseEvent(moneyGain);

        SetUpPlayerInventory();
    }

    protected override void ShowBuildingUI()
    {
        OpenBuyTab();

        base.ShowBuildingUI();
    }

    /// <summary>
    /// Sets up the ui for the shop inventory and adds listeners to the items' buttons
    /// </summary>
    private void SetUpShopInventory()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemSO itemSO in shop.Inventory.Items)
        {
            Item item = new Item(itemSO, shop.Inventory);

            IItemUI itemUI = Instantiate(itemPrefab, content).GetComponent<IItemUI>();

            itemUI.SetUp(item, Constants.BUY_AND_EQUIP, true);
            itemUI.ItemAction.interactable = EconomyManager.Instance.Money >= itemSO.ItemPrice;
            itemUI.ItemAction.onClick.AddListener(() => ItemBoughAndEquipped(itemUI, itemSO));
        }
    }

    private void ItemBoughAndEquipped(IItemUI itemUI, ItemSO itemSO)
    {
        shop.Inventory.RemoveItem(itemSO);
        Character.Instance.Inventory.AddItem(itemSO);
        itemBoughtEvent.RaiseEvent(-itemSO.ItemPrice);

        Character.Instance.EquipItem(itemSO, itemSO.EquipLocation);

        SetUpShopInventory();
    }
}
