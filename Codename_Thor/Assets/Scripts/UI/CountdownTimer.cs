using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // Reference to the TextMeshPro text component
    public int countdownTime = 3;          // Countdown time in seconds

    void Start()
    {
        Time.timeScale = 0f;  // Pause the game
        StartCoroutine(StartCountdown());  // Start the countdown
    }

    IEnumerator StartCountdown()
    {
        int timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            countdownText.text = timeLeft.ToString();  // Update the countdown text
            yield return new WaitForSecondsRealtime(1f);  // Wait for 1 real-time second
            timeLeft--;
        }

        countdownText.text = "Go!";  // Display "Go!" at the end of the countdown
        yield return new WaitForSecondsRealtime(1f);  // Wait for an additional second

        countdownText.gameObject.SetActive(false);  // Hide the countdown text
        Time.timeScale = 1f;  // Unpause the game
    }
}
