using UnityEngine;
using System.Collections.Generic;
public class GardenBoxManager : MonoBehaviour
{
    [SerializeField] private List<GardenBox> gardenBoxes;
    public Collider detectionBox;

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
            }
        }
    }
    #endregion
    private void Awake()
    {
        foreach (var box in gardenBoxes)
        {
            if (box != null)
                box.GardenBoxChanged.AddListener(OnGardenBoxChanged);
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
            _beautyTotal  += box._beautyContribution;
            _passionTotal += box._passionContribution;
            _calmnessTotal +=box._calmnessContribution;
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
