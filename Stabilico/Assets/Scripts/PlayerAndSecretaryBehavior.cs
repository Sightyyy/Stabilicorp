using System.Collections;
using UnityEngine;
using Pathfinding;

public class PlayerAndSecretaryBehavior : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistanceThreshold;
    private Transform ceoHomeTarget;
    private Vector3 ceoInitialPosition;
    private float distanceToTarget;
    private Transform currentTarget;
    private AudioCollection audioCollection;
    public bool isMoving = false;

    private void Start()
    {
        path = GetComponent<AIPath>();
        ceoInitialPosition = transform.position; // Assuming initial position is the starting position
        ceoHomeTarget = GameObject.Find("CEOSHome").transform; // Add a GameObject in the scene for "CEOSHome"
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
                isMoving = false;
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
        isMoving = true;
        currentTarget = ceoHomeTarget;
    }

    public void WalkToWork()
    {
        isMoving = true;
        path.destination = ceoInitialPosition; // Use workerInitialPosition as the destination
        currentTarget = null; // Clear currentTarget to stop movement
    }
}
