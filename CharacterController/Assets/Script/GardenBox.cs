using UnityEngine;

public class GardenBox : MonoBehaviour,IInteractable
{
    public GameObject _gridObject;
    private GridBuilder _gridBuilder;
    private GridXZ<GridObject> _grid;
    public InteractState _interactType = InteractState.Planting;
    public InteractState InteractableType
    {
        get => _interactType;
        set => _interactType = value;
    }
    private void Awake()
    {
        _gridBuilder = _gridObject.GetComponent<GridBuilder>();
        _grid = _gridBuilder._grid;
    }
    public void Interact()
    {

    }
}
