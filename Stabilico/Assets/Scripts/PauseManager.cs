using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // The Pause Panel UI
    [SerializeField] private GameObject settingsPanel; // The Settings Panel UI
    public Image transitionImage;  // Ensure this is assigned in the editor
    public float transitionTime = 1.0f;

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
        StartCoroutine(TransitionToScene("Main Menu"));
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
