using UnityEngine;

public class ModifierObject : PlacedObject
{
    ModifierObjectSO _modifierObjectSO;
    public float _modifier;

    private void Start()
    {
        _modifierObjectSO = _placedObjectTypeSO as ModifierObjectSO;
        _modifier = _modifierObjectSO._modifier;
        //print(GetGridPositionList()[0]);
    }
}
