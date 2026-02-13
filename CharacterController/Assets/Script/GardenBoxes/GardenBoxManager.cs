using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Thought this would do something in the inspector
[HelpURL("https://miro.com/app/board/uXjVGPLT8VU=/")]
public class GardenBoxManager : MonoBehaviour
{
    public UnityEvent OnDetectedChange;
    [SerializeField] private List<GardenBox> gardenBoxes;
    public Collider detectionBox;

    [Header("Attribute Totals")]
    [SerializeField] private int _beautyTotal, _passionTotal, _calmnessTotal;

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
    #endregion
    private void Awake()
    {
        foreach (var box in gardenBoxes)
        {
            if (box != null)
            {
                box.GardenBoxChanged.RemoveListener(OnGardenBoxChanged);
                box.GardenBoxChanged.AddListener(OnGardenBoxChanged);
            }
        }

        detectionBox.gameObject.SetActive(false);
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
            _beautyTotal += boxContribution[PlantAttribute.Beauty];
            _passionTotal += boxContribution[PlantAttribute.Passion];
            _calmnessTotal += boxContribution[PlantAttribute.Calmness];
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
}
