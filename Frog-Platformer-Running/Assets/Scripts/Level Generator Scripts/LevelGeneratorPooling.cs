using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorPooling : MonoBehaviour
{
    [SerializeField]
    private Transform _platform, _platform_Parent;

    [SerializeField]
    private Transform _monster, _monster_Parent;

    [SerializeField]
    private Transform _health_Collectable, _health_Collectable_Parent;

    [SerializeField]
    private int _levelLength = 100;

    [SerializeField]
    private float _distance_Between_Platform = 15f;

    [SerializeField]
    private float _MIN_Position_Y = 0f, _MAX_Position_Y = 7f;

    [SerializeField]
    private float _chanceForMonsterExistence = 0.25f, _chanceForHealthCollectableExistence = 0.1f;

    [SerializeField]
    private float _healthCollectable_MinY = 1f, _healthCollectable_MaxY = 3f;

    private float _platformLastPositionX;
    private Transform[] platform_Array;


    // Start is called before the first frame update
    void Start()
    {
        CreatePlatforms();
    }

    void CreatePlatforms()
    {
        platform_Array = new Transform[_levelLength];

        for (int i = 0; i < platform_Array.Length; i++)
        {
            Transform newPlatform = (Transform)Instantiate(_platform, Vector3.zero, Quaternion.identity);
            platform_Array[i] = newPlatform; 
        }

        for (int i = 0; i < platform_Array.Length; i++)
        {
            float platformPositionY = Random.Range(_MIN_Position_Y, _MAX_Position_Y);

            Vector3 platformPosition;

            if (i < 2)
            {
                platformPositionY = 0f; // the player need a platform to stand on in the begining
            }

            platformPosition = new Vector3(_distance_Between_Platform * i, platformPositionY, 0);

            _platformLastPositionX = platformPosition.x;

            platform_Array[i].position = platformPosition;
            platform_Array[i].parent = _platform_Parent;

            //spaw monster and health collectables
        }

    }

    public void PoolingPlatforms()
    {
        for (int i = 0; i < platform_Array.Length; i++)
        {
            if (!platform_Array[i].gameObject.activeInHierarchy)
            {
                //activate the game object again
                platform_Array[i].gameObject.SetActive(true);

                float platformPositionY = Random.Range(_MIN_Position_Y, _MAX_Position_Y);

                Vector3 platformPosition = new Vector3(_distance_Between_Platform + _platformLastPositionX, platformPositionY, 0);

                platform_Array[i].position = platformPosition;

                _platformLastPositionX = platformPosition.x;
            }

        }    
    }    

}   //class
