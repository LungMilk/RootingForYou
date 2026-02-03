using CustomNamespace.Utilities;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
//using static PlacedObjectTypeSO;

public class PlayerPlantingState : PlayerBaseState
{
    GridBuilder _gridBuilder;
    GridXZ<GridObject> _grid;
    public PlayerPlantingState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() {
        Debug.Log("Entering planting state");
        _gridBuilder = Ctx.InputObject.GetComponentInChildren<GridBuilder>();
        _grid = _gridBuilder._grid;

        Ctx._selectedPlantObject = Ctx._plantCollection.plants[0];
    }
    public override void UpdateState() { 

        if (Input.GetMouseButtonDown(0))
        {
            _grid.GetXZ(Utilities.GetMouseWorldPositionXZ(), out int x, out int z);
            //Debug.Log($"{x},{z}");
            List<Vector2Int> gridPositionList = Ctx._selectedPlantObject.GetGridPositionList(new Vector2Int(x, z), Ctx._dir);

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
                Vector2Int rotationOffset = Ctx._selectedPlantObject.GetRotationOffset(Ctx._dir);
                Vector3 placedObjectWorldPosition = _grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * _grid.GetCellSize();

                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, z), Ctx._dir, Ctx._plantCollection.plants[0], _grid.GetCellSize());

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
                //Utilities.CreateWorldTextObject("Cannot build", transform);
                new WaitForSeconds(2);
                GameObject.Destroy(emptyGO);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _grid.GetXZ(Utilities.GetMouseWorldPositionXZ(), out int x, out int z);
            GridObject gridObject = _grid.GetGridObject(x,z);

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Ctx._dir = PlacedObjectTypeSO.GetNextDir(Ctx._dir);
        }

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
    public override void ExitState() {
       
    }
    public override void CheckSwitchStates()
    {

    }
    public override void InitializeSubState() { }
}
