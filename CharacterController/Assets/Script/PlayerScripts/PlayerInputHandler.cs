using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    //because of the new input system we need something that every script can get so we create a global instance they can all reference

    public PlayerInputs playerInputHandler { get; private set; }

    private void Awake()
    {
        playerInputHandler = new PlayerInputs();
        playerInputHandler.CharacterControls.Enable();
    }

    private void OnDestroy()
    {
        //playerInputHandler.CharacterControls.Disable();
    }

}
