using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecisionManager : MonoBehaviour
{
    public List<DecisionEvent> events; // All events (persistent list)
    private List<DecisionEvent> availableEvents; // Track available events
    private DecisionEvent currentEvent;
    private ContinueOrDefeat continueOrDefeat;

    public TextMeshProUGUI eventDescription; // UI text to show event description
    public UnityEngine.UI.Button choice1Button; // Button for choice 1
    public UnityEngine.UI.Button choice2Button; // Button for choice 2
    public UnityEngine.UI.Button choice3Button;
    public UnityEngine.UI.Button choice4Button;

    public DayAndTimeManager dayAndTimeManager; // Reference to the DayAndTimeManager script
    private AudioCollection audioCollection;
    public GameObject eventHappening; // Reference to the event happening UI element
    private SecretaryInteraction secretaryInteraction;

    public int hasProject = 0;
    private int event7num;
    private int event11num;
    private int event13num;

    private void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
        secretaryInteraction = FindObjectOfType<SecretaryInteraction>();
        continueOrDefeat = FindAnyObjectByType<ContinueOrDefeat>();
    }
    void Start()
    {
        // Initialize events
        events = new List<DecisionEvent>
        {
            new DecisionEvent(
                "Event 1",
                "The water dispenser is broken",
                new Choice("Repair it", new Dictionary<string, int>
                {
                    { "playerFinance", -5 },
                }),
                new Choice("Leave it", new Dictionary<string, int>
                {
                    { "workerHappiness", -5 }
                }),
                new Choice("Buy a new one", new Dictionary<string, int>
                {
                    { "playerFinance", -10 }
                }),
                new Choice("Upgrade it", new Dictionary<string, int>
                {
                    { "playerFinance", -15 }
                })
            ),
            new DecisionEvent(
                "Event 2",
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
                }),
                new Choice("Let them sleep", new Dictionary<string, int>
                {
                    { "workerHappiness", 20 },
                    { "clientTrust", -50}
                }),
                new Choice("Instantly fire them after waking them", new Dictionary<string, int>
                {
                    { "workerHappiness", -40 },
                    { "clientTrust", 30},
                    { "workerAmount", -5}
                })
            ),
            new DecisionEvent(
                "Event 3",
                "We need to persuade people to work here",
                new Choice("Dictatorship-like tone", new Dictionary<string, int>
                {
                    { "workerHappiness", -5 }
                }),
                new Choice("Leader-like tone", new Dictionary<string, int>
                {
                    { "workerHappiness", 5 }
                }),
                new Choice("Pessimistic tone", new Dictionary<string, int>
                {
                    { "workerHappiness", -15 }
                }),
                new Choice("Say nothing", new Dictionary<string, int>
                {
                    { "clientTrust", -5 }
                })
            ),
            new DecisionEvent(
                "Event 4",
                "Our server data got leaked to our clients",
                new Choice("Apologize", new Dictionary<string, int>
                {
                    { "playerFinance", -10}
                }),
                new Choice("Eh, it doesn’t affect us", new Dictionary<string, int>
                {
                    { "playerFinance", -10 },
                    { "clientTrust", -20 }
                }),
                new Choice("Frame the clients", new Dictionary<string, int>
                {
                    { "clientTrust", -60}
                }),
                new Choice("It's not my responsibility", new Dictionary<string, int>
                {
                    { "clientTrust", -100},
                    { "workerHappiness", -40 }
                })
            ),
            new DecisionEvent(
                "Event 5",
                "An investor wants to invest to our company",
                new Choice("Gladly accept it", new Dictionary<string, int>
                {
                    { "playerFinance", 15 }
                }),
                new Choice("Demand increased budget", new Dictionary<string, int>
                {
                    { "playerFinance", 25 },
                }),
                new Choice("Politely refuse", new Dictionary<string, int>
                {
                    { "playerFinance", 0 }
                }),
                new Choice("Accept half of it", new Dictionary<string, int>
                {
                    { "playerFinance", 10 }
                })
            ),
            new DecisionEvent(
                "Event 6",
                "Our server hub needs to be upgraded",
                new Choice("For the sake of data safety", new Dictionary<string, int>
                {
                    { "playerFinance", -15 },
                    { "clientTrust", 20 }
                }),
                new Choice("We need the money for other things", new Dictionary<string, int>
                {
                    { "clientTrust", -10 }
                }),
                new Choice("A server hub? We don’t need it for now", new Dictionary<string, int>
                {
                    { "workerHappiness", -25 },
                    { "clientTrust", -40 }
                }),
                new Choice("Another client could help us with this problem", new Dictionary<string, int>
                {
                    { "playerFinance", -35 },
                    { "clientTrust", 5 }
                })
            ),
            new DecisionEvent(
                "Event 7",
                "The client's CEO invites you to dinner at a fancy restaurant",
                new Choice("Accepts it", new Dictionary<string, int>
                {
                    { "clientTrust", 5 }
                }),
                new Choice("Reject it", new Dictionary<string, int>
                {
                    { "clientTrust", -5 }
                }),
                new Choice("Ask to cover your meal bill", new Dictionary<string, int>
                {
                    { "clientTrust", -10 }
                }),
                new Choice("Offer to cover the meal bill", new Dictionary<string, int>
                {
                    { "clientTrust", 10 },
                    { "playerFinance", -10 }
                })
            ),
            new DecisionEvent(
                "Event 8",
                "Some worker wanted to resign and join other company",
                new Choice("Go ahead. It's easy to look for replacement", new Dictionary<string, int>
                {
                    { "workerAmount", -15 },
                    { "workerHappiness", -10 }
                }),
                new Choice("I wish you all the best", new Dictionary<string, int>
                {
                    { "workerAmount", -15 }
                }),
                new Choice("We still need you", new Dictionary<string, int>
                {
                    { "workerAmount", -15 }
                }),
                new Choice("How about I increase your salary?", new Dictionary<string, int>
                {
                    { "playerFinance", -25 }
                })
            ),
            new DecisionEvent(
                "Event 9",
                "A worker falls ill",
                new Choice("Give paid leave", new Dictionary<string, int>
                {
                    { "playerFinance", -5 },
                    { "workerHappiness", 5 }
                }),
                new Choice("Threaten on firing", new Dictionary<string, int>
                {
                    { "workerHappiness", -15 }
                }),
                new Choice("Visit the worker", new Dictionary<string, int>
                {
                    { "playerFinance", -5 },
                    { "workerHappiness", 10 }
                }),
                new Choice("Fire him", new Dictionary<string, int>
                {
                    { "workerHappiness", -25 },
                    { "workerAmount", -5 }
                })
            ),
            new DecisionEvent(
                "Event 10",
                "We need to look for more workers",
                new Choice("Search for the best out there", new Dictionary<string, int>
                {
                    { "playerFinance", -30 },
                    { "clientTrust", 15 },
                    { "workerAmount", 10 }
                }),
                new Choice("Search for intern", new Dictionary<string, int>
                {
                    { "playerFinance", -10 },
                    { "workerAmount", 5 }
                    // { "workerAmount", event13num }
                }),
                new Choice("Ask for worker transfer from another company", new Dictionary<string, int>
                {
                    { "workerAmount", 10 }
                }),
                new Choice("We have enough workers", new Dictionary<string, int>
                {
                    { "playerFinance", 0 }
                })
            ),
            new DecisionEvent(
                "Event 11",
                "While having a meeting, a worker made a system error",
                new Choice("Demand compensation", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 },
                    { "playerFinance", 25 }
                }),
                new Choice("Fire Him", new Dictionary<string, int>
                {
                    { "workerHappiness", -30 },
                    { "workerAmount", -5 }
                }),
                new Choice("Leave it be", new Dictionary<string, int>
                {
                    { "clientTrust", -15 }
                }),
                new Choice("Apologize", new Dictionary<string, int>
                {
                    { "workerHappiness", 5 },
                    { "clientTrust", -10 }
                })
            ),
            new DecisionEvent(
                "Event 12",
                "The workers are fighting to show who’s better in leading the team",
                new Choice("Leave them be", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                }),
                new Choice("Separate them", new Dictionary<string, int>
                {
                    { "workerHappiness", 0 }
                }),
                new Choice("Join the fight", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                }),
                new Choice("Fire them", new Dictionary<string, int>
                {
                    { "workerHappiness", -15 },
                    { "workerAmount", -10}
                })
            ),
            new DecisionEvent(
                "Event 13",
                "The stock market price is not with your side today",
                new Choice("Aww shucks", new Dictionary<string, int>
                {
                    { "playerFinance", -15 }
                }),
                new Choice("Secretly cut worker’s salary for the month", new Dictionary<string, int>
                {
                    { "workerHappiness", -25 }
                }),
                new Choice("Cut the worker's salary and lie about the reason", new Dictionary<string, int>
                {
                    { "workerHappiness", -40 }
                }),
                new Choice("I still believe fortune is still with me", new Dictionary<string, int>
                {
                    { "playerFinance", -40 }
                })
            ),
            new DecisionEvent(
                "Event 14",
                "The stock market is with your side today",
                new Choice("Yippee!", new Dictionary<string, int>
                {
                    { "playerFinance", 20 }
                }),
                new Choice("Try other stock", new Dictionary<string, int>
                {
                    { "playerFinance", -15}
                }),
                new Choice("Keep investing with extra", new Dictionary<string, int>
                {
                    { "playerFinance", -25 }
                }),
                new Choice("ALL IN!!", new Dictionary<string, int>
                {
                    { "playerFinance", -100 }
                })
            ),
            new DecisionEvent(
                "Event 15",
                "A client from another company angrily reports inappropriate behavior from one of your workers",
                new Choice("Discipline the worker", new Dictionary<string, int>
                {
                    { "clientTrust", -5 },
                    { "workerHappiness", -10 }
                }),
                new Choice("Do nothing", new Dictionary<string, int>
                {
                    { "clientTrust", -30}
                }),
                new Choice("Argue with them", new Dictionary<string, int>
                {
                    { "clientTrust", -65 },
                    { "workerHappiness", -10 }
                }),
                new Choice("Make a scandal about the client’s behavior", new Dictionary<string, int>
                {
                    { "clientTrust", -40 }
                })
            ),
            new DecisionEvent(
                "Event 16",
                "You want to throw a party at a nearby bar",
                new Choice("Invite the clients", new Dictionary<string, int>
                {
                    { "clientTrust", 15 },
                    { "workerHappiness", 15 },
                    { "playerFinance", -30}
                }),
                new Choice("Invite the clients’ CEO", new Dictionary<string, int>
                {
                    { "clientTrust", 25 },
                    { "workerHappiness", 15 },
                    { "playerFinance", -45 }
                }),
                new Choice("Just invite the workers", new Dictionary<string, int>
                {
                    { "playerFinance", -15 },
                    { "workerHappiness", 25 },
                    { "clientTrust", -5 }
                }),
                new Choice("Don’t invite anybody", new Dictionary<string, int>
                {
                    { "clientTrust", -5 },
                    { "workerHappiness", -10 },
                    { "playerFinance", -5 }
                })
            ),
            new DecisionEvent(
                "Event 17",
                "All of your worker invite you to a drinking party",
                new Choice("Accepts it", new Dictionary<string, int>
                {
                    { "playerFinance", -20 },
                    { "workerHappiness", 20 }
                }),
                new Choice("Politely reject it", new Dictionary<string, int>
                {
                    { "workerHappiness", -5 }
                }),
                new Choice("Make them pay your bill", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                }),
                new Choice("Prohibit them on drinking", new Dictionary<string, int>
                {
                    { "workerHappiness", -20 }
                })
            ),
            new DecisionEvent(
                "Event 18",
                "The AC is leaking",
                new Choice("Call the repairman", new Dictionary<string, int>
                {
                    { "playerFinance", -5 }
                }),
                new Choice("It’ll dry out soon enough", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                }),
                new Choice("Replace it", new Dictionary<string, int>
                {
                    { "playerFinance", -15 }
                }),
                new Choice("Change it with a standing fan for a while", new Dictionary<string, int>
                {
                    { "playerFinance", -10 },
                    { "workerHappiness", -5}
                })
            ),
            new DecisionEvent(
                "Event 19",
                "The company’s PC is old enough to be replaced",
                new Choice("Replace all", new Dictionary<string, int>
                {
                    { "playerFinance", -10 },
                    { "workerHappiness", 5 }
                }),
                new Choice("If it still works, just use it", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                }),
                new Choice("Upgrade it", new Dictionary<string, int>
                {
                    { "playerFinance", -30 },
                    { "workerHappiness", 20 }
                }),
                new Choice("Let other company do our work", new Dictionary<string, int>
                {
                    { "playerFinance", -50 },
                    { "clientTrust", -100 }
                })
            ),
            new DecisionEvent(
                "Event 20",
                "The client’s CEO wants to transfer their workers to our company",
                new Choice("We might need extra hand", new Dictionary<string, int>
                {
                    { "workerAmount", 25 },
                    { "clientTrust", 10 }
                }),
                new Choice("We don't have any space for them", new Dictionary<string, int>
                {
                    { "clientTrust", -5 }
                }),
                new Choice("We only have a little space for them", new Dictionary<string, int>
                {
                    { "workerAmount", 10 }
                }),
                new Choice("We need more", new Dictionary<string, int>
                {
                    { "workerAmount", 35 }
                })
            ),
            new DecisionEvent(
                "Event 21",
                "We ran out of paper",
                new Choice("Buy more", new Dictionary<string, int>
                {
                    { "playerFinance", -5 }
                }),
                new Choice("We have technology", new Dictionary<string, int>
                {
                    { "clientTrust", 0 }
                }),
                new Choice("Re-use the old paper", new Dictionary<string, int>
                {
                    { "workerHappiness", -5 },
                    { "clientTrust", -5 }
                }),
                new Choice("Buy books", new Dictionary<string, int>
                {
                    { "playerFinance", -15 }
                })
            ),
            new DecisionEvent(
                "Event 22",
                "The internet subscription has ended",
                new Choice("We really need the internet", new Dictionary<string, int>
                {
                    { "playerFinance", -10 }
                }),
                new Choice("We can do it without internet", new Dictionary<string, int>
                {
                    { "workerHappiness", -10 }
                }),
                new Choice("Subscribe to the fastest one", new Dictionary<string, int>
                {
                    { "playerFinance", -40 }
                }),
                new Choice("Change the provider", new Dictionary<string, int>
                {
                    { "playerFinance", -25 },
                    { "workerHappiness", -5 }
                })
            ),
            new DecisionEvent(
                "Event 23",
                "Taxes are rising",
                new Choice("We have no choice", new Dictionary<string, int>
                {
                    { "playerFinance", -15 }
                }),
                new Choice("We also need money, not just the government", new Dictionary<string, int>
                {
                    { "workerHappiness", -20 },
                    { "workerAmount", -10 }
                }),
                new Choice("Cut worker’s salary", new Dictionary<string, int>
                {
                    { "workerHappiness", -50},
                    { "workerAmount", -15 }
                }),
                new Choice("Evade the tax", new Dictionary<string, int>
                {
                    { "playerFinance", -100 },
                    { "workerAmount", -100 },
                    { "evadeTax", 1}
                })
            ),
            new DecisionEvent(
                "Event 24",
                "Your company is hit by the inflation wave",
                new Choice("Worker’s fate", new Dictionary<string, int>
                {
                    { "workerHappiness", -50 },
                    { "workerAmount", -50 }
                }),
                new Choice("Wage concern", new Dictionary<string, int>
                {
                    { "playerFinance", -50 }
                }),
                new Choice("Do nothing", new Dictionary<string, int>
                {
                    { "workerHappiness", -25 },
                    { "workerAmount", -25 },
                    { "playerFinance", -20 }
                }),
                new Choice("Pressure the clients for increased budget", new Dictionary<string, int>
                {
                    { "clientTrust", -80 },
                    { "playerFinance", 15}
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
                    { "workerHappiness", -30 },
                    { "workerAmount", -15}
                }),
                new Choice("Let it be", new Dictionary<string, int>
                {
                    { "playerFinance", -10 }
                }),
                new Choice("Cover the rumors", new Dictionary<string, int>
                {
                    { "playerFinance", -10 }
                })
            )
        };

        // Initially hide the event happening object
        eventHappening.SetActive(false);

        // Initialize available events list as a copy of all events
        availableEvents = new List<DecisionEvent>(events);

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
        // if(randomNum >= 81 && randomNum <= 90)
        // {
        //     event7num = 15;
        // }
        // else if(randomNum >= 91 && randomNum <= 97)
        // {
        //     event7num = 25;
        // }
        // else if(randomNum >= 98 && randomNum <= 100)
        // {
        //     event7num = 30;
        // }
        // else
        // {
        //     event7num = 0;
        // }

        // if(randomNum >= 35 && randomNum <= 66)
        // {
        //     event11num = -15;
        // }
        // else if(randomNum >= 67 && randomNum <= 100)
        // {
        //     event11num = -20;
        // }
        // else
        // {
        //     event11num = -10;
        // }

        // if(randomNum <= 50)
        // {
        //     event13num = 25;
        // }
        // else
        // {
        //     event13num = 30;
        // }

        if (availableEvents.Count == 0)
        {
            // Repopulate the list if all events have been used
            availableEvents = new List<DecisionEvent>(events);
        }

        // Select a random event
        int randomIndex = Random.Range(0, events.Count);
        currentEvent = availableEvents[randomIndex];
        // events.RemoveAt(randomIndex);

        // Update UI
        eventDescription.text = currentEvent.description;
        choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.choice1.choiceText;
        choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.choice2.choiceText;
        choice3Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.choice3.choiceText;
        choice4Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.choice4.choiceText;

        // Add button listeners
        choice1Button.onClick.RemoveAllListeners();
        choice1Button.onClick.AddListener(() => OnChoiceSelected(currentEvent.choice1));
        choice2Button.onClick.RemoveAllListeners();
        choice2Button.onClick.AddListener(() => OnChoiceSelected(currentEvent.choice2));
        choice3Button.onClick.RemoveAllListeners();
        choice3Button.onClick.AddListener(() => OnChoiceSelected(currentEvent.choice3));
        choice4Button.onClick.RemoveAllListeners();
        choice4Button.onClick.AddListener(() => OnChoiceSelected(currentEvent.choice4));
    }

    void OnChoiceSelected(Choice choice)
    {
        audioCollection.PlaySFX(audioCollection.UIButtonClick);
        ApplyStatChanges(choice.statChanges);
        dayAndTimeManager.ResumeTimeBar(); // Resume the time bar after making a choice

        // Hide the event happening UI again
        eventHappening.SetActive(false);
        secretaryInteraction.enabled = true;
        
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
                case "evadeTax":
                    GameData.instance.evadeTax += change.Value;
                    Debug.Log("TAX EVASION ALERT!!");
                    break;
                default:
                    Debug.LogWarning($"Unknown stat: {change.Key}");
                    break;
            }
        }

        // Log updated stats (optional)
        Debug.Log($"Updated Stats: Finance={GameData.instance.playerFinance}, Workers={GameData.instance.workerAmount}, Happiness={GameData.instance.workerHappiness}, Trust={GameData.instance.clientTrust}");
    }
    public void EnsureNonNegative(ref int value)
    {
        if (value < 0)
        {
            value = 0;
        }
        if (value > 100)
        {
            value = 100;
        }
    }


    // Method to show the event happening UI
    public void TriggerEventHappening()
    {
        eventHappening.SetActive(true);
    }
}
