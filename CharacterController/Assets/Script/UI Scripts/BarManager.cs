using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public List<Slider> bars;
    public List<Slider> previewBars;
    public GardenBoxManager barManager;
    public PuzzleTaskManager puzzleTaskManager;
    public int beauty;
    public int calmness;
    public int passion;
    [Space(10)]
    [Header("Preview Values")]
    public int previewBeauty;
    public int previewCalmness;
    public int previewPassion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBarMax();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateBars();
        //PreviewBars();
    }

    public void UpdateBars()
    {
        if (barManager == null) { return; }

        Dictionary<PlantAttribute, int> currentTotals = barManager.GetAttributeTotals();

        currentTotals.TryGetValue(PlantAttribute.Beauty, out beauty);
        currentTotals.TryGetValue(PlantAttribute.Passion, out passion);
        currentTotals.TryGetValue(PlantAttribute.Calmness, out calmness);

        // MAIN BARS
        bars[0].value = beauty;
        bars[1].value = calmness;
        bars[2].value = passion;

        // PREVIEW BARS (current + selected plant preview)
        previewBars[0].value = beauty + previewBeauty;
        previewBars[1].value = calmness + previewCalmness;
        previewBars[2].value = passion + previewPassion;
    }

    public void PreviewBars()
    {
        previewBars[0].value = previewBeauty + beauty;
        previewBars[1].value = previewCalmness + calmness;
        previewBars[2].value = previewPassion + passion;
    }

    public void SetBarMax()
    {
        if (puzzleTaskManager == null) {return; }
        //ideally this would follow the dictionary method with the different assets doing their thing
        //set max stat based on the puzzles goal
        PuzzleTaskSO currentTask = puzzleTaskManager.GetCurrentTask();
        //need to have better sortingand the like maybe dictionary but that is later
        bars[0].maxValue = currentTask._attributeThresholds[0].requiredValue;
        bars[1].maxValue = currentTask._attributeThresholds[1].requiredValue;
        bars[2].maxValue = currentTask._attributeThresholds[2].requiredValue;

        previewBars[0].maxValue = currentTask._attributeThresholds[0].requiredValue;
        previewBars[1].maxValue = currentTask._attributeThresholds[1].requiredValue;
        previewBars[2].maxValue = currentTask._attributeThresholds[2].requiredValue;
    }
    public void SetPuzzleManager(PuzzleTaskManager manager)
    {
        puzzleTaskManager = manager;
    }
}
