using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlacedObject : MonoBehaviour
{
    //we are creating a placedObject and feeding it the data of the scriptable object
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO,float scale)
    {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO._prefab.transform, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));
        placedObjectTransform.localScale *= scale;
        PlacedObject placedObject = placedObjectTransform.transform.GetComponent<PlacedObject>();

        placedObject._placedObjectTypeSO = placedObjectTypeSO;
        placedObject._origin = origin; 
        placedObject._dir = dir;
        placedObject._scale = scale;
        return placedObject;
    }
    protected PlacedObjectTypeSO _placedObjectTypeSO;
    protected Vector2Int _origin;
    protected PlacedObjectTypeSO.Dir _dir;
    protected float _scale;

    public List<Vector2Int> GetGridPositionList()
    {
        return _placedObjectTypeSO.GetGridPositionList(_origin, _dir);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
