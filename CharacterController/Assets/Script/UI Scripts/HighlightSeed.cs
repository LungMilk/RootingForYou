using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightSeed : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public PlayerStateMachine playerStateMachine;

    public PlantObjectSO plantObject;

    public BarManager barManager;

    public GameObject seedObject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedObject = this.transform.GetChild(0).gameObject;
        seedObject.GetComponent<Image>().sprite = plantObject.displayImage;
        seedObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse is over the seed.");
        
        barManager.previewBeauty = plantObject.beauty;
        barManager.previewCalmness = plantObject.calmness;
        barManager.previewPassion = plantObject.passion;
        seedObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exited the seed.");
        barManager.previewBeauty = 0;
        barManager.previewCalmness = 0;
        barManager.previewPassion = 0;
        seedObject.SetActive(false);
    }

    public void SetPlayerSeed(PlantObjectSO selectedPlant)
    {
        playerStateMachine._selectedPlantObject = selectedPlant;
    }
}
