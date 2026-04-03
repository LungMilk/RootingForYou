using UnityEngine;

public class ToolTipSpeaker : MonoBehaviour
{
    [SerializeField]
    private RectTransform textAnchor;
    [TextArea]
    public string text;
    public ToolTip toolTip;
    [ContextMenu("SpawnBubble")]
    public void CreateBubble()
    {
        toolTip = ToolTipManager.Instance.Get(text, textAnchor);
    }

    /// <summary>
    /// Create a bubble from the speaker
    /// </summary>
    /// <param name="text">text you want to display</param>
    public void CreateBubble(string text)
    {
        if (toolTip != null) { return; }
        toolTip = ToolTipManager.Instance.Get(text, textAnchor);
    }

    /// <summary>
    /// dismiss a bubble controlled by the speaker
    /// </summary>
    /// <para>
    /// <see cref="ToolTipSpeaker"/> contains its own reference to a bubble, calling this will simply dismiss the bubble.
    /// </para>
    public void DismissBubble()
    {
        if (toolTip == null) { print($"{name}: cannot find a ToolTip"); return; }
        ToolTipManager.Instance.Release(toolTip);
    }

    /// <summary>
    /// Sets the speakers variable of string that it defaults to displaying.
    /// </summary>
    /// <param name="givenText"></param>
    public void SetBubbleText(string givenText)
    {
        text = givenText;
    }

    /// <summary>
    /// If bubble is active, bubble turns off, if bubble is not active we create a bubble based on the <see cref="text"/> variable of the speaker
    /// </summary>
    /// <para>
    /// Use if you just want to turn it off and on again with the text being set by something else, inspector and likewise
    /// </para>
    public void SwitchBubbleState()
    {
        if (toolTip == null)
        {
            CreateBubble();
        }
        if (toolTip != null) { DismissBubble(); }
    }

    /// <summary>
    /// If bubble is active we dismiss it, else we create one.
    /// </summary>
    /// <param name="text"> Genereates a bubble based off of a given string</param>
    /// <para>
    /// Use if you want to specify or change the bubble state.
    /// </para>
    public void SwitchBubbleState(string text)
    {
        if (toolTip == null)
        {
            CreateBubble(text);
        }
        if (toolTip != null) { DismissBubble(); }
    }
}
