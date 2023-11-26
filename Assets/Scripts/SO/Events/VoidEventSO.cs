using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntEvent", menuName = "ScriptableObjects/Events/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}