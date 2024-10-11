using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Drag and drop TMP text here in the Inspector
    public Button playButton; // Drag and drop the play button here in the Inspector
    public Button yesButton; // Drag and drop the yes button here in the Inspector
    public Button noButton;

    private bool hasPressedPlay = false;
    private bool isFirstConfirmation = true;

    void Start()
    {
        // Set the initial text
        textMeshPro.text = "Would you like to quit the game?";

        // Add listener to the play button
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonPressed);
        }

        // Add listener to the yes button
        if (yesButton != null)
        {
            yesButton.onClick.AddListener(OnYesButtonPressed);
        }

        if (noButton != null)
        {
            noButton.onClick.AddListener(OnNoButtonPressed);
        }
    }

    void OnPlayButtonPressed()
    {
        hasPressedPlay = true;
        isFirstConfirmation = true; // Reset to initial state when play is pressed
        textMeshPro.text = "Would you like to quit the game?";
    }

    void OnYesButtonPressed()
    {
        if (hasPressedPlay)
        {
            Application.Quit();
        }
        else
        {
            if (isFirstConfirmation)
            {
                isFirstConfirmation = false;
                textMeshPro.text = "Are you sure?\nYou haven't played the game yet";
            }
            else
            {
                Application.Quit();
            }
        }
    }

    void OnNoButtonPressed()
    {
        Start();
    }
}
