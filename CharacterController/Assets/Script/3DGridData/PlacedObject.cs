using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlacedObject : MonoBehaviour
{
    //we are creating a placedObject and feeding it the data of the scriptable object
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO,float scale,bool doesOccupy, bool playerRemoveable)
    {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO._prefab.transform, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));
        placedObjectTransform.localScale *= scale;
        PlacedObject placedObject = placedObjectTransform.transform.GetComponent<PlacedObject>();

        placedObject._placedObjectTypeSO = placedObjectTypeSO;
        placedObject._origin = origin; 
        placedObject._dir = dir;
        placedObject._scale = scale;
        placedObject._doesOccupy = doesOccupy;
        placedObject._playerRemoveable = playerRemoveable;
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
}
