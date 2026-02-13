using UnityEngine;
using System.Collections.Generic;
public class GridObject
{
    private GridXZ<GridObject> _grid;
    private int _x;
    private int _z;
    private List<PlacedObject> _placedObjects;

    public GridObject(GridXZ<GridObject> grid, int x, int z)
    {
        this._grid = grid;
        this._x = x;
        this._z = z;
        _placedObjects = new List<PlacedObject>();
    }
    public List<PlacedObject> GetRemovablePlacedObjects()
    {
        List<PlacedObject> removables = new List<PlacedObject>();
        foreach (var entry in _placedObjects)
        {
            if (entry.PlayerRemovable)
            {
                removables.Add(entry);
            }
        }
        return removables;
    }
    public void SetPlacedObject(PlacedObject placedObject)
    {
        //var oldObject = _placedObjects;
        _placedObjects.Add(placedObject);
        _grid.TriggerGridObjectChanged(_x, _z);
    }

    public List<PlacedObject> GetPlacedObjects()
    {
        return _placedObjects;
    }

    public bool CanBuild()
    {
        //Debug.Log(_placedObjects.Count);
        if (_placedObjects.Count == 0)
        {
            return true;
        }
        if (_placedObjects[0] == null)
        {
            return true;
        }

        return !_placedObjects[0].DoesOccupy;
    }
    public void ClearPlacedObject()
    {
        foreach (var entry in GetRemovablePlacedObjects())
        {
            _placedObjects.Remove(entry);
        }
        _grid.TriggerGridObjectChanged(_x, _z);
    }
    public override string ToString()
    {
        return _x + ", " + _z + "/n" + _placedObjects;
    }
}
