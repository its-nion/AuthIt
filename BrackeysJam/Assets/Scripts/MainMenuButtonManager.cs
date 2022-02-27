using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    public Button _musicButton;
    public Sprite _musicOn;
    public Sprite _musicOff;

    public Animator anim;
    
    public void onStartGamePress()
    {
        SceneManager.LoadScene("Level");
    }

    public void onMusicPress()
    {
        if (_musicButton.image.sprite == _musicOff)
        {
            _musicButton.image.sprite = _musicOn;
            AudioListener.volume = 0.1f;
            return;
        }
        AudioListener.volume = 0.0f;
        _musicButton.image.sprite = _musicOff;
    }
}
