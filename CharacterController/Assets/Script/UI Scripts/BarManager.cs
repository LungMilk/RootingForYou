using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AttributeSliderPair
{
    public PlantAttribute attribute;
    public Slider slider;
}
public class BarManager : MonoBehaviour
{
    [SerializeField] private List<AttributeSliderPair> barPairs;
    [SerializeField] private List<AttributeSliderPair> previewBarPairs;

    private Dictionary<PlantAttribute, Slider> bars;
    private Dictionary<PlantAttribute, Slider> previewBars;
    private Dictionary<PlantAttribute, int> currentTotals = new();
    private Dictionary<PlantAttribute, int> previewValues = new();

    public GardenBoxManager barManager;
    public PuzzleTaskManager puzzleTaskManager;
    //public int beauty;
    //public int calmness;
    //public int passion;
    //[Space(10)]

    //[Header("Preview Values")]
    //public int previewBeauty;
    //public int previewCalmness;
    //public int previewPassion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBarMax();
    }
    private void Awake()
    {
        bars = new Dictionary<PlantAttribute, Slider>();
        previewBars = new Dictionary<PlantAttribute, Slider>();

        foreach (var pair in barPairs)
        {
            bars[pair.attribute] = pair.slider;
        }

        foreach (var pair in previewBarPairs)
        {
            previewBars[pair.attribute] = pair.slider;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        UpdateBars();
        PreviewBars();
    }

    public void UpdateBars()
    {
        if (barManager == null) return;

        currentTotals = barManager.GetAttributeTotals();

        foreach (var entry in currentTotals)
        {
            bars[entry.Key].value = entry.Value;
        }
    }

     public void PreviewBars()
     {
        if (barManager == null) return;

        foreach (var entry in currentTotals)
        {
            currentTotals.TryGetValue(entry.Key, out int baseValue);
            previewValues.TryGetValue(entry.Key, out int previewValue);

            previewBars[entry.Key].value = baseValue + previewValue;
        }
     }

    public Dictionary<PlantAttribute, int> GetPreviewValues()
    {
        return previewValues;
    }
    public void SetPreviewValues(Dictionary<PlantAttribute, int> input)
    {
        foreach(var attribute in input)
        {
            previewValues[attribute.Key] = attribute.Value;
        }
        PreviewBars();
    }

    public void SetBarMax()
    {
        if (puzzleTaskManager == null) return;

        var task = puzzleTaskManager.GetCurrentTask();

        foreach (var threshold in task._attributeThresholds)
        {
            bars[threshold.attribute].maxValue = threshold.requiredValue;
            previewBars[threshold.attribute].maxValue = threshold.requiredValue;
        }
    }
    public void SetPuzzleManager(PuzzleTaskManager manager)
    {
        puzzleTaskManager = manager;
    }
    public void SetGardenBoxManager(GardenBoxManager manager)
    {
        barManager = manager;
    }
}
