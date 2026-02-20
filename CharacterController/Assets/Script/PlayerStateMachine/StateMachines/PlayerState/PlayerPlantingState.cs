using CustomNamespace.Utilities;
using ScriptableObjects;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
//using static PlacedObjectTypeSO;

public class PlayerPlantingState : PlayerBaseState
{
    GridBuilder _gridBuilder;
    GridXZ<GridObject> _grid;
    SoundEffectSO digSound;
    SoundEffectSO plantSound;
    public PlayerPlantingState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() {
        //sound effects need to be reworked
        digSound = Ctx._soundEffects[0];
        plantSound = Ctx._soundEffects[1];
        Debug.Log("Entering planting state");
        _gridBuilder = Ctx.InputObject.GetComponentInChildren<GridBuilder>();
        _grid = _gridBuilder._grid;

        Ctx._selectedPlantObject = Ctx._plantCollection.plants[0];
    }
    public override void UpdateState() {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Plane gridPlane = new Plane(Vector3.up, _grid.GetWorldPosition(0, 0));

            if (gridPlane.Raycast(ray,out float distance)) { 
                Vector3 hitWorldPos = ray.GetPoint(distance);

                _grid.GetXZ(hitWorldPos, out int x, out int z);
                if (_grid.IsValidGridPosition(new Vector2Int(x, z)))
                {
                    Debug.Log($"Clicked cell: {x}, {z}");
                    PlacePlantOnGrid(x,z);
                }
            }
        }
        //i know its dirty just bare with me on this
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Plane gridPlane = new Plane(Vector3.up, _grid.GetWorldPosition(0, 0));

            if (gridPlane.Raycast(ray, out float distance))
            {
                Vector3 hitWorldPos = ray.GetPoint(distance);

                _grid.GetXZ(hitWorldPos, out int x, out int z);
                if (_grid.IsValidGridPosition(new Vector2Int(x, z)))
                {
                    Debug.Log($"Clicked cell: {x}, {z}");
                    RemovePlantOnGrid(x, z);
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Ctx._dir = PlacedObjectTypeSO.GetNextDir(PlacedObjectTypeSO.Dir.Down);
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Ctx._selectedPlantObject = Ctx._plantCollection.plants[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Ctx._selectedPlantObject = Ctx._plantCollection.plants[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Ctx._selectedPlantObject = Ctx._plantCollection.plants[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Ctx._selectedPlantObject = Ctx._plantCollection.plants[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Ctx._selectedPlantObject = Ctx._plantCollection.plants[4];
        }

        CheckSwitchStates(); }

    private void RemovePlantOnGrid(int X, int Z)
    {
        //inconsistent
        GridObject gridObject = _grid.GetGridObject(X, Z);

        //List<PlacedObject> placedObjects = gridObject.GetRemovablePlacedObject();
        PlacedObject placedObject = gridObject.GetPlacedObject();
        if (placedObject == null) { return; }

        var obj = placedObject;

        obj.DestroySelf();
        digSound.Play();

        List<Vector2Int> gridPositionList = obj.GetGridPositionList();

        foreach (Vector2Int gridPosition in gridPositionList)
        {
            //I think it is with my handling of clear placed and clearing removables as I am grabbing the space and calling for an all clear when I need to only clear the desired entry list from removables.
            _grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
        }
    }

    private void PlacePlantOnGrid(int X, int Z)
    {
        List<Vector2Int> gridPositionList = Ctx._selectedPlantObject.GetGridPositionList(new Vector2Int(X, Z), PlacedObjectTypeSO.Dir.Down);

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
            Vector2Int rotationOffset = Ctx._selectedPlantObject.GetRotationOffset(PlacedObjectTypeSO.Dir.Down);
            Vector3 placedObjectWorldPosition = _grid.GetWorldPosition(X, Z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * _grid.GetCellSize();

            var gridData = new GridPlacementData
            {
                cellSize = _grid.GetCellSize(),
                originWorldPos = _grid.GetOriginPosition(),
                rotation = _grid.GetOriginRotation(),
            };

            PlacedObject placedObject = PlacedObject.Create(gridData, new Vector2Int(X, Z), PlacedObjectTypeSO.Dir.Down, Ctx._selectedPlantObject, _grid.GetCellSize(), Ctx._selectedPlantObject._doesOccupy, Ctx._selectedPlantObject._playerRemovable);

            plantSound.Play();

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                //yes the x and y might be confusing for world and grid spaces but don't worry about it
                _grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }
        }
        //else
        //{
        //    GameObject emptyGO = new GameObject("errorPopupWorldtext");
        //    Transform transform = emptyGO.transform;
        //    transform.position = Utilities.GetMouseWorldPositionWithZ();
        //    //Utilities.CreateWorldTextObject("Cannot build", transform);
        //    new WaitForSeconds(2);
        //    GameObject.Destroy(emptyGO);
        //}
    }

    public override void ExitState() {
       
    }
    public override void CheckSwitchStates()
    {

    }
    public override void InitializeSubState() { }
}
