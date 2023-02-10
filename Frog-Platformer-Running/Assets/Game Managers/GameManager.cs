using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    
    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);    //the instance just can be exist once 
        }
        else
        {
            instance = this;    //"this" is refer to the class, "GameManager" class
            DontDestroyOnLoad(gameObject);  //make the game object that hold this class don't destroy when we load a new scene
        }

        
    }
}   //class
