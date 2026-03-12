using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;
public class GardenBox : Interactable
{
    [Space(20)]
    [Header("GardenBox Fields")]
    public UnityEvent GardenBoxChanged;
    [Space(10)]
    [Header("GardenBox Setup")]
    public GridPreset _preset;

    //bool isPressed = false;
    //private GridObject[,] _previousGridObjects;
    //public GameObject _gridObject;
    public GridBuilder _gridBuilder;
    private GridXZ<GridObject> _grid;

    public TextMeshProUGUI _displayTMPro;

    [Space(10)]
    [Header("Attributes")]
    [TextArea]
    public string _displayText;

    [SerializeField]
    private int _beautyContribution, _passionContribution, _calmnessContribution;
    private void Start()
    {
        //_gridBuilder = _gridObject.GetComponent<GridBuilder>();
        _grid = _gridBuilder._grid;

        //if (_grid == null) { print("WEEWOOWEEWOO"); }
        ChangeDisplayText();
        //LoadGridPreset();

        if (_grid != null)
        {
            _grid.OnGridObjectChanged += OnGridChanged;
        }
    }
    private void OnGridChanged(object sender, GridXZ<GridObject>.OnGridObjectChangedEventArgs e)
    {
        if (_grid == null) return;

        if (!_grid.IsValidGridPosition(new Vector2Int(e.x, e.z))) return;

        CalculateGridValues();

        try
        {
            GardenBoxChanged?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"GardenBoxChanged listener threw an error: {ex}");
        }
    }

    [ContextMenu("Calculate Grid Values")]
    public void CalculateGridValues()
    {
        _beautyContribution = 0;
        _passionContribution = 0;
        _calmnessContribution = 0;

        HashSet<PlantObject> currentPlants = new HashSet<PlantObject>();
        //float multipler = 1;

        var gridObjects = new List<GridObject>(_grid.GetTGridObjectList());
        foreach (GridObject gObject in gridObjects)
        {
            if (gObject == null) continue;
            //if (gObject.GetPlacedObjects().OfType<ModifierObject>().FirstOrDefault() is ModifierObject modifier)
            //{
            //    multipler = modifier._modifier;
            //}
            //multiplier = entry.modifier
            //foreach (var entry in gObject.GetPlacedObject())
            //{

            if (gObject.GetPlacedObject() is PlantObject plant)
            {
                var plantAttributes = plant.GetAttributes();

                currentPlants.Add(plant);
            }
            
        }
        //we can expland the getting of placed objects wihtin the space to have the calculation of if the space has a modifier
        foreach (PlantObject plant in currentPlants)
        {
            //print("We have plants");
            var attributes = plant.GetAttributes();
            _beautyContribution += attributes[PlantAttribute.Beauty];
            //print($"beauty: {attributes[PlantAttribute.Beauty]}");
            _passionContribution += attributes[PlantAttribute.Passion];
            //print($"passion: {attributes[PlantAttribute.Passion]}");
            _calmnessContribution += attributes[PlantAttribute.Calmness];
            //print($"calmness: {attributes[PlantAttribute.Calmness]}");
        }

        ChangeDisplayText();
    }

    public Dictionary<PlantAttribute, int> GetAttributeTotals()
    {
        return new Dictionary<PlantAttribute, int>
        {
            {PlantAttribute.Beauty, _beautyContribution },
            {PlantAttribute.Passion, _passionContribution },
            {PlantAttribute.Calmness, _calmnessContribution },
        };
    }

    public void ChangeDisplayText()
    {
        _displayTMPro.text = _displayText + $"\n{_beautyContribution}, <color=red>{_passionContribution}</color>, {_calmnessContribution}";
    }

    public void LoadGridPreset()
    {
        if (_preset == null || _grid == null) return;

        int height = _preset._grid.Length;
        for (int y = 0; y < height; y++)
        {
            var row = _preset._grid[y]._values;
            int width = row.Length;
            for (int x = 0; x < width; x++)
            {
                if (row[x] == null) {  continue; } 
                PlacedObjectTypeSO obj = row[x];
                int gridZ = y;
                int gridX = x;
                //_grid.GetGridObject(gridX, gridZ).SetPlacedObject(obj);
                List<Vector2Int> gridPositionList = obj.GetGridPositionList(new Vector2Int(gridX, gridZ), PlacedObjectTypeSO.Dir.Down);
                Vector2Int rotationOffset = obj.GetRotationOffset(PlacedObjectTypeSO.Dir.Down);
                Vector3 placedObjectWorldPosition = _grid.GetWorldPosition(gridX, gridZ) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * _grid.GetCellSize();

                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(gridX, gridZ), PlacedObjectTypeSO.Dir.Down, obj, _grid.GetCellSize(), obj._doesOccupy, obj._playerRemovable);

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    if (!_grid.IsValidGridPosition(gridPosition))
                    {
                        Debug.LogWarning($"Skipping out-of-bounds grid position {gridPosition} for object {obj.name}");
                        continue;
                    }

                    GridObject targetGridObject = _grid.GetGridObject(gridPosition.x, gridPosition.y);
                    if (targetGridObject != null)
                    {
                        targetGridObject.SetPlacedObject(placedObject);
                    }
                }
            }
        }
    }
}
