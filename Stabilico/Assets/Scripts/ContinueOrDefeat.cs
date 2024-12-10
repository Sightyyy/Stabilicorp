using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueOrDefeat : MonoBehaviour
{
    public Slider Finance;
    public Slider Worker;

    // Update is called once per frame
    void Update()
    {
        if(Finance.value == 0 && Worker.value == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
