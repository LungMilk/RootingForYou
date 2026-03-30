using UnityEngine;
using TMPro;
public class InteractSignifierUI : MonoBehaviour
{
    [TextArea]
    public string interactText;
    [TextArea]
    public string exitText;

    public TextMeshProUGUI uiText;

    public bool isInteracting;
    public void Start()
    {
        InteractText();
        Show(false);
    }
    public void SwitchText()
    {
        if (isInteracting)
        {
            ExitText();
        }
        else
        {
            InteractText();
        }
    }
    public void InteractText()
    {
        uiText.text = interactText;
    }
    public void ExitText()
    {
        uiText.text = exitText;
    }
    public void Show(bool state)
    {
        this.gameObject.SetActive(state);
    }
}
