using UnityEngine;
/// <summary>
/// A component that will speak to the TextBubbleManager.Instance to create and dismiss the bubble.
/// </summary>
public class PopUpSpeaker : MonoBehaviour
{
    [SerializeField]
    private Transform textAnchor;
    [TextArea]
    public string text;
    public TextBubble textBubble;
    [ContextMenu("SpawnBubble")]
    public void CreateBubble()
    {
        textBubble = TextBubbleManager.Instance.Get(text,textAnchor);
    }

    /// <summary>
    /// Create a bubble from the speaker
    /// </summary>
    /// <param name="text">text you want to display</param>
    public void CreateBubble(string text)
    {
        if(textBubble != null) { return; }
        textBubble = TextBubbleManager.Instance.Get(text, textAnchor);
    }

    public void CreateBubbleForSeconds(float time)
    {
        if (textBubble != null) { return; }
        textBubble = TextBubbleManager.Instance.Get(text, textAnchor);
        Invoke("DismissBubble", time);
    }

    [ContextMenu("Dismiss Bubble")]
    /// <summary>
    /// dismiss a bubble controlled by the speaker
    /// </summary>
    /// <para>
    /// <see cref="PopUpSpeaker"/> contains its own reference to a bubble, calling this will simply dismiss the bubble.
    /// </para>
    public void DismissBubble()
    {
        if (textBubble == null)
        {
            print($"{name}: cannot find a text bubble");
            return;
        }

        TextBubbleManager.Instance.Release(textBubble);
        textBubble = null;
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
        if(textBubble == null)
        {
            CreateBubble();
        }
        if (textBubble != null) { DismissBubble(); }
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
        if (textBubble == null)
        {
            CreateBubble(text);
        }
        if (textBubble != null) { DismissBubble(); }
    }

    public void OnDisable()
    {
        DismissBubble();
    }
}
