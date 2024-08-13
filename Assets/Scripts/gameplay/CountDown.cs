using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject timerpanel;
    private float timeRemaining = 300f; // Total countdown time in seconds (5 minutes)
    private bool isRunning = false;

    void Start()
    {
      isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            // Decrement the time remaining
            timeRemaining -= Time.deltaTime;
            if(timeRemaining == 150f)
            {
              timerpanel.GetComponent<Image>().color = Color.red;
            }

            // Check if the time has run out
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isRunning = false;
                // Optionally, handle what happens when the countdown ends
                OnCountdownComplete();
            }

            // Update the timer display
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        // Convert time remaining to minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Format the time display as MM:SS
        timerText.text = $"{minutes:D1}:{seconds:D2}";
    }

    void OnCountdownComplete()
    {
        // Handle what happens when the countdown completes
        timerText.text = "00:00";
        Debug.Log("Countdown complete!");
    }
}
