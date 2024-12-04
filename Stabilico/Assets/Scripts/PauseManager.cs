using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // The Pause Panel UI
    [SerializeField] private GameObject settingsPanel; // The Settings Panel UI

    private void Update()
    {
        // Toggle pause when the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Freeze the game
        pausePanel.SetActive(true); // Show the pause panel
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Unfreeze the game
        pausePanel.SetActive(false); // Hide the pause panel
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // Show the settings panel
        pausePanel.SetActive(false); // Hide the pause panel
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Hide the settings panel
        pausePanel.SetActive(true); // Return to the pause panel
    }

    public void SaveGame()
    {
        // // Example of saving data using PlayerPrefs (replace with your save system)
        // PlayerPrefs.SetInt("PlayerScore", currentScore); // Save player's score
        // PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x); // Save player's position
        // PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y); // Save player's position
        // PlayerPrefs.Save(); // Commit changes
        // Debug.Log("Game progress saved!");
    }

    public void QuitToMainMenu()
    {
        // SaveGame(); // Save progress before quitting
        Time.timeScale = 1f; // Reset time scale to normal before quitting
        SceneManager.LoadScene("Main Menu"); // Load the Main Menu scene
    }
}
