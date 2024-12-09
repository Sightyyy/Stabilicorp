using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StorylineScript : MonoBehaviour
{
    public TypingEffect typingEffect; // Drag and drop TypingEffect component

    public GameObject yesButton;
    public GameObject noButton;
    [SerializeField] private TMPro.TMP_InputField ceoNameInput;
    [SerializeField] private TMPro.TMP_InputField companyNameInput;
    private int yesClick = 0;
    private int noClick = 0;
    private bool isCeoNameAccepted = false;
    private bool isCompanyNameAccepted = false;
    private MainMenu mainMenu;
    private AudioCollection audioCollection;

    void Start()
    {
        audioCollection.PlayBGM(audioCollection.pregame);
        StartCoroutine(StartStoryline());
    }

    private void Update()
    {
        if (ceoNameInput.gameObject.activeSelf && ceoNameInput.interactable)
        {
            CheckCEONameInput();
        }
        else if (companyNameInput.gameObject.activeSelf && companyNameInput.interactable)
        {
            CheckCompanyNameInput();
        }
    }

    void Jeda(float duration)
    {
        typingEffect.Wait(duration, OnWaitComplete);
    }

    void Ketik(string text, float time)
    {
       typingEffect.TypeText(text, time);
    }

    public void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
        mainMenu = GameObject.FindObjectOfType<MainMenu>();
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

    private void CheckCEONameInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            string ceoName = ceoNameInput.text.Trim();

            if (ceoName.Contains("Frederick") || ceoName.Contains("Kayla") || ceoName.Contains("Sightyy") || ceoName.Contains("Scarlet"))
            {
                Ketik("Unfortunately... this name is taken, type another one :>", 60);
            }
            if (ceoName.Length >= 4 && ceoName.Length <= 20)
            {
                GameData.instance.ceoName = ceoName;
                isCeoNameAccepted = true;
                Debug.Log($"CEO Name stored: {ceoName}");

                ceoNameInput.text = "";
                ceoNameInput.interactable = false;
                ceoNameInput.gameObject.SetActive(false);
            }
            else
            {
                Ketik("Invalid CEO Name. Must be 4-20 characters.", 40);
            }
        }
    }


    private void CheckCompanyNameInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            string companyName = companyNameInput.text.Trim();

            if (companyName.Length >= 4 && companyName.Length <= 20)
            {
                GameData.instance.companyName = companyName;
                isCompanyNameAccepted = true;
                Debug.Log($"CEO Name stored: {companyName}");

                companyNameInput.text = "";
                companyNameInput.interactable = false;
                companyNameInput.gameObject.SetActive(false);
            }
            else
            {
                Ketik("Invalid Company Name. Must be 4-20 characters.", 40);
            }
        }
    }

    private IEnumerator StartStoryline()
    {
        Ketik("So, you finally succeed on getting a diploma...", 60);
        yield return new WaitUntil(() => typingEffect.IsTypingFinished());

        Jeda(2);
        yield return new WaitForSeconds(2);

        Ketik("And now you want to make your own company, huh?", 60);
        yield return new WaitUntil(() => typingEffect.IsTypingFinished());

        Jeda(2);
        yield return new WaitForSeconds(2);

        Ketik("Are you ready to face all of the pressure after you create your own company", 60);
        yield return new WaitUntil(() => typingEffect.IsTypingFinished());
        yesButton.SetActive(true);
        Jeda(0.8f);
        yield return new WaitForSeconds(0.8f);
        noButton.SetActive(true);

        // Tunggu sampai pemain mengklik salah satu tombol
        yield return new WaitUntil(() => yesClick == 1 || noClick == 1);

        // Jika yesClick == 1, jalankan logika Yes
        if (yesClick == 1)
        {
            noButton.SetActive(false);
            yesButtonInteractable(false);
            Jeda(1);
            yield return new WaitForSeconds(1);
            yesButton.SetActive(false);
            Ketik("Okay then.", 60);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(3);
            yield return new WaitForSeconds(2);
            Ketik("Please insert your name (4-20 Characters)", 60);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            ceoNameInput.gameObject.SetActive(true);
            ceoNameInput.interactable = true;
            yield return new WaitUntil(() => isCeoNameAccepted);
            Ketik("Please insert your company name (4-20 Characters)", 60);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            companyNameInput.gameObject.SetActive(true);
            companyNameInput.interactable = true;
            yield return new WaitUntil(() => isCompanyNameAccepted);
            Ketik("Okay, that's all I needed to know.", 60);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(2);
            yield return new WaitForSeconds(2);
            Ketik("Good luck", 60);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(1);
            yield return new WaitForSeconds(1);
            mainMenu.PlayGame("Game Content 2");
        }
        // Jika noClick == 1, jalankan logika No
        else if(noClick == 1)
        {
            yesButton.SetActive(false);
            noButtonInteractable(false);
            Jeda(1);
            yield return new WaitForSeconds(1);
            noButton.SetActive(false);
            Ketik(".    .    .", 4);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(3);
            yield return new WaitForSeconds(3);
            Ketik("Then why are you playing this game?", 50);
            yield return new WaitUntil(() => typingEffect.IsTypingFinished());
            Jeda(3);
            yield return new WaitForSeconds(3);
            Ketik("Do you think that making and managing a company is a piece of cake, huh?", 50);
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
