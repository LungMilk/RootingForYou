using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GardenBox : MonoBehaviour, IInteractable,ICameraOption
{
    private GridObject[,] _previousGridObjects;
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

    [ContextMenu("Calculate Grid Values")]
    public void CalculateGridValues()
    {
        GridObject[,] currentGridObjects = _grid.GetTGridObjectList();

        HashSet<GridObject> currentSet = new HashSet<GridObject>();
        foreach (GridObject gObject in currentGridObjects)
        {
            currentSet.Add(gObject);
        }

        if (_previousGridObjects != null)
        {
            foreach (GridObject oldObject in _previousGridObjects)
            {
                if (!currentSet.Contains(oldObject))
                {
                    PlantObject oldPlant = oldObject.GetPlacedObject() as PlantObject;

                    Dictionary<PlantAttribute, int> attributes = oldPlant.GetAttributes();

                    _beautyContribution -= attributes[PlantAttribute.Beauty];
                    _passionContribution -= attributes[PlantAttribute.Passion];
                    _calmnessContribution -= attributes[PlantAttribute.Calmness];

                    Mathf.Clamp(_beautyContribution, 0.0f, 1.0f);
                }
            }
        }

        foreach (GridObject gObject in currentGridObjects)
        {
            PlantObject plantObject = gObject.GetPlacedObject() as PlantObject;
            Dictionary<PlantAttribute, int> attributes = plantObject.GetAttributes();

            _beautyContribution += attributes[PlantAttribute.Beauty];
            _passionContribution += attributes[PlantAttribute.Passion];
            _calmnessContribution += attributes[PlantAttribute.Calmness];
        }

        _previousGridObjects = currentGridObjects;
        ChangeDisplayText();
    }

    public void ChangeDisplayText()
    {
        _displayTMPro.text = _displayText + $"\n{_beautyContribution}, <color=red>{_passionContribution}</color>, {_calmnessContribution}";
    }
    public void Interact()
    {

    }
}
