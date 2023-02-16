using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button musicBtn;

    [SerializeField]
    private Sprite soundOff, soundOn;



    public void PlayGame()
    {
        GameManager.instance.gameStartedFromMainMenu = true;    //this tell us that we start the game from main menu
        SceneManager.LoadScene(Tags.GAMEPLAY_SCENE);
    }

    public void ControlMusic()
    {
        if (GameManager.instance.canPlayMusic)
        {
            musicBtn.image.sprite = soundOn;    //swap button image to sound on
            GameManager.instance.canPlayMusic = false;
        }
        else
        {
            musicBtn.image.sprite = soundOff;   //swap button to image to sound off
            GameManager.instance.canPlayMusic = true;
        }

    }
}   //class
