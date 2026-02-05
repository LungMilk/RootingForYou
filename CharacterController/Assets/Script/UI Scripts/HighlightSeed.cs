using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightSeed : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform seedTransform;
    public GameObject seedObject;
    public PlayerStateMachine playerStateMachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedObject = this.transform.GetChild(0).gameObject;
        seedTransform = seedObject.GetComponent<RectTransform>();
    }

    public void SeedSelected()
    {
        seedTransform.anchoredPosition = new Vector2(0, 150);
        seedTransform.localScale = new Vector2(1.5f, 1.5f);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       ///Debug.Log("Mouse is over the seed.");
        seedTransform.anchoredPosition = new Vector2(0, 100);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        seedTransform.anchoredPosition = new Vector2(0, 0);
       //Debug.Log("Mouse exited the seed.");
    }

    public void SetPlayerSeed(PlantObjectSO selectedPlant)
    {
        
        playerStateMachine._selectedPlantObject = selectedPlant;
    }
}
