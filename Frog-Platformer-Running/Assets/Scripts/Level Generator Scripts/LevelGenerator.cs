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

} // class
