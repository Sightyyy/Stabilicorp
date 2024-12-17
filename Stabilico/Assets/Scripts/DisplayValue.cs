using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayValue : MonoBehaviour
{
    private GameData gameData;
    private DecisionManager decisionManager;
    public TextMeshProUGUI Budget;
    public TextMeshProUGUI Worker;
    public TextMeshProUGUI Happiness;
    public TextMeshProUGUI Trust;

    void Start()
    {
        decisionManager = FindObjectOfType<DecisionManager>();
        gameData = GameData.instance; // Mengambil instance GameData
    }

    void Update()
    {
        // Memperbarui nilai setiap kali frame
        if (gameData != null)
        {
            Budget.text = gameData.playerFinance.ToString();
            Worker.text = gameData.workerAmount.ToString();
            Happiness.text = gameData.workerHappiness.ToString(); // Menggunakan format desimal untuk 2 angka di belakang koma
            Trust.text = gameData.clientTrust.ToString(); // Menggunakan format desimal untuk 2 angka di belakang koma
        }
    }
}
