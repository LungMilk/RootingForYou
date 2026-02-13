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
    [SerializeField]
    int _beauty;
    [SerializeField]
    int _passion;
    [SerializeField]
    int _calmness;

    public override void Initialize(PlacedObjectTypeSO typeSO)
    {
        base.Initialize(typeSO);

        _plantObjectSO = typeSO as PlantObjectSO;

        if (_plantObjectSO == null)
        {
            Debug.LogError($"{name} initialized with non-PlantObjectSO!");
            return;
        }

        _beauty = _plantObjectSO.beauty;
        _passion = _plantObjectSO.passion;
        _calmness = _plantObjectSO.calmness;
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
