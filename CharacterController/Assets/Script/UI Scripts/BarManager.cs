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
    public static BarManager instance;

    //maybe make this a static instance later or something??
    [SerializeField] private List<AttributeSliderPair> barPairs;
    [SerializeField] private List<AttributeSliderPair> previewBarPairs;

    private Dictionary<PlantAttribute, Slider> bars;
    private Dictionary<PlantAttribute, Slider> previewBars;
    private Dictionary<PlantAttribute, int> currentTotals = new();
    private Dictionary<PlantAttribute, int> previewValues = new();

    public GardenBoxManager gardenBoxManager;
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
        foreach (var bar in barPairs)
        {
            bar.slider.gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

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
        if (gardenBoxManager == null) return;

        currentTotals = gardenBoxManager.GetAttributeTotals();

        foreach (var entry in currentTotals)
        {
            bars[entry.Key].value = entry.Value;
        }
    }

     public void PreviewBars()
     {
        if (gardenBoxManager == null) return;

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
        if (task == null) { print($"No current task found for {puzzleTaskManager.name}"); return; }
        foreach (var threshold in task._attributeThresholds)
        {
            if(threshold.requiredValue <= 0 || threshold.showBar == false)
            {
                //do not show our bar.
                bars[threshold.attribute].gameObject.SetActive(false);
                continue;
            }
            bars[threshold.attribute].gameObject.SetActive(true);
            bars[threshold.attribute].maxValue = threshold.requiredValue;
            previewBars[threshold.attribute].maxValue = threshold.requiredValue;
        }
    }
    public void SetPuzzleManager(PuzzleTaskManager manager)
    {
        //print($"New puzzleManager: {manager.name}");
        puzzleTaskManager = manager;
    }
    public void SetGardenBoxManager(GardenBoxManager manager)
    {
        //print($"New boxManager: {manager.name}");
        gardenBoxManager = manager;
    }

    public  void ShowBars(bool show)
    {
        this.gameObject.SetActive(show);
    }

    /// <summary>
    /// Sends the required data to update bar maxes and puzzles with the respective manager. Can choose to turn the bars on or off, default on call to show them.
    /// </summary>
    /// <param name="puzzleMan"> puzzle manager the bars need to get their max from</param>
    /// <param name="boxManager"> boxes the bars wil be updating based on</param>
    /// <param name="show"> Are the bars visible</param>
    public void SetupBarManager(PuzzleTaskManager puzzleMan, GardenBoxManager boxManager,bool show = true)
    {
        //print("setting up BarManager UI");
        ShowBars(show);
        SetPuzzleManager(puzzleMan);
        SetGardenBoxManager(boxManager);
        SetBarMax();
        UpdateBars();
        PreviewBars();
    }
}
