using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IItemUI
{

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemIcon;
    [field: SerializeField] public Button ItemAction { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ButtonText { get; private set; }

    public Item Item { get; private set; }

    public void SetUp(Item item, string buttonText, bool showPrice)
    {
        Item = item;
        ItemSO itemSO = item.ItemSO;

        itemName.text = showPrice ? itemSO.ItemName + " - " + itemSO.ItemPrice : itemSO.ItemName;
        itemIcon.sprite = itemSO.ItemIcon;

        ButtonText.text = buttonText;
    }

    public void ChangeButtonText(string buttonText)
    {
        ButtonText.text = buttonText;
    }
}
