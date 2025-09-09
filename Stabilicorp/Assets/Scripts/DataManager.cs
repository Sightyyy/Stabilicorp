//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class DataManager : MonoBehaviour
//{
//    public static DataManager Instance { get; private set; }

//    public string PlayerName { get; private set; }
//    public string CompanyName { get; private set; }
//    public int TotalEnemiesKilled { get; private set; }

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }

//        LoadPlayerData();
//    }

//    public void CompleteLevel()
//    {
//        LevelsCompleted++;
//        SavePlayerData();
//        Debug.Log($"Level {LevelsCompleted} completed!");
//    }

//    public void RecordDeath()
//    {
//        TotalDeaths++;
//        SavePlayerData();
//    }

//    public void RecordEnemyKill()
//    {
//        TotalEnemiesKilled++;
//        SavePlayerData();
//    }

//    private void SavePlayerData()
//    {
//        PlayerPrefs.SetInt("LevelsCompleted", LevelsCompleted);
//        PlayerPrefs.SetInt("TotalDeaths", TotalDeaths);
//        PlayerPrefs.SetInt("TotalEnemiesKilled", TotalEnemiesKilled);
//        PlayerPrefs.Save();
//    }

//    private void LoadPlayerData()
//    {
//        LevelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);
//        TotalDeaths = PlayerPrefs.GetInt("TotalDeaths", 0);
//        TotalEnemiesKilled = PlayerPrefs.GetInt("TotalEnemiesKilled", 0);
//    }

//    public void ResetProgress()
//    {
//        LevelsCompleted = 0;
//        TotalDeaths = 0;
//        TotalEnemiesKilled = 0;
//        PlayerPrefs.DeleteAll();
//    }
//}