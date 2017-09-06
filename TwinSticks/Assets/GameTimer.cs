using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public GameObject TimerUI;
    public GameObject Timer;

    private bool timerStarted;
    private float timerValue;
    private bool paused = true;


    public void StartTimer()
    {
        paused = false;
        ResetTimer();
    }

    public void SceneCheck(string currentScene)
    {

        if (currentScene.Contains("level"))
        {
            ShowTimer();
        } else
        {
            HideTimer();
        }
    }

    void ShowTimer()
    {
        TimerUI.SetActive(true);
    }

    public void HideTimer()
    {
        TimerUI.SetActive(false);
        paused = true;
    }

    public void SetPaused(bool _paused)
    {
        paused = _paused;
    }

    private void Update()
    {
        if(TimerUI.activeInHierarchy && !paused)
            UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue += Time.deltaTime;
        float hours = 0;
        float minutes = 0;

        float seconds = Mathf.Round(timerValue);
        while (seconds > 60)
        {
            seconds -= 60;
            minutes++;
        }
        while (minutes > 60)
        {
            minutes -= 60;
            hours++;
        }

        string timerText = hours + ":" + minutes + ":" + seconds;
        Timer.GetComponent<Text>().text = timerText;
    }

    void ResetTimer()
    {
        timerValue = 0;
    }

}
