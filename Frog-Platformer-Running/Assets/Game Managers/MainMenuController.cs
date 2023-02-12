using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.instance.gameStartedFromMainMenu = true;    //this tell us that we start the game from main menu
        SceneManager.LoadScene(Tags.GAMEPLAY_SCENE);
    }    




}   //class
