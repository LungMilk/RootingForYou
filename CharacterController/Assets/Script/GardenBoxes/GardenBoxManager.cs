using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
//Thought this would do something in the inspector
[HelpURL("https://miro.com/app/board/uXjVGPLT8VU=/")]
public class GardenBoxManager : MonoBehaviour
{
    public UnityEvent OnDetectedChange;
    [SerializeField] private List<GardenBox> gardenBoxes;
    public PuzzleTaskManager puzzleTaskManager;
    public Collider detectionBox;


    public bool displayFog;
    [Header("Attribute Totals")]
    [SerializeField] private int _beautyTotal, _passionTotal, _calmnessTotal;

    public Material nPCMaterial;
    #region Editor Stuff
    [ContextMenu("Detect GardenBoxes")]
    public void GetGardenBoxesInZone()
    {
        gardenBoxes.Clear();

        Vector3 center = detectionBox.bounds.center;
        Vector3 halfExtents = detectionBox.bounds.extents;

        Collider[] hits = Physics.OverlapBox(center, halfExtents);
        foreach (var hit in hits)
        {
            GardenBox box = hit.GetComponentInChildren<GardenBox>();
            if (box != null)
            {
                gardenBoxes.Add(box);
                box.GardenBoxChanged.RemoveListener(OnGardenBoxChanged);
                box.GardenBoxChanged.AddListener(OnGardenBoxChanged);
            }
        }
    }

    [ContextMenu("Set materials")]
    public void SetMaterials()
    {
        if(nPCMaterial == null)
        {
            return;
        }
        foreach(var box in gardenBoxes)
        {
            if (box.materialManager == null)
            {
                continue;
            }
            box.materialManager.boxMaterial = nPCMaterial;
            box.materialManager.SetMaterials();
        }
    }
    #endregion
    private void Awake()
    {
        displayFog = true;
        foreach (var box in gardenBoxes)
        {
            if (box != null)
            {
                box.GardenBoxChanged.RemoveListener(OnGardenBoxChanged);
                box.GardenBoxChanged.AddListener(OnGardenBoxChanged);
            }
        }
        puzzleTaskManager = this.GetComponent<PuzzleTaskManager>();
        detectionBox.isTrigger = true;
    }
    public void OnGardenBoxChanged()
    {
        //there is absolutely a better way of having them only send their change in contribution instead of a full recalculation but I go no issues with this
        //maybe developing a function for these calculations would be easier but who cares
        _beautyTotal = 0;
        _passionTotal = 0;
        _calmnessTotal = 0;

        foreach (var box in gardenBoxes)
        {
            if (box == null) continue;

            Dictionary<PlantAttribute, int> boxContribution = box.GetAttributeTotals();
            _beautyTotal = Mathf.Max(0, _beautyTotal + boxContribution[PlantAttribute.Beauty]);
            _passionTotal = Mathf.Max(0, _passionTotal + boxContribution[PlantAttribute.Passion]);
            _calmnessTotal = Mathf.Max(0, _calmnessTotal + boxContribution[PlantAttribute.Calmness]);
        }
        OnDetectedChange.Invoke();

        try
        {
            OnDetectedChange?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"OnDetectedChange listener error: {ex}");
        }
    }

    public Dictionary<PlantAttribute,int> GetAttributeTotals()
    {
        return new Dictionary<PlantAttribute, int>
        {
            {PlantAttribute.Beauty, _beautyTotal },
            {PlantAttribute.Passion, _passionTotal },
            {PlantAttribute.Calmness, _calmnessTotal },
        };
    }

    public void OnPlayerEnter()
    {
        //this is where we are also going to set the bar manager and send it puzzle tasks and the like.
        if(!displayFog) { return; }

        //print("Player entered " + name);
        foreach (var box in gardenBoxes)
        {
            box.SetAnxietyFog(true);
        }

        if (HeadTrackerUI.instance != null) 
        {
            HeadTrackerUI.instance.SetPlayerMood(PlayerMood.anxious);
        }
        //we need the manager to talk or send itself to the bar manager as well as its puzzle manager.
        BarManager.instance.SetupBarManager(puzzleTaskManager,this,true);
    }

    public void OnPlayerExit()
    {
        foreach(var box in gardenBoxes)
        {
            box.SetAnxietyFog(false);
        }

        if (HeadTrackerUI.instance != null)
        {
            HeadTrackerUI.instance.SetPlayerMood(PlayerMood.happy);
        }

        BarManager.instance.ShowBars(false);
    }

    public void DisableAnxietyFog()
    {
        displayFog = false;
        foreach (var box in gardenBoxes)
        {
            box.SetAnxietyFog(false);
        }
    }

    public void EnableAnxietyFog()
    {
        displayFog = true;
        foreach (var box in gardenBoxes)
        {
            box.SetAnxietyFog(true);
        }
    }
}
