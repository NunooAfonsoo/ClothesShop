using UnityEngine;

public class MoneyLog : MonoBehaviour, IInteractable
{
    [SerializeField] private IntEventSO moneyGainedEvent;

    public string GetInteractableText()
    {
        return "Press E to Gain Money";
    }

    public void Interact()
    {
        moneyGainedEvent.RaiseEvent(Constants.MONEY_TO_GAIN);
    }
}
