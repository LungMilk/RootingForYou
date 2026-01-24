using UnityEngine;

public class PlantObject : PlacedObject
{
    PlantObjectSO _plantObjectSO;
    int _beauty;
    int _passion;
    int _calmness;

    private void Start()
    {
        _plantObjectSO = _placedObjectTypeSO as PlantObjectSO;

        _beauty = _plantObjectSO.beauty;
        _passion = _plantObjectSO.passion;
        _calmness = _plantObjectSO.calmness;

        print(GetGridPositionList()[0]);
    }
}
