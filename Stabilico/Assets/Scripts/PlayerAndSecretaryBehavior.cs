using System.Collections;
using UnityEngine;

public class PlayerAndSecretaryBehavior : MonoBehaviour
{
    private float moveSpeed = 2f;
    private Vector3 targetPosition;
    private Vector3 workPosition;
    private Vector3 homePosition;
    private Vector3 intermediatePosition;

    private void Start()
    {
        // Set the current position as the work position
        workPosition = transform.position;

        // Set home position for player or secretary
        if (gameObject.name.Contains("Player"))
        {
            homePosition = new Vector3(-10, -1.5f, 0); // Player's home position
        }
        else if (gameObject.name.Contains("Secretary"))
        {
            homePosition = new Vector3(-10, -1.5f, 0); // Secretary's home position
        }

        // Intermediate position for smooth movement transitions
        intermediatePosition = new Vector3(homePosition.x - 2f, homePosition.y, homePosition.z);

        // Set the initial target position
        targetPosition = transform.position;
    }

    private void Update()
    {
        // Move toward the target position
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void GoHome(float delay)
    {
        StartCoroutine(WalkToHome(delay));
    }

    private IEnumerator WalkToHome(float delay)
    {
        yield return new WaitForSeconds(delay); // Add a delay before moving

        // Step 1: Move to intermediate position
        targetPosition = intermediatePosition;
        yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPosition) < 0.1f);

        // Step 2: Move to home position
        targetPosition = homePosition;
    }

    public void ComeBackToWork(float delay)
    {
        StartCoroutine(WalkToWork(delay));
    }

    private IEnumerator WalkToWork(float delay)
    {
        yield return new WaitForSeconds(delay); // Add a delay before moving

        // Step 1: Move to intermediate position
        targetPosition = intermediatePosition;
        yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPosition) < 0.1f);

        // Step 2: Move to work position
        targetPosition = workPosition;
    }
}
