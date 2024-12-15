using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitEmployee : MonoBehaviour
{
    public Slider Finance;
    public Slider Worker;

    public void Recruit()
    {
        if (Worker.value != Worker.maxValue)
        {
            Finance.value -= 5;
            Worker.value += 1;
        }
    }
}
