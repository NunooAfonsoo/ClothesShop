using UnityEngine;
using UnityEngine.UI;

public abstract class BuildingUI : MonoBehaviour
{
    [SerializeField] protected GameObject buildingUI;
    [SerializeField] protected Button exit;
    [SerializeField] protected GameObject itemPrefab;
    [SerializeField] protected Transform content;

    [SerializeField] protected VoidEventSO interactedWithBuildingEvent;

    protected virtual void Awake()
    {
        exit.onClick.AddListener(HideBuildingUI);

        interactedWithBuildingEvent.OnEventRaised += ShowBuildingUI;
    }

    private void HideBuildingUI()
    {
        buildingUI.SetActive(false);
    }

    protected virtual void ShowBuildingUI()
    {
        buildingUI.SetActive(true);
    }
}
