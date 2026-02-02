using System.Collections.Generic;
using UnityEngine;

public class TextBubbleManager : MonoBehaviour
{
    public static TextBubbleManager Instance;

    [SerializeField] private TextBubble bubblePrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<TextBubble> pool = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            CreateBubble();
        }
    }

    void CreateBubble()
    {
        var bubble = Instantiate(bubblePrefab, transform);
        bubble.gameObject.SetActive(false);
        pool.Enqueue(bubble);
        //return bubble;
    }

    public TextBubble Get(string text, Transform anchor)
    {
        if (pool.Count == 0)
        {
            CreateBubble();
        }

        var bubble = pool.Dequeue();
        bubble.Show(text,anchor);
        return bubble;
    }

    public void Release(TextBubble bubble)
    {
        print("Release");
        bubble.Hide();
        pool.Enqueue(bubble);
    }
}
