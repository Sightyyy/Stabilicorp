using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // The Pause Panel UI
    [SerializeField] private GameObject settingsPanel; // The Settings Panel UI
    [SerializeField] private GameObject warningPopUp;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    public Image transitionImage;  // Ensure this is assigned in the editor
    private float transitionTime = 1.0f;
    private int warningStage = 0; // Tracks the stage of the warnings

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
        // Reset to the first stage
        warningStage = 0;

        // Show the initial warning popup
        warningPopUp.SetActive(true);
        warningText.text = "Do you want to quit the game?";

        // Enable buttons and assign listeners
        EnableButtons();
        yesButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(HandleYesClick);
        noButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(HandleNoClick);
    }

    private void ResetButtons()
    {
        // Temporarily disable buttons to prevent multiple clicks
        DisableButtons();
        StartCoroutine(EnableButtonsWithDelay(0.2f));
    }

    private void EnableButtons()
    {
        yesButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        noButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    private void DisableButtons()
    {
        yesButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        noButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    private void Cleanup()
    {
        // Remove listeners and hide the popup
        yesButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(HandleYesClick);
        noButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(HandleNoClick);
        warningPopUp.SetActive(false);
    }

    private void HandleYesClick()
    {
        audioCollection.PlaySFX(audioCollection.UIButtonClick);

        if (warningStage == 0)
        {
            // Player confirmed they want to quit
            warningStage++;
            warningText.text = "Do you want to reset the game?";
            ResetButtons(); // Enable buttons for the next stage
        }
        else if (warningStage == 1)
        {
            // Player confirmed they want to reset
            gameData.ResetData();

            // Hide the pause menu and resume time immediately
            pausePanel.SetActive(false);
            Time.timeScale = 1f;

            // Start the transition to the main menu
            StartCoroutine(TransitionToScene("Main Menu"));

            // Cleanup listeners and popup
            Cleanup();
        }
    }


    private void HandleNoClick()
    {
        audioCollection.PlaySFX(audioCollection.UIButtonClick);

        if (warningStage == 0)
        {
            // Player canceled quitting
            Cleanup();
        }
        else if (warningStage == 1)
        {
            // Player canceled resetting
            gameData.SaveData();
            Time.timeScale = 1f; // Reset time scale
            StartCoroutine(TransitionToScene("Main Menu"));
            Cleanup(); // Cleanup listeners and popup
        }
    }

    private IEnumerator EnableButtonsWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Tidak terpengaruh Time.timeScale
        EnableButtons();
    }
    private IEnumerator TransitionToScene(string sceneName)
    {
        // Fade to black
        yield return StartCoroutine(FadeToBlack());

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Fade from black
        yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator TransitionOpening()
    {
        yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator TransitionAndQuit()
    {
        // Fade to black
        yield return StartCoroutine(FadeToBlack());

        // Quit the game
        Application.Quit();
    }

    private IEnumerator FadeToBlack()
    {
        // Activate the transition image before fading
        transitionImage.gameObject.SetActive(true);  
        
        for (float t = 0.0f; t < transitionTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / transitionTime);
            SetImageAlpha(alpha);
            yield return null;
        }
        
        // Ensure the alpha is set to 1 (fully black)
        SetImageAlpha(1);
    }

    private IEnumerator FadeFromBlack()
    {
        for (float t = 0.0f; t < transitionTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / transitionTime);
            SetImageAlpha(alpha);
            yield return null;
        }
        
        // Ensure the alpha is set to 0 (fully transparent)
        SetImageAlpha(0);
        
        // Deactivate the transition image after fading
        transitionImage.gameObject.SetActive(false);  
    }

    // Helper method to set the alpha (transparency) of the image
    private void SetImageAlpha(float alpha)
    {
        if (transitionImage != null)
        {
            Color color = transitionImage.color;
            color.a = alpha;
            transitionImage.color = color;
        }
        else
        {
            Debug.LogError("Transition Image is not assigned!");
        }
    }
}
