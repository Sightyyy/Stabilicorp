using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFit : MonoBehaviour
{
    public SpriteRenderer background; // Drag the background SpriteRenderer here in the Inspector

    void Start()
    {
        FitCameraToBackground();
    }

    void FitCameraToBackground()
    {
        Camera camera = GetComponent<Camera>();

        // Get the world size of the background sprite
        float backgroundWidth = background.bounds.size.x;
        float backgroundHeight = background.bounds.size.y;

        // Get the screen aspect ratio
        float screenAspect = (float)Screen.width / (float)Screen.height;

        // Calculate camera size to fit the background
        float cameraHeight = backgroundHeight / 2;
        float cameraWidth = backgroundWidth / 2 / screenAspect;

        // Set the orthographic size to the larger value
        camera.orthographicSize = Mathf.Max(cameraHeight, cameraWidth);
        
        // Set the camera position to the center of the background
        camera.transform.position = new Vector3(background.bounds.center.x, background.bounds.center.y, camera.transform.position.z);

    }
}
