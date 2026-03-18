using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class HighlightSeed : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public PlayerStateMachine playerStateMachine;

    public PlantObjectSO plantObject;

    public BarManager barManager;

    public GameObject seedObject;

    public UnityEvent OnMouseOver;
    public UnityEvent OnMouseOut; 


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
        Dictionary<PlantAttribute,int> previews = new Dictionary<PlantAttribute, int>
        {
            {PlantAttribute.Beauty, plantObject.beauty },
            {PlantAttribute.Passion, plantObject.passion},
            {PlantAttribute.Calmness, plantObject.calmness },
        };
        OnMouseOver.Invoke();

        barManager.SetPreviewValues(previews);
        seedObject.SetActive(true);
        //print($"{name}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exited the seed.");
        Dictionary<PlantAttribute, int> previews = new Dictionary<PlantAttribute, int>
        {
            {PlantAttribute.Beauty, 0},
            {PlantAttribute.Passion, 0},
            {PlantAttribute.Calmness, 0 },
        };
        OnMouseOut.Invoke();

        barManager.SetPreviewValues(previews);
        seedObject.SetActive(false);
    }

    public void SetPlayerSeed(PlantObjectSO selectedPlant)
    {
        //this resets our mouse keys to be mouse 0 -> plant.

        var list = PlayerInputHandler.instance.spriteLibraries;
        //
        int index = list.FindIndex(x => x.action == PlayerAction.Plant);

        if (index != -1)
        {
            var item = list[index];
            item.sprite = selectedPlant.displayImage;
            list[index] = item;
        }
        PlayerInputHandler.instance.SetMousePlant();
        playerStateMachine._selectedPlantObject = selectedPlant;
    }
}
