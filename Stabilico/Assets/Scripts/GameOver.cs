using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private AudioCollection audioCollection;
    private GameData gameData;
    void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
        gameData = FindObjectOfType<GameData>();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioCollection.PlayBGM(audioCollection.gameOver);
    }

    public void StartOver()
    {
        audioCollection.PlaySFX(audioCollection.UIButtonClick);
        SceneManager.LoadScene("Main Menu");
    }
}
