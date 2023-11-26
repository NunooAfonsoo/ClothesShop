using TMPro;
using UnityEngine.UI;

public interface IItemUI
{
    Button ItemAction { get; }
    TextMeshProUGUI ButtonText { get; }
    Item Item { get; }

    void SetUp(Item item, string buttonText, bool showPrice);
    void ChangeButtonText(string buttonText);
}
