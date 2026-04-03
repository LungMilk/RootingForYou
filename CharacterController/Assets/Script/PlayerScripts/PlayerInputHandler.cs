using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
public enum PlayerAction
{
    None,
    Plant,
    Remove,
}
[System.Serializable]
//class/struct difference is that a struct has copies of it and not the actual reference to the object.
public class PlayerActionSpriteLibrary
{
    public PlayerAction action;
    public Sprite sprite;
}
public class PlayerInputHandler : MonoBehaviour
{
    //because of the new input system we need something that every script can get so we create a global instance they can all reference
    public static PlayerInputHandler instance;
    public Canvas canvas;
    public PlayerInputs playerInputHandler { get; private set; }
    public PlayerAction leftMouseAction;
    public PlayerAction rightMouseAction;
    public RectTransform mousePos;
    //issue, planting is a variable sprite that I will need to change constantly.
    public List<PlayerActionSpriteLibrary> spriteLibraries;
    private Image currentCursor;
    //simple if that should always check if the lock state is locked or not.
    public bool IsCursorLocked => Cursor.lockState == CursorLockMode.Locked;

    public System.Action LeftClickPressed;
    public System.Action LeftClickReleased;

    public System.Action RightClickPressed;
    public System.Action RightClickReleased;

    public RectTransform refSheet;
    public bool isPaused = false;

    [Tooltip("Ref sheet is already hooked up this is incase we want something else to happen.")]
    public UnityEvent escape;
    private void Awake()
    {
        if (refSheet != null)
        {
            refSheet.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("NO REFERENCE SHEET FOUND FOR ON PAUSE");
        }


        if (instance == null)
        {
            instance = this;
        }
        currentCursor = mousePos.gameObject.GetComponent<Image>();

        playerInputHandler = new PlayerInputs();
        playerInputHandler.CharacterControls.Enable();

        var mouse = playerInputHandler.CharacterControls;
        mouse.LeftClick.started += _ => LeftClickPressed?.Invoke();
        mouse.LeftClick.started += OnLeftDown;
        mouse.LeftClick.canceled += _ => LeftClickReleased?.Invoke();
        mouse.LeftClick.canceled += OnLeftUp;

        mouse.RightClick.started += _ => RightClickPressed?.Invoke();
        mouse.RightClick.started += OnRightDown;
        mouse.RightClick.canceled += _ => RightClickReleased?.Invoke();
        mouse.RightClick.canceled += OnRightUp;

        mouse.Pause.started += _ => OnEscapePressed();
        SetCursorSprite(spriteLibraries.Find(x => x.action == PlayerAction.None).sprite);
    }

    public void OnEscapePressed()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
            escape?.Invoke();
        }

        if(refSheet != null)
        {
            refSheet.gameObject.SetActive(isPaused);
        }
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
        print($"Mouse button:{mouse}->{action}");
    }

    public void SetMousePlant()
    {
        SetCursorSprite(spriteLibraries.Find(x => x.action == PlayerAction.Plant).sprite);
        SetMouseAction(PlayerAction.Plant, 0);
        SetMouseAction(PlayerAction.Remove, 1);
    }

    public void SetMouseRemove()
    {
        SetCursorSprite(spriteLibraries.Find(x => x.action == PlayerAction.Remove).sprite);
        SetMouseAction(PlayerAction.Remove, 0);
        SetMouseAction(PlayerAction.Plant, 1);
    }
    public void Update()
    {
        mousePos.gameObject.SetActive(!IsCursorLocked);
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(mousePos.parent as RectTransform,
            Input.mousePosition, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera, out localPoint);
        mousePos.position = Input.mousePosition;
    }

    public void SetCursorSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            print("Cursor recieved null spirte");
            return;
        }
        currentCursor.sprite = sprite;
    }

    //we are missing the moment to moment input presses of switching between the different actions and the like, for example, pressing the right key still switches to delete and the like.
    //this si a stupid way to do it as I could just set the different sprites to be things and swithc between them but meh

    //also simple rotation of the sprite because it is not that hard
    public void OnLeftDown(InputAction.CallbackContext context)
    {
        //print("Left mouse");
        SetCursorSprite(spriteLibraries.Find(x => x.action == leftMouseAction).sprite);

        mousePos.eulerAngles = new Vector3(0, 0, 45);
    }

    public void OnLeftUp(InputAction.CallbackContext context)
    {
        mousePos.eulerAngles = new Vector3(0, 0, 0);
    }

    public void OnRightDown(InputAction.CallbackContext context)
    {
        //print("Right mouse");
        SetCursorSprite(spriteLibraries.Find(x => x.action == rightMouseAction).sprite);

        mousePos.eulerAngles = new Vector3(0, 0, 45);
    }
    public void OnRightUp(InputAction.CallbackContext context)
    {
        mousePos.eulerAngles = new Vector3(0, 0, 0);
    }

    private void OnDestroy()
    {
        //playerInputHandler.CharacterControls.Disable();
    }

}
