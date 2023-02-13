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

    void Awake()
    {
        MakeInstance();

        scoreText = GameObject.Find(Tags.SCORE_TEXT_OBJ).GetComponent<Text>();
        healthText = GameObject.Find(Tags.HEALTH_TEXT_OBJ).GetComponent<Text>();
        levelText = GameObject.Find(Tags.LEVEL_TEXT_OBJ).GetComponent<Text>();
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
        if (scene.name == "Gameplay")
        {
            
            if (GameManager.instance.gameStartedFromMainMenu)
            {
                Debug.Log("Game was started from Main Menu");
                //GameManager.instance.gameStartedFromMainMenu = false;

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
            // restart the game
            healthText.text = health.ToString();


        }
        else 
        {
            
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
        yield return new WaitForSecondsRealtime(2f);
    }    

}   //class
