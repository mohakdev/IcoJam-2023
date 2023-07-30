using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerLabel;
    public float CurrentTime { get; private set; }
    bool isCounting = false;

    void Start()
    {
        CurrentTime = 0f;
    }
    void Update()
    {
        if (!isCounting) { return; }
        //Updating time each frame by Time.deltaTime
        CurrentTime += Time.deltaTime;
        DisplayTime(CurrentTime);
    }
    public void ResetTime()
    {
        CurrentTime = 0f;
        DisplayTime(CurrentTime);
    }
    public void StopTimer()
    {
        isCounting = false;
    }
    public void StartTimer()
    {
        isCounting = true;
    }
    void DisplayTime(float time)
    {
        float seconds = TimeSpan.FromSeconds(time).Seconds;
        float miliseconds = TimeSpan.FromSeconds(time).Milliseconds;
        timerLabel.text = seconds + ":" + miliseconds;
    }
}
