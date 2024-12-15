using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject workerAPrefab; // Prefab for WorkerA
    [SerializeField] private GameObject workerBPrefab; // Prefab for WorkerB
    [SerializeField] private Transform workerParent;   // Parent object for organizing workers in the hierarchy
    [SerializeField] private float initialX = 10.75f;  // Starting X position
    [SerializeField] private float xIncrement = 2.75f; // X increment for each column
    [SerializeField] private float initialY = 2f;    // Starting Y position
    [SerializeField] private float yDecrement = 2f;  // Y decrement for each new row
    [SerializeField] private int maxColumns = 5;       // Maximum workers per row

    private int currentWorkerDisplayCount = 0;
    private DayAndTimeManager dayAndTimeManager;

    void Start()
    {
        dayAndTimeManager = FindObjectOfType<DayAndTimeManager>();
    }

    void Update()
    {
        // Get the worker amount from GameData
        int workerAmount = GameData.instance.workerAmount;

        // Calculate the number of workers that should be displayed
        int targetWorkerDisplayCount = workerAmount / 5;

        // Adjust workers based on the difference
        if (targetWorkerDisplayCount > currentWorkerDisplayCount)
        {
            AddWorkers(targetWorkerDisplayCount - currentWorkerDisplayCount);
        }
        else if (targetWorkerDisplayCount < currentWorkerDisplayCount)
        {
            Debug.Log("Remove Worker");
            RemoveWorkers(currentWorkerDisplayCount - targetWorkerDisplayCount);
        }

        dayAndTimeManager.activeWorkers.RemoveAll(x=>x==null);
    }

    private void AddWorkers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Calculate the index of the worker being added
            int workerIndex = currentWorkerDisplayCount + i;

            // Determine row and column based on index
            int column = workerIndex % maxColumns; // Column within the row
            int row = workerIndex / maxColumns;    // Row number

            // Calculate position
            float newX = initialX + (column * xIncrement);
            float newY = initialY - (row * yDecrement);
            Vector3 spawnPosition = new Vector3(newX, newY, 0);

            // Randomly choose between WorkerA and WorkerB
            GameObject workerPrefab = Random.value > 0.5f ? workerAPrefab : workerBPrefab;

            // Instantiate the worker at the calculated position
            GameObject clone = Instantiate(workerPrefab, spawnPosition, Quaternion.identity, workerParent);
            clone.GetComponent<WorkerBehavior>().row = row;
            dayAndTimeManager.activeWorkers.Add(clone);
        }

        // Update the current display count
        currentWorkerDisplayCount += count;
    }

    private void RemoveWorkers(int count)
    {
        // Remove workers from the parent transform
        for (int i = workerParent.childCount - 1; i >= workerParent.childCount-count; i--)
        {
            if (workerParent.childCount > 0 && i < workerParent.childCount)
            {
                Debug.Log("Remove one worker");
                Debug.Log(workerParent.GetChild(i).gameObject.name);
                // dayAndTimeManager.activeWorkers.Remove(workerParent.GetChild(workerParent.childCount - 1).gameObject);
                Destroy(workerParent.GetChild(i).gameObject);
            }
        }

        // Update the current display count
        currentWorkerDisplayCount -= count;
    }
}
