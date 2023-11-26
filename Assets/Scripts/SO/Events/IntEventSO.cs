using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntEvent", menuName = "ScriptableObjects/Events/IntEventSO")]
public class IntEventSO : ScriptableObject
{
    public event Action<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}