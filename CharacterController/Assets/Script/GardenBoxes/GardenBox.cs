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

    private int _beautyContribution, _passionContribution, _calmnessContribution;
    private void Start()
    {
        //_gridBuilder = _gridObject.GetComponent<GridBuilder>();
        _grid = _gridBuilder._grid;

        //if (_grid == null) { print("WEEWOOWEEWOO"); }
        ChangeDisplayText();
        LoadGridPreset();

        _grid.OnGridObjectChanged += OnGridChanged;
    }
    private void OnGridChanged(object sender, GridXZ<GridObject>.OnGridObjectChangedEventArgs e)
    {
        CalculateGridValues();
        //There is an issue with removing that gives an out of array index error that I currently do not know how to fix
        GardenBoxChanged?.Invoke();
    }

    [ContextMenu("Calculate Grid Values")]
    public void CalculateGridValues()
    {
        _beautyContribution = 0;
        _passionContribution = 0;
        _calmnessContribution = 0;

        HashSet<PlantObject> currentPlants = new HashSet<PlantObject>();
        float multipler = 1;
        foreach (GridObject gObject in _grid.GetTGridObjectList())
        {
            if (gObject.GetPlacedObjects().OfType<ModifierObject>().FirstOrDefault() is ModifierObject modifier)
            {
                multipler = modifier._modifier;
            }
            //multiplier = entry.modifier
            foreach (var entry in gObject.GetPlacedObjects())
            {
                if (entry is PlantObject plantObject)
                {
                    var plantAttributes = plantObject.GetAttributes();

                    plantAttributes[PlantAttribute.Beauty] = Mathf.CeilToInt(plantAttributes[PlantAttribute.Beauty] * multipler);
                    plantAttributes[PlantAttribute.Passion] =Mathf.CeilToInt(plantAttributes[PlantAttribute.Passion] * multipler);
                    plantAttributes[PlantAttribute.Calmness] =Mathf.CeilToInt(plantAttributes[PlantAttribute.Calmness] * multipler);

                    currentPlants.Add(plantObject);
                }
            }
        }
        //we can expland the getting of placed objects wihtin the space to have the calculation of if the space has a modifier
        foreach (PlantObject plant in currentPlants)
        {
            var attributes = plant.GetAttributes();

            _beautyContribution += attributes[PlantAttribute.Beauty];
            _passionContribution += attributes[PlantAttribute.Passion];
            _calmnessContribution += attributes[PlantAttribute.Calmness];
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
        if (_preset == null) { return; }

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
                    //yes the x and y might be confusing for world and grid spaces but don't worry about it
                    _grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                }
            }
        }
    }
}
