using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.Events;
public class GardenBox : MonoBehaviour, IInteractable,ICameraOption
{
    public UnityEvent GardenBoxChanged;
    public GridPreset _preset;

    bool isPressed = false;
    //private GridObject[,] _previousGridObjects;
    //public GameObject _gridObject;
    public GridBuilder _gridBuilder;
    private GridXZ<GridObject> _grid;

    public TextMeshProUGUI _displayTMPro;
    [TextArea]
    public string _displayText;

    public InteractState _interactType = InteractState.Planting;

    public int _beautyContribution, _passionContribution, _calmnessContribution;
    public InteractState InteractableType
    {
        get => _interactType;
        set => _interactType = value;
    }

    [SerializeField] private CinemachineCamera cameraOption;
    public CinemachineCamera CameraOption
    {
        get => cameraOption;
        set => cameraOption = value;
    }
    private void Start()
    {
        //_gridBuilder = _gridObject.GetComponent<GridBuilder>();
        _grid = _gridBuilder._grid;

        //if (_grid == null) { print("WEEWOOWEEWOO"); }
        _grid.OnGridObjectChanged += OnGridChanged;
        ChangeDisplayText();
        LoadGridPreset();
    }
    private void OnGridChanged(object sender, GridXZ<GridObject>.OnGridObjectChangedEventArgs e)
    {
        CalculateGridValues();
    }
    private void Update()
    {
        if (isPressed)
        {
            CalculateGridValues();
        }
    }

    [ContextMenu("Calculate Grid Values")]
    public void CalculateGridValues()
    {
        _beautyContribution = 0;
        _passionContribution = 0;
        _calmnessContribution = 0;

        HashSet<PlantObject> currentPlants = new HashSet<PlantObject>();
        
        foreach (GridObject gObject in _grid.GetTGridObjectList())
        {
            if (gObject.GetPlacedObject() is PlantObject plant)
            {
                currentPlants.Add(plant);
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
        GardenBoxChanged.Invoke();
    }

    public void ChangeDisplayText()
    {
        _displayTMPro.text = _displayText + $"\n{_beautyContribution}, <color=red>{_passionContribution}</color>, {_calmnessContribution}";
    }
    [ContextMenu("Interact")]
    public void Interact()
    {
        isPressed = !isPressed;
        CalculateGridValues();
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

                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(gridX, gridZ), PlacedObjectTypeSO.Dir.Down, obj, _grid.GetCellSize());

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    //yes the x and y might be confusing for world and grid spaces but don't worry about it
                    _grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                }
            }
        }
    }
}
