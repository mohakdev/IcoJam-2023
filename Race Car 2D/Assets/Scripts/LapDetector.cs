using System;
using UnityEngine;

public class LapDetector : MonoBehaviour
{
    [NonSerialized] public bool isTouched;
    LapDetector[] sideDetectors;
    public bool isMainDetector;
    public event Action OnPlayerLapCompleted;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (!isMainDetector) 
            {
                isTouched = true;
                return;
            }
            sideDetectors = GetComponentsInChildren<LapDetector>();
            if (GetSideDetectorState()) { OnPlayerLapCompleted?.Invoke(); }
            SetSideDetectorState(false);
        }
    }
    void SetSideDetectorState(bool state)
    {
        foreach(LapDetector detector in sideDetectors)
        {
            if (detector.isMainDetector) { continue; }
            detector.isTouched = state;
        }
    }
    bool GetSideDetectorState()
    {
        foreach (LapDetector detector in sideDetectors)
        {
            if (detector.isMainDetector) { continue; }
            if (!detector.isTouched) { return false; }
        }
        return true;
    }
}
