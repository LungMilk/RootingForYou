using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// For all tools probably I just typed shovel because its on my mind
/// </summary>
public class ShovelButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public RectTransform button;
    public UnityEvent OnClick;
    public void OnPointerDown(PointerEventData eventData)
    {
        //print("Pressed me");
        OnClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("Over shovel");
    }
}
