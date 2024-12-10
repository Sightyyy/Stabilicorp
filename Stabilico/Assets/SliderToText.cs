using UnityEngine;
using TMPro;

public class SliderToText : MonoBehaviour
{
    private GameData gameData;

    [Header("TextMeshPro Texts")]
    public TMP_Text text1; // Untuk playerFinance
    public TMP_Text text2; // Untuk workerAmount
    public TMP_Text text3; // Untuk workerHappiness
    public TMP_Text text4; // Untuk clientTrust

    void Start()
    {
        gameData = GameData.instance;
        UpdateText();
    }

    public void UpdateText()
    {
        // Update TMP_Text dengan nilai dari GameData
        text1.text = gameData.playerFinance.ToString();
        text2.text = gameData.workerAmount.ToString();
        text3.text = gameData.workerHappiness.ToString();
        text4.text = gameData.clientTrust.ToString();
    }

    // Contoh fungsi untuk mengubah nilai dan memperbarui teks
    public void ModifyPlayerFinance(int amount)
    {
        gameData.playerFinance += amount;
        UpdateText();
    }

    public void ModifyWorkerAmount(int amount)
    {
        gameData.workerAmount += amount;
        UpdateText();
    }

    public void ModifyWorkerHappiness(int amount)
    {
        gameData.workerHappiness += amount;
        UpdateText();
    }

    public void ModifyClientTrust(int amount)
    {
        gameData.clientTrust += amount;
        UpdateText();
    }
}
