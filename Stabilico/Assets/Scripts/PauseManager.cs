using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // The Pause Panel UI
    [SerializeField] private GameObject settingsPanel; // The Settings Panel UI

    private AudioCollection audioCollection;
    private GameData gameData;

    private void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
        gameData = FindObjectOfType<GameData>();
    }

    private void Start()
    {
        audioCollection.PlayBGM(audioCollection.office);
    }

    private void Update()
    {
        // Toggle pause when the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audioCollection.PlaySFX(audioCollection.UIButtonClick);
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Freeze the game
        audioCollection.PauseBGM();
        pausePanel.SetActive(true); // Show the pause panel
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Unfreeze the game
        audioCollection.ResumeBGM();
        audioCollection.PlaySFX(audioCollection.UIButtonClick);
        pausePanel.SetActive(false); // Hide the pause panel
    }

    public void OpenSettings()
    {
        audioCollection.PlaySFX(audioCollection.UIButtonClick);
        settingsPanel.SetActive(true); // Show the settings panel
        pausePanel.SetActive(false); // Hide the pause panel
    }

    public void CloseSettings()
    {
        audioCollection.PlaySFX(audioCollection.UIButtonClick);
        settingsPanel.SetActive(false); // Hide the settings panel
        pausePanel.SetActive(true); // Return to the pause panel
    }

    public void QuitToMainMenu()
    {
        gameData.SaveData();
        Time.timeScale = 1f; // Reset time scale to normal before quitting
        audioCollection.PlaySFX(audioCollection.UIButtonClick);
        SceneManager.LoadScene("Main Menu"); // Load the Main Menu scene
    }
}
