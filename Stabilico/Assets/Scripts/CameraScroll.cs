using UnityEngine;
using UnityEngine.UI;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Reference to the camera
    [SerializeField] private Button leftButton; // Left arrow button
    [SerializeField] private Button rightButton; // Right arrow button

    private float scrollAmount = 17.76f; // Distance the camera moves per click
    private float minX = 0f; // Minimum x position of the camera
    private float maxX = 17.76f; // Maximum x position of the camera
    private float smoothTime = 0.3f; // Time it takes to reach the target position

    private float targetX; // Target x position of the camera
    private float velocity = 0f; // Used by SmoothDamp for smoothing

    private void Start()
    {
        // Initialize the target position to the camera's current position
        targetX = mainCamera.transform.position.x;
        UpdateButtonVisibility();
    }

    private void Update()
    {
        // Smoothly move the camera towards the target position
        float newX = Mathf.SmoothDamp(mainCamera.transform.position.x, targetX, ref velocity, smoothTime);
        mainCamera.transform.position = new Vector3(newX, mainCamera.transform.position.y, mainCamera.transform.position.z);

        // Update button visibility dynamically
        UpdateButtonVisibility();
    }

    public void ScrollLeft()
    {
        // Decrease target position, clamped within bounds
        targetX = Mathf.Clamp(targetX - scrollAmount, minX, maxX);
    }

    public void ScrollRight()
    {
        // Increase target position, clamped within bounds
        targetX = Mathf.Clamp(targetX + scrollAmount, minX, maxX);
    }

    private void UpdateButtonVisibility()
    {
        // Hide the left button if the camera is at the minimum x position
        leftButton.gameObject.SetActive(targetX > minX);

        // Hide the right button if the camera is at the maximum x position
        rightButton.gameObject.SetActive(targetX < maxX);
    }
}
