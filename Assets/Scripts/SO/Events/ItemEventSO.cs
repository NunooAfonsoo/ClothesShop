using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemEvent", menuName = "ScriptableObjects/Events/ItemEventSO")]
public class ItemEventSO : ScriptableObject
{
    public event Action<ItemSO> OnEventRaised;

    public void RaiseEvent(ItemSO item)
    {
        OnEventRaised?.Invoke(item);
    }
}