using UnityEngine;

public class StringData : MonoBehaviour
{
    // Singleton instance
    public static StringData instance;

    // Data to pass to GameData
    public string compName;
    public string playerName;
    private GameData gameData;

    private void Update()
    {
        gameData = FindObjectOfType<GameData>();
        if(gameData != null)
        {
            TransferDataToGameData(gameData);
        }
    }

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes
    }

    // Method to pass data to GameData and destroy itself
    private void TransferDataToGameData(GameData gameData)
    {
        if (gameData != null)
        {
            gameData.ceoName = playerName;
            gameData.companyName = compName;
            Debug.Log("Data transferred from StringData to GameData.");
            Destroy(gameObject); // Destroy itself after transferring data
        }
    }
}
