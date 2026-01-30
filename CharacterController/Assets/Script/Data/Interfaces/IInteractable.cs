using UnityEngine;

//[System.Serializable]
public interface IInteractable 
{
    public InteractState InteractableType { get; set; }
    void Interact();
}
