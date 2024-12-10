using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueOrDefeat : MonoBehaviour
{
    private GameData gameData;

    void Start()
    {
        gameData = GameData.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameData.playerFinance == 0 && gameData.workerAmount == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
