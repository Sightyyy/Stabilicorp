using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDebugger : MonoBehaviour
{
    public DayAndTimeManager dayAndTimeManager; // Reference to the DayAndTimeManager script
    private DecisionManager decisionManager;

    private void Awake()
    {
        decisionManager = FindObjectOfType<DecisionManager>();
    }

    private void Update()
    {
        DebugProject(); // Panggil fungsi setiap frame
    }

    public void DebugProject()
    {
        // Mengecek apakah Ctrl + F ditekan dalam satu frame
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
        {
            decisionManager.hasProject += 1; // Tambahkan nilai hasProject di DayAndTimeManager
            Debug.Log("Project Debugging Started! HasProject: " + decisionManager.hasProject);
        }
    }
}
