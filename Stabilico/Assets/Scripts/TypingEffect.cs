using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;  // TextMeshProUGUI component
    private string fullText;              // Full text to display
    private float typingSpeed;            // Speed of typing effect
    private bool isTyping = false;        // Flag to check if typing is in progress

    // Public method to start typing effect
    public void TypeText(string text, float speed)
    {
        fullText = text;
        typingSpeed = speed;
        isTyping = true; // Set typing flag
        StartCoroutine(ShowTextWithTypingEffect());
    }

    // Coroutine for the typing effect
    private IEnumerator ShowTextWithTypingEffect()
    {
        textMeshPro.text = "";  // Clear the text at the start
        foreach (char letter in fullText.ToCharArray())
        {
            textMeshPro.text += letter;  // Add one character at a time
            yield return new WaitForSeconds(1f / typingSpeed);  // Wait based on typing speed
        }
        isTyping = false; // Reset typing flag when done
    }

    // Method to check if typing is finished
    public bool IsTypingFinished()
    {
        return !isTyping;
    }

    // Public method to create a wait with a callback
    public void Wait(float duration, System.Action callback)
    {
        StartCoroutine(WaitCoroutine(duration, callback));
    }

    // Coroutine for the wait
    private IEnumerator WaitCoroutine(float duration, System.Action callback)
    {
        yield return new WaitForSeconds(duration);  // Wait for the specified duration
        callback?.Invoke();  // Execute the passed callback after the delay
    }
}
