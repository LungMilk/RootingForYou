using Unity.VisualScripting;
using UnityEngine;
//[System.Serializable]
public interface IActionCall
{
    //call action might be vague but this is ideally used for "ingame button functionality" for now at least
    bool Called
    {
        get => false; 
        set { } 
    }
    void CallAction();
}
