using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    public List<GameObject> bars;
    public GardenBoxManager barManager;
    public int beauty;
    public int calmness;
    public int passion;

    public int previewBeauty;
    public int previewCalmness;
    public int previewPassion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        SetBarMax();
        UpdateBars();
        PreviewBars();
    }

    public void UpdateBars()
    {
        Dictionary<PlantAttribute,int> currentTotals = barManager.GetAttributeTotals();
        currentTotals.TryGetValue(PlantAttribute.Beauty, out beauty);
        currentTotals.TryGetValue(PlantAttribute.Passion, out passion);
        currentTotals.TryGetValue(PlantAttribute.Calmness, out calmness);

        if (beauty >= 0)
        {
            bars[0].GetComponent<StatDisplay>().currentStat = beauty;
        }
        if (calmness >= 0)
        {
            bars[1].GetComponent<StatDisplay>().currentStat = calmness;
        }
        if (passion >= 0)
        {
            bars[2].GetComponent<StatDisplay>().currentStat = passion;
        }

        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].GetComponent<StatDisplay>().UpdateBarUI();
        }
    }

    public void PreviewBars()
    {
        bars[0].GetComponent<StatDisplay>().previewStat = previewBeauty;
        bars[1].GetComponent<StatDisplay>().previewStat = previewCalmness;
        bars[2].GetComponent<StatDisplay>().previewStat = previewPassion;

        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].GetComponent<StatDisplay>().UpdateBarUI();
        }
    }

    public void SetBarMax()
    {
        //set max stat based on the puzzles goal
        //bars[0].GetComponent<StatDisplay>().maxStat = barManager.GetComponent<PuzzleTaskManager>().;
        //bars[1].GetComponent<StatDisplay>().maxStat = previewCalmness;
        //bars[2].GetComponent<StatDisplay>().maxStat = previewPassion;
    }
}
