using UnityEngine;

public class PopUpSpeaker : MonoBehaviour
{
    [SerializeField]
    private Transform textAnchor;
    [TextArea]
    public string text;
    TextBubble textBubble;
    [ContextMenu("SpawnBubble")]
    public void CreateBubble()
    {
        textBubble = TextBubbleManager.Instance.Get(text,textAnchor);
    }

    public void DismissBubble()
    {
        TextBubbleManager.Instance.Release(textBubble);
    }
}
