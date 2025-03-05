using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [Header("UI Dependencies")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI orderUserText;
    public TextMeshProUGUI simulationTitle;
    public TextMeshProUGUI nextArrivalTime;
    public TextMeshProUGUI currServiceTime;

    private float timer = 0f;
    private float currServiceTimer = 0f;

    void Update()
    {

        int hours = Mathf.FloorToInt(timer / 3600f);
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);
        timerText.text = string.Format("{0}:{1:00}:{2:00}:{3}", hours, minutes, seconds, milliseconds);

        if (currServiceTimer <= 0.0f)
        {
            currServiceTimer = 0.0f;
        }
        else
        {
            currServiceTimer -= Time.deltaTime;
        }
        currServiceTime.text = currServiceTimer.ToString("F2");
    }
    public void SetTimerText(float time)
    {
        timer = time;
    }
    public void SetOrderText(string order)
    {
        orderUserText.text = order;
    }
    public void SetSimulationTitle(string title)
    {
        simulationTitle.text = title;
    }
    public void SetNextArrivalTime(float time)
    {

        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        nextArrivalTime.text = string.Format("{0}:{1:00}:{2:00}:{3}", hours, minutes, seconds, milliseconds);

    }
    public void SetCurrentServiceTime(float time)
    {
        this.currServiceTimer = time;
    }
}
