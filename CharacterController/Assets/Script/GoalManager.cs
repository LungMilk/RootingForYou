using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
public class GoalManager : MonoBehaviour
{
    //needs to be a global static instance that will log any and all available tasks with a priority
    public static GoalManager Instance;
    [SerializeField]
    private List<GoalSO> _objectives = new List<GoalSO>();
    private List<GoalSO> _completedObjectives = new List<GoalSO>();

    public TextMeshProUGUI _goalUIText;
    private string _goalListText;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PopulateObjectiveList();
    }
    /// <summary>
    /// Sorts the objectives based off of priority, then outputs a string to <see cref="_goalUIText"/> to be displayed.
    /// </summary>
    [ContextMenu("PopulateObjectiveList")]
    public void PopulateObjectiveList()
    {
        if (_objectives == null) { _goalListText = $"No Objectives Currently"; }

        _objectives.Sort((a,b) => b.priority.CompareTo(a.priority));

        for(int i = 0;  i < _objectives.Count; i++)
        {
            if (i == 0)
            {
                _goalListText = $"1.{_objectives[i]._displayText}";
            }
            else
            {
                _goalListText += $"\n{i +1}.{_objectives[i]._displayText}";
            }
        }

        _goalUIText.text = _goalListText;
    }

    /// <summary>
    /// Tell the manager which goal has been completed, that goal is then added to the <see cref="_completedObjectives"/> for storage.
    /// </summary>
    /// <param name="goal"></param>
    public void CompleteObjective(GoalSO recievedGoal)
    {
        GoalSO currentGoal = _objectives.Find(item => item == recievedGoal);
        if (currentGoal == null) {
            print($"no objective of {recievedGoal.name} found in list");
        currentGoal.isCompleted = true;
        _completedObjectives.Add(currentGoal);
        _objectives.Remove(currentGoal);
        PopulateObjectiveList();
            }
    }
    /// <summary>
    /// Add a new goal to <see cref="_objectives"/>
    /// </summary>
    /// <param name="goal"></param>
    public void AddObjective(GoalSO recievedGoal)
    {
        if (_objectives.Contains(recievedGoal) && recievedGoal.repeating == false)
        {
            print($"{recievedGoal.name} objective already present in list");
            return;
        }
        _objectives.Add(recievedGoal);
        PopulateObjectiveList();
    }

    public List<GoalSO> GetCurrentObjectives()
    {
        return _objectives;
    }
    public List<GoalSO> GetCompletedObjectives()
    {
        return _completedObjectives;
    }
}
