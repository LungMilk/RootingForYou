using CustomNamespace.Utilities;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    private PlacedObjectTypeSO placedObjectTypeSO;
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
        _grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, Quaternion.identity, (GridXZ<GridObject> gameObject, int x, int z) => new GridObject(_grid,x,z));

        placedObjectTypeSO = placedObjectTypeSOList[0];
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            _grid.GetXZ(Utilities.GetMouseWorldPositionXZ(), out int x, out int z);
            Debug.Log($"{x},{z}");
            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, z),dir);

            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!_grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    canBuild = false; break;
                }
            }

            if (canBuild)
            {
                //offsetting the origin of the object by whatever our rotation was
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = _grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * _grid.GetCellSize();

                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, z), dir, placedObjectTypeSO);

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    //yes the x and y might be confusing for world and grid spaces but don't worry about it
                    _grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
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

        if (Input.GetMouseButtonDown(1))
        {
            GridObject gridObject = _grid.GetGridObject(Utilities.GetMouseWorldPositionWithZ());
            PlacedObject placedObject = gridObject.GetPlacedObject();
            if (placedObject != null)
            {
                placedObject.DestroySelf();

                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    _grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) ){
            dir = PlacedObjectTypeSO.GetNextDir(dir);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            placedObjectTypeSO = placedObjectTypeSOList[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            placedObjectTypeSO = placedObjectTypeSOList[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            placedObjectTypeSO = placedObjectTypeSOList[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            placedObjectTypeSO = placedObjectTypeSOList[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            placedObjectTypeSO = placedObjectTypeSOList[4];
        }
    }
}
