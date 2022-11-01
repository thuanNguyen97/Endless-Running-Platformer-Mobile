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

                for (int i = 0; i < endPlatformLenght; i++)
                {
                    platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                    platformInfo[currentPlatformInfoIndex].positionY = 0f;

                    currentPlatformInfoIndex++;
                }    
            }    

        } // while loop
    }

} // class
