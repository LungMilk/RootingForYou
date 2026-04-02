using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

public class HighlightShovel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject shovelObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shovelObject = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Mouse is over the seed.");
        shovelObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 120, 0);
    }

    private void OnMouseExit()
    {
        shovelObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse is over the seed.");
        shovelObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 120, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shovelObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }
}
