using RadiantTools.AudioSystem;
using RadiantTools.LevelManager;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void ClickSound()
    {
        AudioManager.Instance.PlayAudio(AudioManager.SoundTypes.ButtonClick);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        TransitionScript.Instance.TransitionToLevel(1);
    }
    public void OpenMenu()
    {
        TransitionScript.Instance.TransitionToLevel(0);
    }
}
