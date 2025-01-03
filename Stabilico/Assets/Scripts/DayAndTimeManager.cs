using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DayAndTimeManager : MonoBehaviour
{
    private GameData gameData;
    private ContinueOrDefeat continueOrDefeat;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI monthText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI yearText;
    public Slider timeProgressBar;
    public Slider workerSlider;
    public Slider financeSlider;
    public Slider happinessSlider;
    public GameObject projectProgressBar;
    public List<GameObject> activeWorkers;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject secretary;

    private string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    private string[] monthsOfYear = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    private int dayIndex = 0;
    private int monthIndex = 0;
    private int date = 1;
    private int year = 2024;
    private float timer; // Timer akan diinisialisasi dengan nilai awal

    public DecisionManager decisionManager;
    private bool isPaused = false;
    public bool isDeficient = false;
    public bool isVeryDeficient = false;
    public bool isUnhappy = false;
    public bool isHiring = false;
    public bool isNewWorker = false;
    public bool isNoWorker = false;
    public bool isTired = false;
    public bool isBroke = false;
    private SecretaryInteraction secretaryInteraction;

    private void Start()
    {
        secretaryInteraction = FindObjectOfType<SecretaryInteraction>();
        continueOrDefeat = FindObjectOfType<ContinueOrDefeat>();
        gameData = GameData.instance;
        LoadTimeFromGameData();
        dayText.text = daysOfWeek[dayIndex];
        monthText.text = monthsOfYear[monthIndex];
        dateText.text = date.ToString();
        yearText.text = year.ToString();

        timeProgressBar.maxValue = 60;
        timeProgressBar.value = 11; // Atur nilai awal timeProgressBar
        timer = timeProgressBar.value; // Sinkronisasi nilai timer dengan nilai awal slider

        workerSlider.GetComponent<Slider>();
        projectProgressBar.GetComponent<Slider>().maxValue = 100;
        projectProgressBar.SetActive(false);

        StartCoroutine(TimeTracker());
    }

    private IEnumerator TimeTracker()
    {
        while (true)
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            float startValue = timeProgressBar.value;
            float endValue = timer + 1f;

            float elapsed = 0f;
            while (elapsed < 1f)
            {
                elapsed += Time.deltaTime;
                timeProgressBar.value = Mathf.Lerp(startValue, endValue, elapsed / 1f);
                yield return null;
            }

            timer += 1f;

            if (timer >= 60f) // Reset jika sudah mencapai maxValue
            {
                timer = 0f;
                timeProgressBar.value = 0;
                IncrementDay();
            }

            // Event untuk player dan worker
            if (timer == 10f)
            {
                CommandPlayerAndSecretaryToComeBack();
                CommandWorkersToComeBack();
                if(isNewWorker)
                {
                    gameData.playerFinance -= 15;
                    gameData.workerAmount += 5;
                    isTired = true;
                }
                else if(isNoWorker)
                {
                    isTired = true;
                }
            }
            if (timer == 50f)
            {
                CommandPlayerAndSecretaryToGoHome();
                CommandWorkersToGoHome();
            }
            if (timer == 15f || timer == 35f)
            {
                CommandRandomWorkerToDispenser();
            }

            // Trigger event
            if ((timer >= 20f && timer < 21f) || (timer >= 40f && timer < 41f) || (timer >= 30 && timer < 31))
            {
                isPaused = true;
                secretaryInteraction.enabled = false;
                decisionManager.TriggerEventHappening();
            }

            FillProjectProgressBar();

            if (timer >= timeProgressBar.maxValue)
            {
                timer = 0f;
                timeProgressBar.value = 0;
                IncrementDay();
            }
        }
    }

    private void CommandPlayerAndSecretaryToGoHome()
    {
        player.GetComponent<PlayerAndSecretaryBehavior>().WalkToHome(); // Player goes home immediately
        secretary.GetComponent<PlayerAndSecretaryBehavior>().WalkToHome(); // Secretary goes home with a delay
    }

    private void CommandPlayerAndSecretaryToComeBack()
    {
        player.GetComponent<PlayerAndSecretaryBehavior>().WalkToWork(); // Player returns immediately
        secretary.GetComponent<PlayerAndSecretaryBehavior>().WalkToWork(); // Secretary returns with a delay
    }

    private void CommandWorkersToGoHome()
    {
        for(int i = 0; i < activeWorkers.Count; i++)
        {
            activeWorkers[i].GetComponent<WorkerMovement>().WalkToHome();
        }
    }

    private void CommandWorkersToComeBack()
    {
        for (int i = 0; i < activeWorkers.Count; i++)
        {
            activeWorkers[i].GetComponent<WorkerMovement>().WalkToWork(); // Add 0.5s delay for spacing
        }
    }
    private void CommandRandomWorkerToDispenser()
    {
        if (activeWorkers.Count == 0) return;

        // Select a random worker
        GameObject randomWorker = activeWorkers[Random.Range(0, activeWorkers.Count)];

        // Trigger the movement
        randomWorker.GetComponent<WorkerMovement>().WalkToDispenser();
    }

    private void FillProjectProgressBar()
    {
        if (decisionManager.hasProject == 0) return; // Jika tidak ada project, keluar dari fungsi

        Slider projectSlider = projectProgressBar.GetComponent<Slider>();
        projectProgressBar.SetActive(true);

        // Total waktu yang dibutuhkan untuk menyelesaikan project dalam detik
        float projectDuration = 60f; // Sesuaikan dengan waktu yang dibutuhkan (misalnya 60 detik untuk 1 project)
        
        int totalWorkers = (int)workerSlider.value;
        int workingWorkers = activeWorkers.Count;

        if (timer > 10 && timer < 50)
        {
            if (workingWorkers > 0)
            {
                // Menghitung progress berdasarkan worker aktif per waktu proyek
                float progressRate = workingWorkers / ((float)totalWorkers*5);
                projectSlider.value += (progressRate * (Time.deltaTime / projectDuration)) * 10000; // Persentase progress per waktu
            }
        }

        // Cek apakah progress telah selesai
        if (projectSlider.value >= projectSlider.maxValue)
        {
            Debug.Log("Project selesai!");
            gameData.playerFinance += 60;
            decisionManager.hasProject--; // Mengurangi jumlah project yang sedang dikerjakan
            if (decisionManager.hasProject <= 0)
            {
                decisionManager.hasProject = 0;
                ResetProjectProgressBar();
            }
            else
            {
                Debug.Log("Masih ada proyek lain yang perlu dikerjakan.");
                projectSlider.value = 0; // Reset nilai progress untuk proyek berikutnya
            }
        }
    }


    private void ResetProjectProgressBar()
    {
        Slider projectSlider = projectProgressBar.GetComponent<Slider>();
        projectSlider.value = 0;
        projectSlider.maxValue = 100;
        projectProgressBar.SetActive(false);
        decisionManager.hasProject = 0;
    }

    private void IncrementDay()
    {
        // Reset tired state and hiring flags
        isTired = false;
        isNewWorker = false;
        isNoWorker = false;

        // Handle hiring logic
        int rand = Random.Range(0, 2);
        if (isHiring)
        {
            if (gameData.playerFinance <= 0)
            {
                isBroke = true;
            }

            if (rand == 0)
            {
                isNoWorker = true;
            }
            else
            {
                isNewWorker = true;
            }

            isHiring = false;
        }

        // Adjust worker conditions
        if (gameData.playerFinance <= 0 && gameData.workerAmount <= 0)
        {
            continueOrDefeat.TriggerLose();
        }

        if (gameData.playerFinance <= 0)
        {
            gameData.workerAmount -= 5;
            if (gameData.workerHappiness > 0)
            {
                gameData.workerHappiness -= 5;
            }
        }

        if (gameData.playerFinance <= 0 && gameData.workerHappiness <= 0)
        {
            gameData.workerAmount -= 10;
        }
        else if (gameData.workerHappiness <= 0)
        {
            gameData.workerAmount -= 5;
        }

        // Update day and date
        dayIndex = (dayIndex + 1) % daysOfWeek.Length;
        dayText.text = daysOfWeek[dayIndex];
        IncrementDate();

        SaveTimeToGameData();
    }



    private void IncrementDate()
    {
        int maxDaysInMonth = GetDaysInMonth(monthIndex, year);
        date++;
        // if(gameData.playerFinance == 0 || gameData.workerHappiness == 0)
        // {
        //     gameData.workerHappiness -= 10;
        //     gameData.workerAmount -= 5;
        // }
        // if (gameData.playerFinance == 0 && gameData.workerHappiness == 0)
        // {
        //     gameData.workerHappiness -= 10;
        //     gameData.workerAmount -= 10;
        // }

        if (date > maxDaysInMonth)
        {
            date = 1;
            IncrementMonth();
        }

        dateText.text = date.ToString();
    }

    private void IncrementMonth()
    {
        monthIndex = (monthIndex + 1) % monthsOfYear.Length;
        monthText.text = monthsOfYear[monthIndex];

        if (monthIndex == 0)
        {
            year++;
            yearText.text = year.ToString();
        }
    }

    private int GetDaysInMonth(int monthIndex, int year)
    {
        switch (monthIndex)
        {
            case 0: case 2: case 4: case 6: case 7: case 9: case 11: return 31;
            case 3: case 5: case 8: case 10: return 30;
            case 1: return IsLeapYear(year) ? 29 : 28;
            default: return 30;
        }
    }

    private bool IsLeapYear(int year) => year % 4 == 0;

    public void ResumeTimeBar()
    {
        isPaused = false;
    }
    private void LoadTimeFromGameData()
    {
        // Load values from GameData
        dayIndex = gameData.currentDay;
        monthIndex = gameData.currentMonth;
        date = gameData.currentDate;
        year = gameData.currentYear;
        timer = gameData.timeBarValue;

        // Update UI
        dayText.text = daysOfWeek[dayIndex];
        monthText.text = monthsOfYear[monthIndex];
        dateText.text = date.ToString();
        yearText.text = year.ToString();
        timeProgressBar.value = timer;

        Debug.Log("Time loaded from GameData.");
    }

    private void SaveTimeToGameData()
    {
        gameData.currentDay = dayIndex;
        gameData.currentMonth = monthIndex;
        gameData.currentDate = date;
        gameData.currentYear = year;
        gameData.timeBarValue = timer;

        gameData.SaveData();
        Debug.Log("Time saved to GameData.");
    }
}
