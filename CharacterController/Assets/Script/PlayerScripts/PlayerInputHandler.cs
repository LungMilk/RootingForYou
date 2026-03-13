using UnityEngine;
using UnityEngine.InputSystem;
public enum PlayerAction
{
    None,
    Plant,
    Remove,
}
public class PlayerInputHandler : MonoBehaviour
{
    //because of the new input system we need something that every script can get so we create a global instance they can all reference

    public PlayerInputs playerInputHandler { get; private set; }
    public PlayerAction leftMouseAction;
    public PlayerAction rightMouseAction;

    private void Awake()
    {
        playerInputHandler = new PlayerInputs();
        playerInputHandler.CharacterControls.Enable();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    /// <param name="mouse">0 = left mouse button, 1 = right mouse button, defaults to 0</param>
    public void SetMouseAction(PlayerAction action,int mouse = 0)
    {
        //yes an if is probably better I just wanted to use a switch as I rarely get the chance and want to see what it is like
        switch (mouse)
        {
            case 0: 
                leftMouseAction = action;
                break;
            case 1:
                rightMouseAction = action;
                break;
            default:
                leftMouseAction = action;
                break;
        }
    }

    private void OnDestroy()
    {
        //playerInputHandler.CharacterControls.Disable();
    }

}
