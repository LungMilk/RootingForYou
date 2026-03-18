using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager Instance;

    [SerializeField] private ToolTip bubblePrefab;
    [SerializeField] private int poolSize = 15;

    private Queue<ToolTip> pool = new();

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
    /// <summary>
    /// Asks the object pool to return a textbubble object
    /// </summary>
    /// <param name="text">What the bubble should contain</param>
    /// <param name="anchor">Where the bubble will be anchored</param>
    /// <returns></returns>
    public ToolTip Get(string text, Transform anchor)
    {
        if (pool.Count == 0)
        {
            CreateBubble();
        }

        var bubble = pool.Dequeue();
        bubble.Show(text, anchor);
        return bubble;
    }
    /// <summary>
    /// With the reference from <see cref="Release(ToolTip)"/> we return our textBubble back to the pool
    /// </summary>
    /// <param name="bubble">The bubble gained from <see cref="Get(string, Transform)"/></param>
    public void Release(ToolTip bubble)
    {
        if (bubble == null) { return; }
        //print("Release");
        bubble.Hide();
        pool.Enqueue(bubble);
    }
}
