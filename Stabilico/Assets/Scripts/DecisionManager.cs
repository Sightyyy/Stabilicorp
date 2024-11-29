using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecisionManager : MonoBehaviour
{
    public List<DecisionEvent> events; // List of events
    private DecisionEvent currentEvent;

    public TextMeshProUGUI eventDescription; // UI text to show event description
    public UnityEngine.UI.Button choice1Button; // Button for choice 1
    public UnityEngine.UI.Button choice2Button; // Button for choice 2

    public DayAndTimeManager dayAndTimeManager; // Reference to the DayAndTimeManager script
    public GameObject eventHappening; // Reference to the event happening UI element

    public int hasProject = 0;

    void Start()
    {
        // Initialize events
        events = new List<DecisionEvent>
        {
            new DecisionEvent(
                "Event 1",
                "Your company faces a new challenge. Choose wisely!",
                new Choice("Invest in new technology", new Dictionary<string, int>
                {
                    { "playerFinance", -20 },
                    { "clientTrust", 10 }
                }),
                new Choice("Cut costs by reducing benefits", new Dictionary<string, int>
                {
                    { "workerHappiness", -15 },
                    { "playerFinance", 15 }
                })
            ),
            new DecisionEvent(
                "Event 2",
                "A scandal threatens your reputation. How do you respond?",
                new Choice("Public apology", new Dictionary<string, int>
                {
                    { "clientTrust", -5 },
                    { "playerFinance", -10 }
                }),
                new Choice("Deny allegations", new Dictionary<string, int>
                {
                    { "clientTrust", -20 },
                    { "workerHappiness", 5 }
                })
            )
        };

        // Initially hide the event happening object
        eventHappening.SetActive(false);

        ShowNextEvent();
    }

    public void DebugProject()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                hasProject += 1;
                dayAndTimeManager.projectProgressBar.SetActive(true); // Show the Project Progress Bar
                Debug.Log("Project Debugging Started!");
            }
        }
    }

    public void ShowNextEvent()
    {
        if (events.Count == 0)
        {
            Debug.Log("All events completed!");
            return;
        }

        // Select a random event
        int randomIndex = Random.Range(0, events.Count);
        currentEvent = events[randomIndex];
        events.RemoveAt(randomIndex);

        // Update UI
        eventDescription.text = currentEvent.description;
        choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.choice1.choiceText;
        choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.choice2.choiceText;

        // Add button listeners
        choice1Button.onClick.RemoveAllListeners();
        choice1Button.onClick.AddListener(() => OnChoiceSelected(currentEvent.choice1));
        choice2Button.onClick.RemoveAllListeners();
        choice2Button.onClick.AddListener(() => OnChoiceSelected(currentEvent.choice2));
    }

    void OnChoiceSelected(Choice choice)
    {
        ApplyStatChanges(choice.statChanges);
        dayAndTimeManager.ResumeTimeBar(); // Resume the time bar after making a choice

        // Hide the event happening UI again
        eventHappening.SetActive(false);

        ShowNextEvent();
    }

    void ApplyStatChanges(Dictionary<string, int> changes)
    {
        foreach (var change in changes)
        {
            switch (change.Key)
            {
                case "playerFinance":
                    GameData.instance.playerFinance += change.Value;
                    Debug.Log("Finance = " + GameData.instance.playerFinance);
                    break;
                case "workerAmount":
                    GameData.instance.workerAmount += change.Value;
                    Debug.Log("Workers = " + GameData.instance.workerAmount);
                    break;
                case "workerHappiness":
                    GameData.instance.workerHappiness += change.Value;
                    Debug.Log("Happiness = " + GameData.instance.workerHappiness);
                    break;
                case "clientTrust":
                    GameData.instance.clientTrust += change.Value;
                    Debug.Log("Trust = " + GameData.instance.clientTrust);
                    break;
                default:
                    Debug.LogWarning($"Unknown stat: {change.Key}");
                    break;
            }
        }

        // Log updated stats (optional)
        Debug.Log($"Updated Stats: Finance={GameData.instance.playerFinance}, Workers={GameData.instance.workerAmount}, Happiness={GameData.instance.workerHappiness}, Trust={GameData.instance.clientTrust}");
    }

    // Method to show the event happening UI
    public void TriggerEventHappening()
    {
        eventHappening.SetActive(true);
    }
}
