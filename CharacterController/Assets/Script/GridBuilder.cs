using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    //[SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    private GridXZ<GridObject> _grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    public int _gridW;
    public int _gridH;
    public float _gridCellSize;
    private void Awake()
    {
        int gridWidth = _gridW;
        int gridHeight = _gridH;
        float cellSize = _gridCellSize;
        _grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, transform.position, transform.rotation, (GridXZ<GridObject> gameObject, int x, int z) => new GridObject(_grid, x, z));
    }

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
}
