using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightSeed : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public PlayerStateMachine playerStateMachine;

    public PlantObjectSO plantObject;

    public GameObject barManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse is over the seed.");
        barManager.GetComponent<BarManager>().previewBeauty = plantObject.beauty;
        barManager.GetComponent<BarManager>().previewCalmness = plantObject.calmness;
        barManager.GetComponent<BarManager>().previewPassion = plantObject.passion;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exited the seed.");
        barManager.GetComponent<BarManager>().previewBeauty = 0;
        barManager.GetComponent<BarManager>().previewCalmness = 0;
        barManager.GetComponent<BarManager>().previewPassion = 0;
    }

    public void SetPlayerSeed(PlantObjectSO selectedPlant)
    {
        playerStateMachine._selectedPlantObject = selectedPlant;
    }
}
