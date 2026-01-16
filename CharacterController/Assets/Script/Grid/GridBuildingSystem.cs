using CustomNamespace.Utilities;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private PlacedObjectTypeSO placedObjectTypeSO;
    private GridXZ<GridObject> _grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;
    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        _grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> gameObject, int x, int z) => new GridObject(_grid,x,z));
    }

    public class GridObject
    {
        private GridXZ<GridObject> _grid;
        private int _x;
        private int _z;
        private Transform _transform;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this._grid = grid;
            this._x = x;
            this._z = z;
        }

        public void SetTransform(Transform transform)
        {
            this._transform = transform;
            _grid.TriggerGridObjectChanged(_x, _z);
        }
        public bool CanBuild()
        {
            return _transform == null;
        }
        public void ClearTransform()
        {
            _transform = null;
        }
        public override string ToString()
        {
            return _x + ", " + _z + "/n" +_transform;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            _grid.GetXZ(Utilities.GetMouseWorldPositionWithZ(), out int x, out int z);
            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, z));

            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!_grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) ;
                {
                    canBuild = false; break;
                }
            }

            if (canBuild)
            {
                Transform builtTransform = Instantiate(placedObjectTypeSO.prefab, _grid.GetWorldPosition(x, z), Quaternion.identity);

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    //yes the x and y might be confusing for world and grid spaces but don't worry about it
                    _grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(builtTransform);
                }
            }
            else
            {
                GameObject emptyGO = new GameObject("errorPopupWorldtext");
                Transform transform = emptyGO.transform;
                transform.position = Utilities.GetMouseWorldPositionWithZ();
                Utilities.CreateWorldTextObject("Cannot build", transform);
                new WaitForSeconds(2);
                Destroy(emptyGO);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) ){
            dir = PlacedObjectTypeSO.GetNextDir(dir);
        }
    }
}
