using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonManager : MonoBehaviour
{
    public Animator _uiAnim;
    
    public void endTutorial()
    {
        _uiAnim.SetBool("isTutorialFinished", true);
    }
}
