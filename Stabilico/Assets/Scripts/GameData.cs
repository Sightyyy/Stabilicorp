using UnityEngine;

public class GameData : MonoBehaviour
{
    // Singleton instance
    public static GameData instance;

    // Game data variables
    public string ceoName;
    public string companyName;
    public int inGameDate;
    public int playerFinance;
    public int workerAmount;
    public int workerHappiness;
    public int clientTrust;

    private void Awake()
    {
        playerFinance = 50;
        workerAmount = 20;
        workerHappiness = 50;
        clientTrust = 50;
        // If there's already an instance, destroy this one to prevent duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Set this as the Singleton instance and make it persist across scenes
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Debug.Log("GameData instance initialized.");
    }

    // Method to save game data
    public void SaveData()
    {
        PlayerPrefs.SetString("CeoName", ceoName);
        PlayerPrefs.SetString("CompanyName", companyName);
        PlayerPrefs.SetInt("InGameDate", inGameDate);
        PlayerPrefs.SetInt("PlayerFinance", playerFinance);
        PlayerPrefs.SetInt("WorkerAmount", workerAmount);
        PlayerPrefs.SetInt("WorkerHappiness", workerHappiness);
        PlayerPrefs.SetInt("ClientTrust", clientTrust);

        PlayerPrefs.Save(); // Ensure the data is written to disk
        Debug.Log("Game data saved.");
    }

    // Example of a method to reset all values (optional)
    public void ResetData()
    {
        ceoName = "";
        companyName = "";
        // inGameDate = 0; // Not sure how to fix this one
        playerFinance = 50;
        workerAmount = 20;
        workerHappiness = 50;
        clientTrust = 50;
    }
}
