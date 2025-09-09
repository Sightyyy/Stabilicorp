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
        transitionImage.gameObject.SetActive(true);
        StartCoroutine(TransitionOpening());        
        audioCollection.PlayBGM(audioCollection.mainMenu);
    }

    public void PlayGame(string sceneName)
    {
        if (PlayerPrefs.HasKey("CeoName") && !string.IsNullOrEmpty(PlayerPrefs.GetString("CeoName")))
        {
            sceneName = "Game Content 2";
        }
        StartCoroutine(TransitionToScene(sceneName));
    }

    public void QuitGame()
    {
        StartCoroutine(TransitionAndQuit());
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        yield return StartCoroutine(FadeToBlack());
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator TransitionOpening()
    {
        yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator TransitionAndQuit()
    {
        yield return StartCoroutine(FadeToBlack());
        Application.Quit();
    }

    private IEnumerator FadeToBlack()
    {
        transitionImage.gameObject.SetActive(true);  
        
        for (float t = 0.0f; t < transitionTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / transitionTime);
            SetImageAlpha(alpha);
            yield return null;
        }
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
        SetImageAlpha(0);
        transitionImage.gameObject.SetActive(false);  
    }

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
