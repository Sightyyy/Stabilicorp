using UnityEngine;

public class GameData : MonoBehaviour
{
    // Singleton instance
    public static GameData Instance;

    // Game data variables
    public string ceoName;
    public string companyName;
    public int inGameDate;
    public int playerReputation;
    public int playerFinance;
    public int playerMentalHealth;

    private void Awake()
    {
        // If there's already an instance, destroy this one to prevent duplicates
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Set this as the Singleton instance and make it persist across scenes
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Example of a method to reset all values (optional)
    public void ResetData()
    {
        ceoName = "";
        companyName = "";
        inGameDate = 0;
        playerReputation = 0;
        playerFinance = 0;
        playerMentalHealth = 0;
    }
}
