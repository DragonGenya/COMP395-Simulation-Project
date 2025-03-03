using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 60f)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

            timerText.text = "Timer: " + string.Format("{0}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
        else
        {
            timerText.text = "Timer: " + timer.ToString("F2");
        }
    }
}
