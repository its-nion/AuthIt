using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonManager : MonoBehaviour
{
    public TimerScript _timerScript;
    public GameObject _tutorial;

    public GameObject _pauseCanvas;
    
    public void endTutorial()
    {
        _tutorial.gameObject.SetActive(false);
        _timerScript.startTimer();
    }

    public void goToHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void pauseButtonPress()
    {
        _timerScript._paused = true;
        _pauseCanvas.SetActive(true);
    }

    public void continueGame()
    {
        _pauseCanvas.SetActive(false);
        _timerScript._paused = false;
    }
}
