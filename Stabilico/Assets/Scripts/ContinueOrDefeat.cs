using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueOrDefeat : MonoBehaviour
{
    private GameData gameData;
    private DayAndTimeManager dayAndTimeManager;

    void Start()
    {
        dayAndTimeManager = FindObjectOfType<DayAndTimeManager>();
        gameData = GameData.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameData.workerAmount == 0)
        {
            gameData.ResetData();
            SceneManager.LoadScene("Game Over");
        }

        if(gameData.playerFinance == 0 && gameData.workerHappiness == 0)
        {
            dayAndTimeManager.isVeryDeficient = true;
        }
        else
        {
            dayAndTimeManager.isVeryDeficient = false;
        }

        if(gameData.playerFinance == 0 && dayAndTimeManager.isVeryDeficient == false && dayAndTimeManager.isDeficient == false)
        {
            dayAndTimeManager.isUnhappy = true;
        }
        else
        {
            dayAndTimeManager.isUnhappy = false;
        }

        if(gameData.workerHappiness == 0 && dayAndTimeManager.isVeryDeficient == false)
        {
            dayAndTimeManager.isDeficient = true;
        }
        else
        {
            dayAndTimeManager.isDeficient = false;
        }
    }
}
