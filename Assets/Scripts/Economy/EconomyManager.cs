using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    [SerializeField] private IntEventSO moneyGainedEvent;
    [SerializeField] private IntEventSO moneySpentEvent;
    [SerializeField] private IntEventSO moneyChangedEvent;

    public int Money { get; private set; }

    private void Awake()
    {
        Instance = this;

        moneyGainedEvent.OnEventRaised += ChangeMoney;
        moneySpentEvent.OnEventRaised += ChangeMoney;
    }

    public void ChangeMoney(int change)
    {
        Money += change;

        moneyChangedEvent.RaiseEvent(Money);
    }
}
