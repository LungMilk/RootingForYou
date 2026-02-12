using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
public enum TaskCheck
{
    GreaterOrEqual,
    LessOrEqual,
    Equal,
}
[System.Serializable]
public struct AttributeThreshold
{
    public PlantAttribute attribute;
    public int requiredValue;
}
[System.Serializable]
[CreateAssetMenu(fileName = "New PuzzleTaskSO", menuName = "ScriptableObjects/PuzzleTaskSO")]
public class PuzzleTaskSO : ScriptableObject
{
    //EditorGUILayout.HelpBox()
    [Tooltip("Intended to help label which task is which, Doubtful to be used in gameplay")]
    public string _name;
    [Tooltip("Use to help identify where it is going to be used and why")]
    [TextArea] public string _description;
    public TaskCheck checkOperation = TaskCheck.GreaterOrEqual;
    public List<AttributeThreshold> _attributeThresholds = new List<AttributeThreshold> {
        new AttributeThreshold {attribute = PlantAttribute.Beauty, requiredValue = 0},
        new AttributeThreshold {attribute = PlantAttribute.Passion, requiredValue = 0},
        new AttributeThreshold {attribute = PlantAttribute.Calmness, requiredValue = 0},
    };

    public bool IsCompleted(Dictionary<PlantAttribute, int> currentAttributes)
    {
        foreach (var threshold in _attributeThresholds)
        {
            if (!currentAttributes.TryGetValue(threshold.attribute, out int value))
            {
                return false;
            }
            if (CheckValue(value, threshold.requiredValue)) 
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckValue(int current, int required) 
    {
        switch (checkOperation)
        {
            case TaskCheck.GreaterOrEqual:
                return current >= required;
            case TaskCheck.LessOrEqual:
                return current <= required;
            case TaskCheck.Equal:
                return current == required;

            default:
                return false;
        }
    }
}
