using UnityEngine;

public abstract class Building : MonoBehaviour, IInteractable
{
    [SerializeField] private Color interactColor;
    [SerializeField] private Color normalColor;

    [SerializeField] protected VoidEventSO interactedWithBuildingEvent;

    public virtual void Interact()
    {
        interactedWithBuildingEvent.RaiseEvent();
    }

    public virtual string GetInteractableText()
    {
        return "Building";
    }
}