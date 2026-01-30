using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New PlantObjectSO", menuName = "ScriptableObjects/PlantObjectSO")]
public class PlantObjectSO : PlacedObjectTypeSO
{
    //public string displayName;
    [Header("Plant Data")]
    [TexturePreview(5)] 
    public Sprite displayImage;

    //public GameObject objectPrefab;

    public int beauty;
    public int passion;
    public int calmness;
}
