using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerLabel;
    public float CurrentTime { get; private set; }
    public float BestTime { get; private set; }

    bool isCounting = false;

    void Start()
    {
        CurrentTime = 0f;
        BestTime = PlayerPrefs.GetFloat("bestTime", 0);
    }
    void Update()
    {
        if (!isCounting) { return; }
        //Updating time each frame by Time.deltaTime
        CurrentTime += Time.deltaTime;
        SetTimeOnUI();
    }
    public void ResetTime()
    {
        CurrentTime = 0f;
        SetTimeOnUI();
    }
    public void StopTimer()
    {
        isCounting = false;
    }
    public void SetBestTime(float time)
    {
        BestTime = time;
        PlayerPrefs.SetFloat("bestTime", BestTime);
    }
    public void StartTimer()
    {
        isCounting = true;
    }
    public string DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}",minutes, seconds);
    }
    void SetTimeOnUI()
    {
        timerLabel.text = DisplayTime(CurrentTime);
    }

    public void SetTimeSpeed(int value)
    {
        Time.timeScale = value;
    }
}
