using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [Header("GameObjects")]
    public SpriteRenderer _picture;
    public SpriteRenderer _damage;
    public SpriteRenderer _frame;
    public SpriteRenderer _xRay;
    public TMP_Text _basicInfos;
    public TMP_Text _advancedInfos;
    
    [Header("General Settings")] 
    public Painting[] paintings;

    // private variables
    private List<Painting> _unplayedLevels = new List<Painting>();
    private Painting _currentPainting;

    void Start()
    {
        initializeGame();

        pickNewRandomPaintingAsCurrent();
        
        loadPaintingIntoGame(_currentPainting);
    }
    
    // public methods
    public void levelEnd(string status)
    {
        switch (status)
        {
            case "fake": Debug.Log("fake");
                break;
            case "real": Debug.Log("real");
                break;
                default: Debug.Log("time");
                    break;
        }
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
        _currentPainting = _unplayedLevels[x];
    }

    private void loadPaintingIntoGame(Painting p)
    {
        _picture.sprite = p._painting;
        _damage.sprite = p._damage;
        _frame.sprite = p._frame;
        _xRay.sprite = p._xray;

        _basicInfos.text =
            "Name:  <color=#8C94A2>" + p._name.Replace(" ", "  ") + "</color>\n"
            + "Autor:  <color=#8C94A2>" + p._autor.Replace(" ", "  ") + "</color>\n"
            + "Date:  <color=#8C94A2>" + p._date + "</color>\n";
        
        _advancedInfos.text =
            "Condition:  <color=#8C94A2>" + p._condition.Replace(" ", "  ") + "</color>\n"
            + "Fact:  <color=#8C94A2>" + p._fact.Replace(" ", "  ") + "</color>\n";
        
        _picture.GetComponent<BoxCollider2D>().size = (p._painting.rect.size / p._painting.pixelsPerUnit);
    }
}
