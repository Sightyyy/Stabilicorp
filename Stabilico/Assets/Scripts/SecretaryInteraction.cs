using System.Collections;
using UnityEngine;
using TMPro; // For TextMeshProUGUI
using UnityEngine.UI;

public class SecretaryInteraction : MonoBehaviour
{
    [SerializeField] private Image bubbleChatImage; // UI Image for the bubble chat
    [SerializeField] private TextMeshProUGUI bubbleChatText; // TextMeshProUGUI for chat text
    [SerializeField] private GameObject yesNoButtons; // Container for Yes/No buttons

    private PlayerAndSecretaryBehavior playerAndSecretaryBehavior;
    private DayAndTimeManager dayAndTimeManager;
    private bool isClicked = false;

    private void Awake()
    {
        dayAndTimeManager = FindObjectOfType<DayAndTimeManager>();
        playerAndSecretaryBehavior = FindObjectOfType<PlayerAndSecretaryBehavior>();
    }

    private void Start()
    {
        // Ensure the bubble chat and buttons are initially hidden
        bubbleChatImage.gameObject.SetActive(false);
        yesNoButtons.SetActive(false);
    }

    private void Update()
    {
        // Prevent bubble chat from showing while the secretary is moving
        if (playerAndSecretaryBehavior.isMoving && bubbleChatImage.gameObject.activeSelf)
        {
            HideBubbleChat();
        }
    }

    // Triggered when the mouse hovers over the secretary
    private void OnMouseEnter()
    {
        if(dayAndTimeManager.isTired && !playerAndSecretaryBehavior.isMoving)
        {
            ShowBubbleChat("Wew... I'm tired, maybe tomorrow.");
        }

        if (dayAndTimeManager.isHiring && !playerAndSecretaryBehavior.isMoving)
        {
            ShowBubbleChat("Please wait, I am currently hiring.");
        }
        else if(!dayAndTimeManager.isTired && !playerAndSecretaryBehavior.isMoving)
        {
            ShowBubbleChat("Do you need help in anything?");
        }
    }

    // Triggered when the mouse stops hovering over the secretary
    private void OnMouseExit()
    {
        if(isClicked == false)
        {
            HideBubbleChat();
        }
    }

    // Triggered when the secretary is clicked
    public void OnMouseDown()
    {
        Debug.Log("Talked!");
        if (playerAndSecretaryBehavior.isMoving || dayAndTimeManager.isHiring || dayAndTimeManager.isTired) return; // Prevent interaction while moving

        isClicked = true;

        ShowBubbleChat("Do you want to hire workers?");
        yesNoButtons.SetActive(true); // Show Yes/No buttons for interaction
    }

    // Called when the "Yes" button is clicked
    public void OnYesClicked()
    {
        HideBubbleChat();
        ShowBubbleChat("Alright hiring in progress!");
        dayAndTimeManager.isHiring = true;

        // Hide buttons after selection
        yesNoButtons.SetActive(false);
        isClicked = false;
    }

    // Called when the "No" button is clicked
    public void OnNoClicked()
    {
        HideBubbleChat();
        ShowBubbleChat("Okay!");

        // Hide buttons after selection
        yesNoButtons.SetActive(false);
    }

    private void ShowBubbleChat(string message)
    {
        bubbleChatImage.gameObject.SetActive(true);
        bubbleChatText.text = message;
    }

    private void HideBubbleChat()
    {
        bubbleChatImage.gameObject.SetActive(false);
        bubbleChatText.text = "";
        yesNoButtons.SetActive(false);
    }
}
