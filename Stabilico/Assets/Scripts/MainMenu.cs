using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image transitionImage;
    public float transitionTime = 1.0f;

    AudioCollection audioCollection;

    private void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }
    private void Start()
    {
        // Pastikan image dimulai dengan tidak terlihat
        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 0);
        audioCollection.PlayBGM(audioCollection.mainMenu);
    }

    public void PlayGame(string sceneName)
    {
        StartCoroutine(TransitionToScene(sceneName));
    }

    public void QuitGame()
    {
        StartCoroutine(TransitionAndQuit());
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

    private IEnumerator TransitionAndQuit()
    {
        // Fade to black
        yield return StartCoroutine(FadeToBlack());

        // Quit the game
        Application.Quit();
    }

    private IEnumerator FadeToBlack()
    {
        for (float t = 0.0f; t < transitionTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / transitionTime);
            transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, alpha);
            yield return null;
        }
        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 1);
    }

    private IEnumerator FadeFromBlack()
    {
        for (float t = 0.0f; t < transitionTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / transitionTime);
            transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, alpha);
            yield return null;
        }
        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 0);
    }
}
