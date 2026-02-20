using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public struct GridPlacementData
{
    public Vector3 originWorldPos;
    public Quaternion rotation;
    public float cellSize;

    public GridPlacementData(Vector3 originWorldPos, Quaternion rotation, float cellSize)
    {
        this.originWorldPos = originWorldPos;
        this.rotation = rotation;
        this.cellSize = cellSize;
    }
}
public class PlacedObject : MonoBehaviour
{
    //we are creating a placedObject and feeding it the data of the scriptable object
    public static PlacedObject Create(GridPlacementData data, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO,float scale,bool doesOccupy, bool playerRemoveable)
    {
        //our base world position for grid cell
        Vector3 baseWorldPos = data.originWorldPos + data.rotation * new Vector3(origin.x,0,origin.y) * data.cellSize;
        //rotaion offset in the grids units
        Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
        Vector3 offsetWorld = data.rotation * new Vector3(rotationOffset.x,0, rotationOffset.y) * data.cellSize;

        //final world position
        Vector3 finalWorldPos = baseWorldPos + offsetWorld;
        //final rotation ( grid rotation + objects rotation
        Quaternion finalRotation = data.rotation * Quaternion.Euler(0,placedObjectTypeSO.GetRotationAngle(dir), 0);

        Transform placedObjectTransform = Instantiate(placedObjectTypeSO._prefab.transform, finalWorldPos, finalRotation);
        placedObjectTransform.localScale *= scale;
        PlacedObject placedObject = placedObjectTransform.transform.GetComponent<PlacedObject>();

        //seting up its data
        placedObject._placedObjectTypeSO = placedObjectTypeSO;
        placedObject._origin = origin; 
        placedObject._dir = dir;
        placedObject._scale = scale;
        placedObject._doesOccupy = doesOccupy;
        placedObject._playerRemoveable = playerRemoveable;
        placedObject.Initialize(placedObjectTypeSO);
        return placedObject;
    }
    //if we have placed object simply make a bool to if occupies the gridobject can take said bool and just return its chill
    //how it modifies plant values, idk.
    protected PlacedObjectTypeSO _placedObjectTypeSO;
    protected Vector2Int _origin;
    protected PlacedObjectTypeSO.Dir _dir;
    protected float _scale;
    protected bool _doesOccupy;
    protected bool _playerRemoveable;
    public bool DoesOccupy { get { return _doesOccupy; } }
    public bool PlayerRemovable { get { return _playerRemoveable; } }
    public List<Vector2Int> GetGridPositionList()
    {
        return _placedObjectTypeSO.GetGridPositionList(_origin, _dir);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public virtual void Initialize(PlacedObjectTypeSO typeSO)
    {
        _placedObjectTypeSO = typeSO;
    }
}
