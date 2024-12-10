using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueOrDefeat : MonoBehaviour
{
    private GameData gameData;

    void Start()
    {
        gameData = FindObjectOfType<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameData.playerFinance == 0 || gameData.workerAmount == 0 || gameData.workerHappiness == 0 || gameData.clientTrust == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
