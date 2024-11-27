using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBehavior : MonoBehaviour
{
    private float moveSpeed = 2f;
    private Vector3 targetPosition;
    private Vector3 tablePosition;
    public int row;
    [SerializeField] private GameObject waypoint;

    private void Start()
    {
        targetPosition = transform.position; // Initial target is the current position
        tablePosition = transform.position;
        if(row == 0)
        {
            waypoint = GameObject.Find("Waypoint1").gameObject;
        }
        else if(row == 1)
        {
            waypoint = GameObject.Find("Waypoint2").gameObject;
        }
        else if(row == 2)
        {
            waypoint = GameObject.Find("Waypoint3").gameObject;
        }
        else
        {
            waypoint = GameObject.Find("Waypoint4").gameObject;
        }
    }

    private void Update()
    {
        // Move the worker toward the target position
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    // Public method to start moving the worker to the dispenser
    public void MoveToDispenser(Vector3 dispenserPosition)
    {
        StartCoroutine(WalkToDispenser(dispenserPosition));
    }

    private IEnumerator WalkToDispenser(Vector3 dispenserPosition)
    {
        // Step 1: Move in x-direction until 4 units away
        targetPosition = new Vector3(dispenserPosition.x - 2f, transform.position.y, transform.position.z);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPosition) < 0.1f);

        // Step 2: Adjust y-position to match dispenser
        targetPosition = new Vector3(targetPosition.x, dispenserPosition.y, transform.position.z);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPosition) < 0.1f);

        // Step 3: Move in x-direction to 0.5 units away
        targetPosition = new Vector3(dispenserPosition.x - 0.5f, dispenserPosition.y, transform.position.z);
        yield return new WaitForSeconds(3f);
        StartCoroutine(WalkToTable());
    }

    private IEnumerator WalkToTable()
    {
        // Step 1: Move in x-direction until 4 units away
        targetPosition = new Vector3(waypoint.transform.position.x, transform.position.y, transform.position.z);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPosition) < 0.1f);

        // Step 2: Adjust y-position to match dispenser
        targetPosition = new Vector3(transform.position.x, waypoint.transform.position.y, transform.position.z);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPosition) < 0.1f);

        // Step 3: Move in x-direction to 0.5 units away
        targetPosition = new Vector3(tablePosition.x, tablePosition.y, transform.position.z);
        yield return new WaitForSeconds(3f);
    }
}