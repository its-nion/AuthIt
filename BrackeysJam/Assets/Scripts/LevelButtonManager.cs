using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonManager : MonoBehaviour
{
    public TimerScript _timerScript;
    public GameObject _tutorial;
    
    public void endTutorial()
    {
        _tutorial.gameObject.SetActive(false);
        _timerScript.startTimer();
    }

    public void goToHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
