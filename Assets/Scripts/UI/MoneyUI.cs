using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private IntEventSO moneyChangedEvent;

    private void Awake()
    {
        moneyChangedEvent.OnEventRaised += MoneyChanged;
    }

    private void MoneyChanged(int money)
    {
        moneyText.text = money.ToString();
    }
}
