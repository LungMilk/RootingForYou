using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum PlantAttribute
{
    Beauty,
    Passion,
    Calmness,
}

public class PlantObject : PlacedObject
{
    PlantObjectSO _plantObjectSO;
    int _beauty;
    int _passion;
    int _calmness;

    private void Awake()
    {
        _plantObjectSO = _placedObjectTypeSO as PlantObjectSO;

        _beauty = _plantObjectSO.beauty;
        _passion = _plantObjectSO.passion;
        _calmness = _plantObjectSO.calmness;

        //print(GetGridPositionList()[0]);
    }

    public Dictionary<PlantAttribute,int> GetAttributes()
    {
        return new Dictionary<PlantAttribute, int>
        {
            {PlantAttribute.Beauty, _beauty },
            {PlantAttribute.Passion, _passion },
            {PlantAttribute.Calmness, _calmness },
        };
    }
}
