using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]    private int levelLenght;
    [SerializeField]    private int startPlatformLenght, endPlatformLenght;
    [SerializeField]    private int distanceBetweenPlatform;
    [SerializeField]    private Transform platformPrefabs, platformParent;
    [SerializeField]    private Transform monster, monsterParent;
    [SerializeField]    private Transform healththCollectable, healthCollectableParent;
    [SerializeField]    private float platformPositionMinY = 0f, platformPositionMaxY = 10f;
    [SerializeField]    private int platformLenghtMin = 1, platformLenghtMax = 4; 
    [SerializeField]    private float chanceForMonsterExistence = 0.25f, chanceForCollectable = 0.1f;
    [SerializeField]    private float healthCollectableMinY = 1f, healthCollectableMaxY = 3f;

    private float platformLastPositionX;

    private enum PlatformType
    {
        None,
        Flat,

    }    
        
    private class PlatformPositionInfo
    {
        public PlatformType platformType;
        public float positionY;
        public bool hasMonster;
        public bool hasHealthCollectable;

        //constructor
        public PlatformPositionInfo(PlatformType type, float posY, bool has_monster, bool has_Health)
        {
            this.platformType = type;
            this.positionY = posY;
            this.hasMonster = has_monster;
            this.hasHealthCollectable = has_Health;
        }
    }    //class platform pos information

    private void Start()
    {
        GenerateLevel(true);
    }

    void FillOutPositionInfo(PlatformPositionInfo[] platformInfo)
    {
        int currentPlatformInfoIndex = 0;

        for (int i = 0; i < startPlatformLenght; i++)
        {
            platformInfo [currentPlatformInfoIndex].platformType = PlatformType.Flat;
            platformInfo [currentPlatformInfoIndex].positionY = 0f;

            currentPlatformInfoIndex++;
        }    

        while (currentPlatformInfoIndex < levelLenght - endPlatformLenght)
        {
            if (platformInfo[currentPlatformInfoIndex - 1].platformType != PlatformType.None)
            {
                currentPlatformInfoIndex++;
                continue;
            }

            float platformPositionY = Random.Range(platformPositionMinY, platformPositionMaxY);

            int platformLenght = Random.Range(platformLenghtMin, platformLenghtMax);

            for (int i  = 0; i < platformLenght; i++)
            {
                bool has_Monster = (Random.Range(0f, 1f) < chanceForMonsterExistence);
                bool has_healthCollectable = (Random.Range(0f, 1f) < chanceForCollectable);

                platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = platformPositionY;
                platformInfo[currentPlatformInfoIndex].hasMonster = has_Monster;
                platformInfo[currentPlatformInfoIndex].hasHealthCollectable = has_healthCollectable;

                currentPlatformInfoIndex++;

                if (currentPlatformInfoIndex > (levelLenght - endPlatformLenght))
                {
                    currentPlatformInfoIndex = levelLenght - endPlatformLenght;
                    break;
                }    

                for (i = 0; i < endPlatformLenght; i++)
                {
                    platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                    platformInfo[currentPlatformInfoIndex].positionY = 0f;

                    currentPlatformInfoIndex++;
                }    
            }    

        } // while loop
    }

    void CreatePlatformsFromPositionInfo(PlatformPositionInfo[] platformPositionInfo, bool gameStarted)
    {
        for (int i = 0; i < platformPositionInfo.Length; i++)
        {
            PlatformPositionInfo positionInfo = platformPositionInfo[i];

            if (positionInfo.platformType == PlatformType.None)
            {
                continue;
            }

            Vector3 platformPosition;

            // here we are going to check if the game is started or not
            if (gameStarted)
            {
                //generate the level as the game started
                platformPosition = new Vector3(distanceBetweenPlatform * i, positionInfo.positionY, 0);
            }
            else
            {
                //genetate the next level when the player collide with the More Platform object (as the platformLastPositionX)
                platformPosition = new Vector3(distanceBetweenPlatform + platformLastPositionX, positionInfo.positionY, 0);
            }

            // save the platform postion x for later use 
            platformLastPositionX = platformPosition.x;

            Transform createBlock = (Transform)Instantiate(platformPrefabs, platformPosition, Quaternion.identity);
            createBlock.parent = platformParent;

            if (positionInfo.hasMonster)
            {
                // code later
            }

            if (positionInfo.hasHealthCollectable)
            {
                // code later
            }


        }    // for loop
    }    

    public void GenerateLevel(bool gameStarted)
    {
        PlatformPositionInfo[] platformInfo = new PlatformPositionInfo[levelLenght];

        for (int i = 0; i < platformInfo.Length; i++)
        {
            platformInfo[i] = new PlatformPositionInfo(PlatformType.None, -1f, false, false);
        }    

        FillOutPositionInfo(platformInfo);

        CreatePlatformsFromPositionInfo(platformInfo, gameStarted);

    }    

} // class
