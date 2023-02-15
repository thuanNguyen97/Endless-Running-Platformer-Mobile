using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    private Text scoreText, healthText, levelText;

    private float score, health, level;

    [HideInInspector]
    public bool canCountScore;

    private BGScroller bgScroller;

    private GameObject pausePanel;

    void Awake()
    {
        MakeInstance();

        scoreText = GameObject.Find(Tags.SCORE_TEXT_OBJ).GetComponent<Text>();
        healthText = GameObject.Find(Tags.HEALTH_TEXT_OBJ).GetComponent<Text>();
        levelText = GameObject.Find(Tags.LEVEL_TEXT_OBJ).GetComponent<Text>();

        bgScroller = GameObject.Find(Tags.BACKGROUND_GAME_OBJ).GetComponent<BGScroller>();

        pausePanel = GameObject.Find(Tags.PAUSE_PANEL_OBJ);
        pausePanel.SetActive(false);    //deactivate the pause panel 
    }

    void Update()
    {
        IncrementScore(1);
        Debug.Log("Score is incrementing");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }    
    }    

    void OnEnable() //delegated
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    void OnDisable()    //delegated
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        instance = null;
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)  //delegated
    {
        if (scene.name == Tags.GAMEPLAY_SCENE)
        {
            
            if (GameManager.instance.gameStartedFromMainMenu)
            {
                Debug.Log("Game was started from Main Menu");
                GameManager.instance.gameStartedFromMainMenu = false;

                score = 0;
                health = 3;
                level = 0;
            }
            else if (GameManager.instance.gameRestartedPlayerDied)
            {
                GameManager.instance.gameRestartedPlayerDied = false;

                score = GameManager.instance.score;
                health = GameManager.instance.health;
            }

            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            levelText.text = level.ToString();
        }
    }

    public void TakeDamage()
    {
        health--;

        if (health >= 0)
        {
            //remain health
            healthText.text = health.ToString();

            // restart the game
            StartCoroutine(PlayerDied(Tags.GAMEPLAY_SCENE));
            
        }
        else 
        {
            StartCoroutine(PlayerDied(Tags.MAINMENU_SCENE));
        }
        
    }    

    public void IncrementHealth()
    {
        health++;
        healthText.text = health.ToString();
    }    

    public void IncrementScore(float scoreValue)
    {
        if (canCountScore)
        {
            score += scoreValue;
            scoreText.text = score.ToString();
        }    
    }

    IEnumerator PlayerDied(string sceneName)
    {
        canCountScore = false;  //player died so cannot count score
        bgScroller.canScroll = false;   // stop scroll the background

        GameManager.instance.score = this.score;
        GameManager.instance.health = this.health;
        GameManager.instance.gameRestartedPlayerDied = true;

        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(sceneName);  //reload the scene
    }    

    public void PauseGame()
    {
        //stop score counting
        canCountScore = false;

        bgScroller.canScroll = false;

        //activate the pause panel
        pausePanel.gameObject.SetActive(true);

        //pause everything
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        canCountScore = true;

        pausePanel.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;    //make the game still running in the main menu scene
        SceneManager.LoadScene(Tags.MAINMENU_SCENE);
    }

    public void Reload()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Tags.GAMEPLAY_SCENE);
    }

}   //class
