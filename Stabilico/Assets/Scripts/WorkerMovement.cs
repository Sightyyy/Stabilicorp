using UnityEngine;
using Pathfinding;
using System.Collections;

public class WorkerMovement : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistanceThreshold;

    private Transform drinkTarget;
    private Transform workerHomeTarget;
    private Vector3 workerInitialPosition; // Use Vector3 instead of Transform

    private float distanceToTarget;
    private Transform currentTarget;
    private AudioCollection audioCollection;
    private bool isDrinking = false;

    private void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }

    private void Start()
    {
        path = GetComponent<AIPath>();
        workerInitialPosition = transform.position; // Store the initial position as a Vector3
        Debug.Log("Worker initial position: " + workerInitialPosition);
        drinkTarget = GameObject.Find("WaterDispenser").transform;
        workerHomeTarget = GameObject.Find("WorkerHome").transform; // Add a GameObject in the scene for "WorkerHome"
    }

    private void Update()
    {
        path.maxSpeed = moveSpeed;

        // Check distance to the current target
        if (currentTarget != null)
        {
            distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);
            if (distanceToTarget < stopDistanceThreshold)
            {
                path.destination = transform.position; // Stop moving when close enough
            }
            else
            {
                path.destination = currentTarget.position; // Move to the target
            }
        }
    }

    // Public methods to set destinations
    public void WalkToHome()
    {
        currentTarget = workerHomeTarget;
    }

    public void WalkToDispenser()
    {
        currentTarget = drinkTarget;
        StartCoroutine(WaitAndReturnToTable(6f));
    }

    public void WalkToTable()
    {
        path.destination = workerInitialPosition; // Use workerInitialPosition as the destination
        currentTarget = null; // Clear currentTarget to stop movement
    }

    public void WalkToWork()
    {
        WalkToTable(); // Alias for going back to the initial position
    }

    // Coroutine to wait for a specified time before walking back to the table
    private IEnumerator WaitAndReturnToTable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        WalkToTable();
    }
}
