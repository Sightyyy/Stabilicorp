using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayAndTimeManager : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI monthText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI yearText;
    public Slider timeProgressBar;

    private string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    private string[] monthsOfYear = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    private int dayIndex = 0;
    private int monthIndex = 0;
    private int date = 1;
    private int year = 2024;
    private float timer = 0f;

    private void Start()
    {
        // Initialize text elements and slider
        dayText.text = daysOfWeek[dayIndex];
        monthText.text = monthsOfYear[monthIndex];
        dateText.text = date.ToString();
        yearText.text = year.ToString();
        timeProgressBar.maxValue = 180;
        timeProgressBar.value = 0;
        
        // Start coroutine for time tracking
        StartCoroutine(TimeTracker());
    }

    private IEnumerator TimeTracker()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Increment time every second
            timer += 1f;
            timeProgressBar.value = timer;

            if (timer >= 180f)
            {
                timer = 0f;
                timeProgressBar.value = 0;
                IncrementDay();
            }
        }
    }

    private void IncrementDay()
    {
        // Move to the next day
        dayIndex = (dayIndex + 1) % daysOfWeek.Length;
        dayText.text = daysOfWeek[dayIndex];

        // Move to the next date
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

        if (monthIndex == 0) // Reset to January
        {
            year++;
            yearText.text = year.ToString();
        }
    }

    private int GetDaysInMonth(int monthIndex, int year)
    {
        switch (monthIndex)
        {
            case 0: // January
            case 2: // March
            case 4: // May
            case 6: // July
            case 7: // August
            case 9: // October
            case 11: // December
                return 31;
            case 3: // April
            case 5: // June
            case 8: // September
            case 10: // November
                return 30;
            case 1: // February
                if (IsLeapYear(year))
                    return 29;
                else
                    return 28;
            default:
                return 30;
        }
    }

    private bool IsLeapYear(int year)
    {
        return year % 4 == 0;
    }
}
