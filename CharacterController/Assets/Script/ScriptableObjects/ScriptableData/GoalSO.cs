using UnityEngine;

[CreateAssetMenu(fileName = "New GoalSO.asset",menuName = "ScriptableObjects/GoalSO")]
public class GoalSO : ScriptableObject
{
    [Tooltip("_displayText is what is read and displayed in UI to the player to give context as to their objective.")]
    [TextArea]
    public string _displayText;
    [Tooltip("Higher the priority the higher on the list it will appear when sorting. 0-10 is about our range")]
    public int priority;

    public bool isCompleted = false;
    [Tooltip("if true, the goal will be added to the manager regardless of if one is already present")]
    public bool repeating = false;
}
