using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float _startTime = 30.0f;
    public TMP_Text _timerText;
    public LevelManager _LevelManager;

    public bool _paused = false;

    private float _actualTime;

    public void startTimer()
    {
        _actualTime = _startTime + 1;

        StartCoroutine("countTimeDown");
    }

    IEnumerator countTimeDown(){
        while (_actualTime > 0)
        {
            while (_paused == true)
            {
                yield return new WaitForSeconds(0.2f);
            }
            
            _actualTime--;
            _timerText.text = _actualTime.ToString();
            yield return new WaitForSeconds(1);
        }
        _LevelManager.levelEnd("time");
    }
}
