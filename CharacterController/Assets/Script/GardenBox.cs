using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GardenBox : MonoBehaviour, IInteractable,ICameraOption
{
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
    }
    private void OnGridChanged(object sender, GridXZ<GridObject>.OnGridObjectChangedEventArgs e)
    {
        CalculateGridValues();
    }
    private void Update()
    {
        if (isPressed)
        {
            //CalculateGridValues();
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

        foreach (PlantObject plant in currentPlants)
        {
            var attributes = plant.GetAttributes();

            _beautyContribution += attributes[PlantAttribute.Beauty];
            _passionContribution += attributes[PlantAttribute.Passion];
            _calmnessContribution += attributes[PlantAttribute.Calmness];
        }
        ChangeDisplayText();
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
}
