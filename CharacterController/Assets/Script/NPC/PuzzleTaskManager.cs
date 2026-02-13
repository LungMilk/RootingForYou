using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class PuzzleTaskManager : MonoBehaviour
{
    [Header("Is Reading")]
    public List<GardenBoxManager> _gardenBoxManagers;
    [SerializeField]
    private List<PuzzleTaskSO> _tasks;

    [Header("Targets")]
    [Tooltip("Targets must have an IActionCall interface")]
    public List<GameObject> _targets;

    private void Start()
    {
        foreach (GardenBoxManager boxManager in _gardenBoxManagers)
        {
            boxManager.OnDetectedChange.AddListener(CheckTaskProgress);
        }
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
                cumulativeTotal.Add(entry.Key, entry.Value);
            }
        }

        //Problem!!!! this would call actions multiple times.
        foreach (PuzzleTaskSO task in _tasks)
        {
            if (task == null) {
                print("Task is not assigned");
                return; }

            if (!task.IsCompleted(cumulativeTotal)) { return; }

            foreach (var target in _targets)
            {
                if (target.TryGetComponent<IActionCall>(out var action) && !action.Called)
                {
                    action.CallAction();
                    action.Called = true;
                }
                else
                {
                    print(target.name + " does not have IActionCallInterface");
                }
            }
        }
    }
    public PuzzleTaskSO GetCurrentTask()
    {
        if (_tasks != null)
            return _tasks[0];
        else return null;
    }
}
