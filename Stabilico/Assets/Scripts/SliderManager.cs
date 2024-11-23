using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ceoNameDisplay;
    [SerializeField] private TextMeshProUGUI companyNameDisplay;
    // Reference to the sliders
    [SerializeField] private Slider financeSlider;
    [SerializeField] private Slider workerAmountSlider;
    [SerializeField] private Slider workerHappinessSlider;
    [SerializeField] private Slider clientTrustSlider;

    private void Start()
    {
        ceoNameDisplay.text = GameData.instance.ceoName;
        companyNameDisplay.text = GameData.instance.companyName;
    }

    private void Update()
    {
        if (GameData.instance != null)
        {
            // Update slider values based on GameData values
            financeSlider.value = GameData.instance.playerFinance;
            workerAmountSlider.value = GameData.instance.workerAmount;
            workerHappinessSlider.value = GameData.instance.workerHappiness;
            clientTrustSlider.value = GameData.instance.clientTrust;
        }
    }
}
