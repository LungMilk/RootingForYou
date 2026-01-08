using UnityEngine;

public class DebugButton : MonoBehaviour ,IInteractable
{
    public bool isPressed;
    public void Interact()
    {
        isPressed = !isPressed;
        print("I was interacted with!");
    }
}
