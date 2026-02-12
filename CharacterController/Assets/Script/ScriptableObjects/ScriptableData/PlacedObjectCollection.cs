using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlacedObjectCollection", menuName = "SOCollections/PlacedObjectColleciton")]
public class PlacedObjectCollection : ScriptableObject
{
    public List<PlacedObjectTypeSO> placedObjects;
}
