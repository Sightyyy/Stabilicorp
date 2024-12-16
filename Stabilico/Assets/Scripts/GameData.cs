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
    public int evadeTax;

    // New variables for date and time
    public int currentDay;       // Indeks hari (0 = Senin, dst.)
    public int currentMonth;     // Indeks bulan (0 = Januari, dst.)
    public int currentDate;      // Tanggal (1-31)
    public int currentYear;      // Tahun
    public float timeBarValue;   // Progress bar waktu

    private void Awake()
    {
        playerFinance = 50;
        workerAmount = 20;
        workerHappiness = 50;
        clientTrust = 50;
        evadeTax = 0;
        currentDay = 0;      // Default to Monday
        currentMonth = 0;    // Default to January
        currentDate = 1;          // Default to the 1st
        currentYear = 2024;       // Default to the current year
        timeBarValue = 0f;

        // If there's already an instance, destroy this one to prevent duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("Duplicate GameData instance destroyed.");
            return;
        }

        // Set this as the Singleton instance
        instance = this;

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

        // Save new data
        PlayerPrefs.SetInt("DayIndex", currentDay);
        PlayerPrefs.SetInt("MonthIndex", currentMonth);
        PlayerPrefs.SetInt("Date", currentDate);
        PlayerPrefs.SetInt("Year", currentYear);
        PlayerPrefs.SetFloat("TimeProgressBarValue", timeBarValue);

        PlayerPrefs.Save(); // Ensure the data is written to disk
        Debug.Log("Game data saved.");
    }

    // Method to load game data
    public void LoadData()
    {
        ceoName = PlayerPrefs.GetString("CeoName", "");
        companyName = PlayerPrefs.GetString("CompanyName", "");
        inGameDate = PlayerPrefs.GetInt("InGameDate", 0);
        playerFinance = PlayerPrefs.GetInt("PlayerFinance", 50);
        workerAmount = PlayerPrefs.GetInt("WorkerAmount", 20);
        workerHappiness = PlayerPrefs.GetInt("WorkerHappiness", 50);
        clientTrust = PlayerPrefs.GetInt("ClientTrust", 50);

        // Load new data
        currentDay = PlayerPrefs.GetInt("DayIndex", 0);
        currentMonth = PlayerPrefs.GetInt("MonthIndex", 0);
        currentDate = PlayerPrefs.GetInt("Date", 1);
        currentYear = PlayerPrefs.GetInt("Year", 2024);
        timeBarValue = PlayerPrefs.GetFloat("TimeProgressBarValue", 0f);

        Debug.Log("Game data loaded.");
    }

    // Optional reset data method
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ceoName = "";
        companyName = "";
        inGameDate = 0;
        playerFinance = 50;
        workerAmount = 20;
        workerHappiness = 50;
        clientTrust = 50;
        evadeTax = 0;

        // Reset new data
        currentDay = 0;
        currentMonth = 0;
        currentDate = 1;
        currentYear = 2024;
        timeBarValue = 0f;
    }
}
