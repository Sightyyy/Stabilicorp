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
    private AudioCollection audioCollection;
    public GameObject eventHappening; // Reference to the event happening UI element

    public int hasProject = 0;
    private int event7num;
    private int event11num;
    private int event13num;

    private void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }
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
            ),
            new DecisionEvent(
                "Event 3",
                "The water dispenser is broken",
                new Choice("Repair it", new Dictionary<string, int>
                {
                    { "playerFinance", -5 },
                }),
                new Choice("Leave it", new Dictionary<string, int>
                {
                    { "workerHappiness", -5 }
                })
            ),
            new DecisionEvent(
                "Event 4",
                "The workers are sleeping while working",
                new Choice("Wake them & threaten them", new Dictionary<string, int>
                {
                    { "workerHappiness", -15 },
                    { "clientTrust", 5}
                }),
                new Choice("Wake them & gently warn them", new Dictionary<string, int>
                {
                    { "workerHappiness", 10 },
                    { "clientTrust", -20 }
                })
            ),
            new DecisionEvent(
                "Event 5",
                "We need to persuade people to work here",
                new Choice("Dictatorship-like tone", new Dictionary<string, int>
                {
                    { "workerHappiness", -5 }
                }),
                new Choice("Leader-like tone", new Dictionary<string, int>
                {
                    { "workerHappiness", 5 }
                })
            ),
            new DecisionEvent(
                "Event 6",
                "Our server data got leaked to our clients",
                new Choice("Apologize", new Dictionary<string, int>
                {
                    { "playerFinance", -10}
                }),
                new Choice("Eh, it doesn’t affect us", new Dictionary<string, int>
                {
                    { "playerFinance", -10 },
                    { "clientTrust", -20 }
                })
            ),
            new DecisionEvent(
                "Event 7",
                "An investor wants to invest to our company",
                new Choice("Gladly accept it", new Dictionary<string, int>
                {
                    { "playerFinance", 10}
                }),
                new Choice("Demand increased budget", new Dictionary<string, int>
                {
                    { "playerFinance", event7num },
                })
            ),
            new DecisionEvent(
                "Event 8",
                "Our server hub needs to be upgraded",
                new Choice("For the sake of data safety", new Dictionary<string, int>
                {
                    { "playerFinance", -15 },
                    { "clientTrust", 20 }
                }),
                new Choice("We need the money for other things", new Dictionary<string, int>
                {
                    { "clientTrust", -10 }
                })
            ),
            new DecisionEvent(
                "Event 9",
                "Our server hub needs to be upgraded",
                new Choice("For the sake of data safety", new Dictionary<string, int>
                {
                    { "playerFinance", -15 },
                    { "clientTrust", 20 }
                }),
                new Choice("We need the money for other things", new Dictionary<string, int>
                {
                    { "clientTrust", -10 }
                })
            ),
            new DecisionEvent(
                "Event 10",
                "The client's CEO invites you to dinner at a fancy restaurant",
                new Choice("Accepts it", new Dictionary<string, int>
                {
                    { "clientTrust", 5 }
                }),
                new Choice("Reject it", new Dictionary<string, int>
                {
                    { "clientTrust", -5 }
                })
            ),
            new DecisionEvent(
                "Event 11",
                "Some worker wanted to resign and join other company",
                new Choice("Go ahead. It's easy to look for replacement", new Dictionary<string, int>
                {
                    { "workerAmount", event11num },
                    { "workerHappiness", -10 }
                }),
                new Choice("I wish you all the best", new Dictionary<string, int>
                {
                    { "workerAmount", event11num }
                })
            ),
            new DecisionEvent(
                "Event 12",
                "A worker falls ill",
                new Choice("Give paid leave", new Dictionary<string, int>
                {
                    { "playerFinance", -5 },
                    { "workerHappiness", 5 }
                }),
                new Choice("Threaten on firing", new Dictionary<string, int>
                {
                    { "playerFinance", 5 },
                    { "workerHappiness", -10 }
                })
            ),
            new DecisionEvent(
                "Event 13",
                "We need to look for more workers",
                new Choice("Search for the best out there", new Dictionary<string, int>
                {
                    { "playerFinance", -30 },
                    { "clientTrust", 15 },
                    { "workerAmount", 10 }
                }),
                new Choice("Threaten on firing", new Dictionary<string, int>
                {
                    { "playerFinance", -10 },
                    { "workerAmount", event13num }
                })
            ),
            new DecisionEvent(
                "Event 14",
                "While having a meeting, a worker made a system error",
                new Choice("Demand compensation", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 },
                    { "playerFinance", 5 }
                }),
                new Choice("Threaten on firing", new Dictionary<string, int>
                {
                    { "workerHappiness", -30 },
                    { "playerFinance", 15 }
                })
            ),
            new DecisionEvent(
                "Event 15",
                "The workers are fighting to show who’s better in leading the team",
                new Choice("Leave them be", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                }),
                new Choice("Separate them", new Dictionary<string, int>
                {
                    { "workerHappiness", 0 }
                })
            ),
            new DecisionEvent(
                "Event 16",
                "The stock market price is not with your side today",
                new Choice("Aww shucks", new Dictionary<string, int>
                {
                    { "playerFinance", -10 }
                }),
                new Choice("Secretly cut worker’s salary for the month", new Dictionary<string, int>
                {
                    { "workerHappiness", -25 }
                })
            ),
            new DecisionEvent(
                "Event 17",
                "The stock market is with your side today",
                new Choice("Yippee!", new Dictionary<string, int>
                {
                    { "playerFinance", 10 }
                }),
                new Choice("Try other stock", new Dictionary<string, int>
                {
                    { "playerFinance", -15}
                })
            ),
            new DecisionEvent(
                "Event 18",
                "A client from another company angrily reports inappropriate behavior from one of your workers",
                new Choice("Discipline the worker", new Dictionary<string, int>
                {
                    { "clientTrust", -5 },
                    { "workerHappiness", -10 },
                }),
                new Choice("Do nothing", new Dictionary<string, int>
                {
                    { "clientTrust", -30}
                })
            ),
            new DecisionEvent(
                "Event 19",
                "It's your birthday and you want to throw a party at a nearby bar",
                new Choice("Invite the clients", new Dictionary<string, int>
                {
                    { "clientTrust", 15 },
                    { "workerHappiness", 15 }
                }),
                new Choice("Invite the clients’ CEO", new Dictionary<string, int>
                {
                    { "clientTrust", 25 },
                    { "workerHappiness", 15 }
                })
            ),
            new DecisionEvent(
                "Event 20",
                "All of your worker invite you to a drinking party",
                new Choice("Accepts it", new Dictionary<string, int>
                {
                    { "playerFinance", -20 },
                    { "workerHappiness", 20 }
                }),
                new Choice("Politely reject it", new Dictionary<string, int>
                {
                    { "workerHappiness", -5 }
                })
            ),
            new DecisionEvent(
                "Event 21",
                "The AC is leaking",
                new Choice("Call the repairman", new Dictionary<string, int>
                {
                    { "playerFinance", -5 }
                }),
                new Choice("It’ll dry out soon enough", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                })
            ),
            new DecisionEvent(
                "Event 22",
                "The company’s PC is old enough to be replaced",
                new Choice("Replace all", new Dictionary<string, int>
                {
                    { "playerFinance", -10 },
                    { "workerHappiness", 5 }
                }),
                new Choice("If it still works, just use it", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                })
            ),
            new DecisionEvent(
                "Event 23",
                "The client’s CEO wants to transfer their workers to our company",
                new Choice("We might need extra hand", new Dictionary<string, int>
                {
                    { "workerAmount", 25 },
                    { "clientTrust", 10 }
                }),
                new Choice("We don't have any space for them", new Dictionary<string, int>
                {
                    { "clientTrust", -5 }
                })
            ),
            new DecisionEvent(
                "Event 24",
                "We ran out of papers",
                new Choice("Buy more", new Dictionary<string, int>
                {
                    { "playerFinance", -5 }
                }),
                new Choice("We have technology", new Dictionary<string, int>
                {
                    { "clientTrust", 0 }
                })
            ),
            new DecisionEvent(
                "Event 25",
                "The internet subscription has ended",
                new Choice("We really need the internet", new Dictionary<string, int>
                {
                    { "playerFinance", -10 }
                }),
                new Choice("We can do it without internet", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                })
            ),
            new DecisionEvent(
                "Event 26",
                "Taxes are rising",
                new Choice("We have no choice", new Dictionary<string, int>
                {
                    { "playerFinance", -15 }
                }),
                new Choice("We also need money, not just the government", new Dictionary<string, int>
                {
                    { "workerHappiness", -20 },
                    { "workerAmount", -10 }
                })
            ),
            new DecisionEvent(
                "Event 27",
                "Your company is hit by the inflation wave",
                new Choice("Worker’s fate", new Dictionary<string, int>
                {
                    { "workerHappiness", -50 },
                    { "workerAmount", -50}
                }),
                new Choice("Wage concern", new Dictionary<string, int>
                {
                    { "playerFinance", -50 }
                })
            ),
            new DecisionEvent(
                "Event 28",
                "Your company sales is below average",
                new Choice("Punish the worker", new Dictionary<string, int>
                {
                    { "workerHappiness", -20 }
                }),
                new Choice("Fire the incompetents", new Dictionary<string, int>
                {
                    { "workerHappiness", -30 }
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
        int randomNum = Random.Range(0, 100);
        if(randomNum >= 81 && randomNum <= 90)
        {
            event7num = 15;
        }
        else if(randomNum >= 91 && randomNum <= 97)
        {
            event7num = 25;
        }
        else if(randomNum >= 98 && randomNum <= 100)
        {
            event7num = 30;
        }
        else
        {
            event7num = 0;
        }

        if(randomNum >= 35 && randomNum <= 66)
        {
            event11num = -15;
        }
        else if(randomNum >= 67 && randomNum <= 100)
        {
            event11num = -20;
        }
        else
        {
            event11num = -10;
        }

        if(randomNum <= 50)
        {
            event13num = 25;
        }
        else
        {
            event13num = 30;
        }

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
        audioCollection.PlaySFX(audioCollection.UIButtonClick);
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
                    EnsureNonNegative(ref GameData.instance.workerAmount);
                    Debug.Log("Workers = " + GameData.instance.workerAmount);
                    break;
                case "workerHappiness":
                    GameData.instance.workerHappiness += change.Value;
                    EnsureNonNegative(ref GameData.instance.workerHappiness);
                    Debug.Log("Happiness = " + GameData.instance.workerHappiness);
                    break;
                case "clientTrust":
                    GameData.instance.clientTrust += change.Value;
                    EnsureNonNegative(ref GameData.instance.clientTrust);
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
    private void EnsureNonNegative(ref int value)
    {
        if (value < 0)
        {
            value = 0;
        }
    }


    // Method to show the event happening UI
    public void TriggerEventHappening()
    {
        eventHappening.SetActive(true);
    }
}
