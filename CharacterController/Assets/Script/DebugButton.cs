using UnityEngine;

public class DebugButton : MonoBehaviour ,IInteractable
{
    public InteractState _interactType;
    public InteractState InteractableType
    {
        get => _interactType;
        set => _interactType = value;
    }
    public bool isPressed;
    public void Interact()
    {
        isPressed = !isPressed;
        //print("I was interacted with!");
    }
}
