using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightSeed : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform seedTransform;
    public GameObject seedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedObject = this.transform.GetChild(0).gameObject;
        seedTransform = seedObject.GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("Mouse is over the seed.");
        seedTransform.anchoredPosition = new Vector2(0, 100);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        seedTransform.anchoredPosition = new Vector2(0, 0);
       // Debug.Log("Mouse exited the seed.");
    }
}
