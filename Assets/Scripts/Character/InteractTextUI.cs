using TMPro;
using UnityEngine;

public class InteractTextUI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI interactWithText;

    public void ShowInteractText(string interactableText)
    {
        background.SetActive(true);
        interactWithText.text = interactableText;
    }

    public void HideInteractText()
    {
        background.SetActive(false);
    }
}
