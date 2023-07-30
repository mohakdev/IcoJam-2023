using RadiantTools.AudioSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    [SerializeField] LapDetector mainLapCollider;
    [SerializeField] CarController[] carControllers;
    [SerializeField] Text lapLabel;
    [SerializeField] GameObject startScreen;
    Text readyGoLabel;
    Timer timer;
    public int lapsCompleted = 0;
    void Start()
    {
        mainLapCollider.OnPlayerLapCompleted += AddPlayerLaps;
        readyGoLabel = startScreen.transform.GetComponentInChildren<Text>();
        timer = GetComponent<Timer>();

        SetStartingPosition();
        StartCoroutine(StartRace());
    }
    IEnumerator StartRace()
    {
        ChangeCarsState(false);
        startScreen.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlayAudio(AudioManager.SoundTypes.ReadyGo);

        startScreen.SetActive(true);
        readyGoLabel.text = "3";
        yield return new WaitForSeconds(1);
        readyGoLabel.text = "2";
        yield return new WaitForSeconds(1);
        readyGoLabel.text = "1";
        yield return new WaitForSeconds(1);
        readyGoLabel.text = "GO";
        yield return new WaitForSeconds(1);
        startScreen.SetActive(false);

        ChangeCarsState(true);
        timer.StartTimer();
    }

    void AddPlayerLaps()
    {
        lapsCompleted++;
        lapLabel.text = "Laps Left = " + (3 - lapsCompleted).ToString();
        //Race Finished
        if (lapsCompleted == 3) 
        {
            RaceFinished(true);
        }
    }

    void SetStartingPosition()
    {
        //Arraging all cars in position
        Vector2 defaultPosition = new Vector2(13,2);
        Vector2 offset = new Vector2(8, 4);
        bool onTop = true;
        for (int k = 0; k < carControllers.Length; k++)
        {
            //Place Car on Top
            if (k % 2 == 0)
            {
                defaultPosition -= new Vector2(offset.x, 0);
            }
            if(onTop) { defaultPosition.y = 2; }
            else { defaultPosition.y = -2; }
            carControllers[k].transform.localPosition = (Vector3)defaultPosition;
            onTop = !onTop;
        }
    }
    void ChangeCarsState(bool state)
    {
        for(int j = 0; j < carControllers.Length; j++)
        {
            carControllers[j].enabled = state;
        }
    }
    public void RaceFinished(bool victory)
    {
        Time.timeScale = 0;
        if (victory)
        {
            Debug.Log("You Won");
        }
        else
        {
            Debug.Log("You Lost");
        }
    }

}
