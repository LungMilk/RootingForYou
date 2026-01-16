using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO)
    {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO._prefab, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.transform.GetComponent<PlacedObject>();

        placedObject._placedObjectTypeSO = placedObjectTypeSO;
        placedObject._origin = origin; 
        placedObject._dir = dir;
        return placedObject;
    }

    private PlacedObjectTypeSO _placedObjectTypeSO;
    private Vector2Int _origin;
    private PlacedObjectTypeSO.Dir _dir;

    public List<Vector2Int> GetGridPositionList()
    {
        return _placedObjectTypeSO.GetGridPositionList(_origin, _dir);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
