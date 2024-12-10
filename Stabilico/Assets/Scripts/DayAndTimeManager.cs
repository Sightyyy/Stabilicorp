using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DayAndTimeManager : MonoBehaviour
{
    private GameData gameData;

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
    private Vector3 dispenserPosition = new Vector3(26, 0, 0);

    private string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    private string[] monthsOfYear = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    private int dayIndex = 0;
    private int monthIndex = 0;
    private int date = 1;
    private int year = 2024;
    private float timer; // Timer akan diinisialisasi dengan nilai awal

    public DecisionManager decisionManager;
    private bool isPaused = false;

    private void Start()
    {
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
            }
            if (timer == 50f)
            {
                CommandPlayerAndSecretaryToGoHome();
                CommandWorkersToGoHome();
            }
            if (timer == 10f || timer == 30f)
            {
                CommandRandomWorkerToDispenser();
            }

            // Trigger event
            if ((timer >= 20f && timer < 21f) || (timer >= 40f && timer < 41f))
            {
                isPaused = true;
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
        player.GetComponent<PlayerAndSecretaryBehavior>().GoHome(0f); // Player goes home immediately
        secretary.GetComponent<PlayerAndSecretaryBehavior>().GoHome(0.5f); // Secretary goes home with a delay
    }

    private void CommandPlayerAndSecretaryToComeBack()
    {
        player.GetComponent<PlayerAndSecretaryBehavior>().ComeBackToWork(0f); // Player returns immediately
        secretary.GetComponent<PlayerAndSecretaryBehavior>().ComeBackToWork(0.5f); // Secretary returns with a delay
    }

    private void CommandWorkersToGoHome()
    {
        for(int i = 0; i < activeWorkers.Count; i++)
        {
            activeWorkers[i].GetComponent<WorkerBehavior>().GoHome(i * 0.5f);
        }
    }

    private void CommandWorkersToComeBack()
    {
        for (int i = 0; i < activeWorkers.Count; i++)
        {
            activeWorkers[i].GetComponent<WorkerBehavior>().ComeBackToWork(i * 0.5f); // Add 0.5s delay for spacing
        }
    }
    private void CommandRandomWorkerToDispenser()
    {
        if (activeWorkers.Count == 0) return;

        // Select a random worker
        GameObject randomWorker = activeWorkers[Random.Range(0, activeWorkers.Count)];

        // Trigger the movement
        randomWorker.GetComponent<WorkerBehavior>().MoveToDispenser(dispenserPosition);
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
        dayIndex = (dayIndex + 1) % daysOfWeek.Length;
        dayText.text = daysOfWeek[dayIndex];
        IncrementDate();

        SaveTimeToGameData();
    }

    private void IncrementDate()
    {
        int maxDaysInMonth = GetDaysInMonth(monthIndex, year);
        date++;
        int conditionDate = date % 5;

        if (conditionDate == 0)
        {
            if(financeSlider.value == 0 || happinessSlider.value == 0)
            {
                workerSlider.value -= 5;
            }
            else if (financeSlider.value == 0 && happinessSlider.value == 0)
            {
                workerSlider.value -= 10;
            }
        }
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
