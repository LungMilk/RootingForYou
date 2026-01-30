using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlacedObjectCollection", menuName = "Data/PlacedObjectColleciton")]
public class PlacedObjectCollection : ScriptableObject
{
    public List<PlacedObjectTypeSO> placedObjects;
}
