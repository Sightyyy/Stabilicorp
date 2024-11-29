using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DayAndTimeManager : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI monthText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI yearText;
    public Slider timeProgressBar;
    public Slider workerSlider; // Slider untuk jumlah worker total
    public GameObject projectProgressBar;
    public List<GameObject> activeWorkers;
    private Vector3 dispenserPosition = new Vector3(26, 0, 0);

    private string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    private string[] monthsOfYear = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    private int dayIndex = 0;
    private int monthIndex = 0;
    private int date = 1;
    private int year = 2024;
    private float timer = 0f;

    public DecisionManager decisionManager; // Reference ke script DecisionManager
    private bool isPaused = false;

    private void Start()
    {
        dayText.text = daysOfWeek[dayIndex];
        monthText.text = monthsOfYear[monthIndex];
        dateText.text = date.ToString();
        yearText.text = year.ToString();
        timeProgressBar.maxValue = 60;
        timeProgressBar.value = 0;
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

            if (timer == 10f || timer == 30f)
            {
                CommandRandomWorkerToDispenser();
            }

            // Check for trigger points
            if (timer >= 20f && timer < 21f || timer >= 40f && timer < 41f)
            {
                isPaused = true;
                decisionManager.TriggerEventHappening(); // Activate the event happening UI
            }

            // Panggil fungsi baru untuk mengisi project progress bar
            FillProjectProgressBar();

            if (timer >= timeProgressBar.maxValue)
            {
                timer = 0f;
                timeProgressBar.value = 0;
                IncrementDay();
            }
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

        if (workingWorkers > 0)
        {
            // Menghitung progress berdasarkan worker aktif per waktu proyek
            float progressRate = workingWorkers / ((float)totalWorkers*5);
            projectSlider.value += (progressRate * (Time.deltaTime / projectDuration)) * 10000; // Persentase progress per waktu
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
    }

    private void IncrementDate()
    {
        int maxDaysInMonth = GetDaysInMonth(monthIndex, year);
        date++;

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
}
