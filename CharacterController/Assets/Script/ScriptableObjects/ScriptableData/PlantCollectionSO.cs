using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New PlantCollectionSO", menuName = "Data/PlantCollectionSO")]
public class PlantCollectionSO : ScriptableObject
{
    public List<PlantObjectSO> plants;
}
