using UnityEngine;

public class GridObject
{
    private GridXZ<GridObject> _grid;
    private int _x;
    private int _z;
    private PlacedObject _placedObject;

    public GridObject(GridXZ<GridObject> grid, int x, int z)
    {
        this._grid = grid;
        this._x = x;
        this._z = z;
    }

    public void SetPlacedObject(PlacedObject placedObject)
    {
        this._placedObject = placedObject;
        //_grid.TriggerGridObjectChanged(_x, _z);
    }

    public PlacedObject GetPlacedObject()
    {
        return _placedObject;
    }

    public bool CanBuild()
    {
        return _placedObject == null;
    }
    public void ClearPlacedObject()
    {
        _placedObject = null;
    }
    public override string ToString()
    {
        return _x + ", " + _z + "/n" + _placedObject;
    }
}
