using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
public class PuzzleTaskManager : MonoBehaviour
{
    [Header("Is Reading")]
    public List<GardenBoxManager> _gardenBoxManagers;
    [SerializeField]
    [Tooltip("Top to bottom is order they are checked, only one is checked at a time.")]
    private List<PuzzleTaskSO> _tasks;
    [SerializeField]
    private List<PuzzleTaskSO> _completedTasks;

    //[Header("Targets")]
    //[Tooltip("Targets must have an IActionCall interface")]
    //public List<GameObject> _targets;
    [Tooltip("Each object declared here will be called once if a task is completed")]
    public UnityEvent OnTaskCompleted;
    private void Start()
    {
        foreach (GardenBoxManager boxManager in _gardenBoxManagers)
        {
            boxManager.OnDetectedChange.AddListener(CheckTaskProgress);
        }
        _completedTasks = new List<PuzzleTaskSO>();
        _completedTasks.Clear();
    }
    //when we check progress we are doing a cumulative check to all box managers if they have reached the goal
    public void CheckTaskProgress()
    {
        Dictionary<PlantAttribute, int> cumulativeTotal = new Dictionary<PlantAttribute, int>();
        foreach (GardenBoxManager boxManager in _gardenBoxManagers)
        {
            //for every box manager we are going to add their cumulative totals to an existing dictionary
            Dictionary<PlantAttribute, int> managerAttributeTotal = boxManager.GetAttributeTotals();
            foreach (var entry in managerAttributeTotal)
            {
                cumulativeTotal.TryGetValue(entry.Key, out int current);
                cumulativeTotal[entry.Key] = current + entry.Value;
            }
        }

        if (_tasks == null || _tasks.Count == 0)
        {
            // No active tasks left
            return;
        }

        PuzzleTaskSO task = _tasks[0];

        if (!task.IsCompleted(cumulativeTotal))
        {
            return;
        }

        OnTaskCompleted.Invoke();

        _completedTasks.Add(task);
        _tasks.RemoveAt(0);
        //foreach (var target in _targets)
        //{
        //    if (target.TryGetComponent<IActionCall>(out var action) && !action.Called)
        //    {
        //        action.CallAction();
        //        action.Called = true;
        //    }
        //    else
        //    {
        //        print(target.name + " does not have IActionCallInterface");
        //    }
        //}
    }
    public PuzzleTaskSO GetCurrentTask()
    {
        if (_tasks != null)
            return _tasks[0];
        else return null;
    }
}
