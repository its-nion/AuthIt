using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationTriggers : MonoBehaviour
{
    public TimerScript _TimerScript;
    
    public void sceneToLevelTransition()
    {
        SceneManager.LoadScene("Level");
    }

    public void activateTimer()
    {
        _TimerScript.startTimer();
    }
}
