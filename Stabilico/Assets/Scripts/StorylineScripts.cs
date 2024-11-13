using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StorylineScript : MonoBehaviour
{
    public TypingEffect typingEffect; // Drag and drop TypingEffect component

    public GameObject yesButton;
    public GameObject noButton;
    private int yesClick = 0;
    private int noClick = 0;

    void Start()
    {
        StartCoroutine(StartStoryline());
    }

    void Jeda(float duration)
    {
        typingEffect.Wait(duration, OnWaitComplete);
    }

    void Ketik(string text, float time)
    {
       typingEffect.TypeText(text, time);
    }

    public void selectYes()
    {
        yesClick = 1;
    }

    public void selectNo()
    {
        noClick = 1;
    }

    public void yesButtonInteractable(bool interact)
    {
        Button Yes1 = yesButton.GetComponent<Button>();
        if(Yes1 != null)
        {
            Yes1.interactable = interact;
        }
    }

    public void noButtonInteractable(bool interact)
    {
        Button No1 = noButton.GetComponent<Button>();
        if(No1 != null)
        {
            No1.interactable = interact;
        }
    }

    private IEnumerator StartStoryline()
    {
        Ketik("So, you finally succeed on getting a diploma...", 20);
        yield return new WaitUntil(() => typingEffect.IsTypingFinished());

        Jeda(3);
        yield return new WaitForSeconds(3);

        Ketik("And now you want to make your own company, huh?", 20);
        yield return new WaitUntil(() => typingEffect.IsTypingFinished());

        Jeda(3);
        yield return new WaitForSeconds(3);

        Ketik("Are you ready to face all of the pressure after you create your own company", 22);
        yield return new WaitUntil(() => typingEffect.IsTypingFinished());
        Jeda(1);
        yield return new WaitForSeconds(1);
        yesButton.SetActive(true);
        Jeda(0.8f);
        yield return new WaitForSeconds(0.8f);
        noButton.SetActive(true);

        // Tunggu sampai pemain mengklik salah satu tombol
        yield return new WaitUntil(() => yesClick == 1 || noClick == 1);

        // Jika yesClick == 1, jalankan logika Yes
        if (yesClick == 1)
        {
            Ketik("", 1);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            noButton.SetActive(false);
            yesButtonInteractable(false);
            Jeda(2);
            yield return new WaitForSeconds(2);
            yesButton.SetActive(false);
            Ketik("Okay then.", 20);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(2);
            yield return new WaitForSeconds(2);
            Ketik("Please insert your name", 20);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(3);
            yield return new WaitForSeconds(3);
            Ketik("Please insert your company name", 20);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(2);
            yield return new WaitForSeconds(2);
            Ketik("Okay, thats all i needed to know.", 20);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(3);
            yield return new WaitForSeconds(3);
            Ketik("Good luck", 20);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());

        }
        // Jika noClick == 1, jalankan logika No
        else if(noClick == 1)
        {
            Ketik("", 1);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            yesButton.SetActive(false);
            noButtonInteractable(false);
            Jeda(2);
            yield return new WaitForSeconds(2);
            noButton.SetActive(false);
            Ketik(".    .    .", 4);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(3);
            yield return new WaitForSeconds(3);
            Ketik("Then why are you playing this game?", 14);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(3);
            yield return new WaitForSeconds(3);
            Ketik("Do you think that making and managing a company is a piece of cake, huh?", 16);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(3);
            yield return new WaitForSeconds(3);
            Ketik("GET OUT!!!", 1.5f);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Application.Quit();
        }
    }

    private void OnWaitComplete()
    {
        // Action to perform after waiting
        Debug.Log("Wait completed!");
    }
}
