using System.Collections;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class SecretaryInteraction : MonoBehaviour
{
    [SerializeField] private GameObject bubbleChatObject; // GameObject for the bubble chat
    [SerializeField] private TextMeshPro bubbleChatText; // TextMeshProUGUI for chat text

    private PlayerAndSecretaryBehavior playerAndSecretaryBehavior;
    private DayAndTimeManager dayAndTimeManager;
    private bool isClicked = false;
    private bool awaitingResponse = false; // To track if the player needs to respond

    private void Awake()
    {
        dayAndTimeManager = FindObjectOfType<DayAndTimeManager>();
        playerAndSecretaryBehavior = FindObjectOfType<PlayerAndSecretaryBehavior>();
    }

    private void Start()
    {
        // Ensure the bubble chat is initially hidden
        bubbleChatObject.SetActive(false);
    }

    private void Update()
    {
        // Prevent bubble chat from showing while the secretary is moving
        if (playerAndSecretaryBehavior.isMoving && bubbleChatObject.activeSelf)
        {
            HideBubbleChat();
        }

        // Handle key inputs for Yes/No responses
        if (awaitingResponse)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                OnYesPressed();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                OnNoPressed();
            }
        }
    }

    private void OnMouseEnter()
    {
        if (!isClicked && !awaitingResponse)
        {
            if (dayAndTimeManager.isBroke)
            {
                ShowBubbleChat("We don't have enough money to hire.");
            }
            else if (dayAndTimeManager.isTired && !playerAndSecretaryBehavior.isMoving)
            {
                ShowBubbleChat("Whew... I'm tired, maybe tomorrow.");
            }
            else if (dayAndTimeManager.isHiring && !playerAndSecretaryBehavior.isMoving)
            {
                ShowBubbleChat("Please wait, I am currently hiring.");
            }
            else if (!dayAndTimeManager.isTired && !playerAndSecretaryBehavior.isMoving)
            {
                ShowBubbleChat("Do you need help with anything?");
            }
        }
    }

    public void OnMouseExit()
    {
        if (!isClicked && !awaitingResponse)
        {
            HideBubbleChat();
        }
    }

    public void OnMouseDown()
    {
        if (playerAndSecretaryBehavior.isMoving || dayAndTimeManager.isHiring || dayAndTimeManager.isTired || dayAndTimeManager.isBroke) return;

        isClicked = true;

        ShowBubbleChat("Do you want to hire workers? Press 'Y' for Yes or 'N' for No.");
        awaitingResponse = true; // Start waiting for player input
    }

    private void OnYesPressed()
    {
        HideBubbleChat(); // Ensure the bubble chat closes immediately
        ShowBubbleChat("Alright, hiring in progress!");
        dayAndTimeManager.isHiring = true;

        awaitingResponse = false; // End waiting for input
        isClicked = false;
    }

    private void OnNoPressed()
    {
        HideBubbleChat(); // Ensure the bubble chat closes immediately
        ShowBubbleChat("Okay!");

        awaitingResponse = false; // End waiting for input
        isClicked = false;
    }


    private void ShowBubbleChat(string message)
    {
        bubbleChatObject.SetActive(true);
        bubbleChatText.text = message;
    }

    private void HideBubbleChat()
    {
        bubbleChatObject.SetActive(false);
        bubbleChatText.text = "";
        awaitingResponse = false; // Reset waiting for input
        isClicked = false; // Reset click state
    }

}
