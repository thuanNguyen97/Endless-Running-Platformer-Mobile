using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEneble()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Tags.GAMEPLAY_SCENE)
        {
            if (GameManager.instance.gameStartedFromMainMenu)
            {
                Debug.Log("Game was started from Main Menu");
            }
        }
    }
    
}   //class
