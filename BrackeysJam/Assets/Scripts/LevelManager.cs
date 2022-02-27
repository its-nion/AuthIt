using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [Header("GameObjects")]
    public SpriteRenderer _picture;
    public SpriteRenderer _damage;
    public SpriteRenderer _frame;
    public SpriteRenderer _xRay;
    public Text _basicInfos;
    public Text _advancedInfos;

    public TMP_Text _endScreenTitle;
    public TMP_Text _endScreenReason;

    public Canvas _endScreenCanvas;
    public Button _endScreenHomeButton;
    public Button _endScreenNextButton;

    public TimerScript _timerScript;
    public SwipeManager _swipeManager;

    [Header("General Settings")] 
    public Painting[] paintings;

    // private variables
    private List<Painting> _unplayedLevels = new List<Painting>();
    private int _currentPaintingIndex;
    private Painting _currentPainting;
    private bool _hasLevelEnded = false;

    void Start()
    {
        initializeGame();

        pickNewRandomPaintingAsCurrent();
        
        loadPaintingIntoGame(_currentPainting);
    }
    
    // public methods
    public void levelEnd(string status)
    {
        _timerScript.StopCoroutine("countTimeDown");
        
        switch (status)
        {
            case "fake": if (_hasLevelEnded == false) _hasLevelEnded = true;
                if (_currentPainting._fake == true)
                {
                    showHomeNextEndscreen(false);
                    break;
                }
                showHomeEndscreen(true);
                break;
            case "real": if (_hasLevelEnded == false) _hasLevelEnded = true;
                if (_currentPainting._fake == false)
                {
                    showHomeNextEndscreen(true);
                    break;
                }
                showHomeEndscreen(false);
                break;
                default: if (_hasLevelEnded == false) _hasLevelEnded = true;
                    showTimerEndscreen();
                    break;
        }
    }

    public void continueNextLevel()
    {
        if (_unplayedLevels.Count == 1)
        {
            _endScreenTitle.text = "Thank  you  for  playing  this  <color=yellow>Game</color>!";
            _endScreenReason.text = "Unfortunately  these  are  all  the  levels  we  managed  to  create  in  this  week. " +
                                    " We  would  appreciate  a  positive  feedback  if  you  enjoyed  it.";
            _endScreenNextButton.gameObject.SetActive(false);
            return;
        }
        _unplayedLevels.RemoveAt(_currentPaintingIndex);
        pickNewRandomPaintingAsCurrent();
        loadPaintingIntoGame(_currentPainting);
        
        _endScreenHomeButton.gameObject.SetActive(false);
        _endScreenNextButton.gameObject.SetActive(false);
        _endScreenCanvas.gameObject.SetActive(false);
        
        _hasLevelEnded = false;
        _swipeManager.resetTriggeredState();
        _timerScript.startTimer();
    }
    
    // private methods
    private void initializeGame()
    {
        foreach(Painting p in paintings)
        {
            _unplayedLevels.Add(p);
        }
    }
    
    private void pickNewRandomPaintingAsCurrent()
    {
        var x = Random.Range(0, _unplayedLevels.Count);
        _currentPaintingIndex = x;
        _currentPainting = _unplayedLevels[x];
    }

    private void loadPaintingIntoGame(Painting p)
    {
        _picture.sprite = p._painting;
        _damage.sprite = p._damage;
        _frame.sprite = p._frame;
        _xRay.sprite = p._xray;

        _basicInfos.text =
            "Name:  <color=#8C94A2>" + p._name.Replace(" ", "       ") + "</color>\n"
            + "Autor:  <color=#8C94A2>" + p._autor.Replace(" ", "       ") + "</color>\n"
            + "Date:  <color=#8C94A2>" + p._date + "</color>\n";
        
        _advancedInfos.text =
            "Condition:  <color=#8C94A2>" + p._condition.Replace(" ", "       ") + "</color>\n"
            + "Fact:  <color=#8C94A2>" + p._fact.Replace(" ", "       ") + "</color>\n";
        
        _picture.GetComponent<BoxCollider2D>().size = (p._painting.rect.size / p._painting.pixelsPerUnit);
    }

    private void showHomeEndscreen(bool wasPaintingReal)
    {
        if (wasPaintingReal == false)
        {
            _endScreenTitle.text = "The  painting  was  <color=red>Fake</color>!";
            _endScreenReason.text = _currentPainting._solution.Replace(" ","  ");
            
            _endScreenHomeButton.gameObject.SetActive(true);
            _endScreenCanvas.gameObject.SetActive(true);
            
            return;
        }
        _endScreenTitle.text = "The  painting  was  <color=red>Real</color>!";
        _endScreenReason.text = _currentPainting._solution.Replace(" ","  ");
        
        _endScreenHomeButton.gameObject.SetActive(true);
        _endScreenCanvas.gameObject.SetActive(true);
    }

    private void showHomeNextEndscreen(bool wasPaintingReal)
    {
        if (wasPaintingReal == false)
        {
            _endScreenTitle.text = "The  painting  was  <color=green>Fake</color>!";
            _endScreenReason.text = _currentPainting._solution.Replace(" ","  ");
            
            _endScreenHomeButton.gameObject.SetActive(true);
            _endScreenNextButton.gameObject.SetActive(true);
            _endScreenCanvas.gameObject.SetActive(true);

            return;
        }
        _endScreenTitle.text = "The  painting  was  <color=green>Real</color>!";
        _endScreenReason.text = _currentPainting._solution.Replace(" ","  ");
        
        _endScreenHomeButton.gameObject.SetActive(true);
        _endScreenNextButton.gameObject.SetActive(true);
        _endScreenCanvas.gameObject.SetActive(true);
    }

    private void showTimerEndscreen()
    {
        _endScreenTitle.text = "You  ran  out  of  <color=red>Time</color>!";
        _endScreenReason.text = "Try  to  be  faster  next  time!";
        
        _endScreenHomeButton.gameObject.SetActive(true);
        _endScreenCanvas.gameObject.SetActive(true);
    }
}
