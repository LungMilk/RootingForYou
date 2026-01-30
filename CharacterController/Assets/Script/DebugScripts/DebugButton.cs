using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class DebugButton : MonoBehaviour ,IInteractable, IActionCall
{
    bool hasFired = false;
    [SerializeField] private List<GameObject> targets;

    public InteractState _interactType;
    public InteractState InteractableType
    {
        get => _interactType;
        set => _interactType = value;
    }
    public bool isPressed;
    [ContextMenu("Interact")]
    public void Interact()
    {
        isPressed = !isPressed;

        if (targets != null)
        {
            CallAction();
        }
        //print("I was interacted with!");
    }
    public void CallAction()
    {
        //if (hasFired) return;

        foreach (var target in targets)
        {
            if (target.TryGetComponent<IActionCall>(out var action))
            {
                action.CallAction();
            }
            else
            {
                print(target.name + "Does not have IActionCallInterface");
            }
        }
    }
}
