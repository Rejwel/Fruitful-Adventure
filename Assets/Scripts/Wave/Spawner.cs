using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // SCHEME
    //    MAGE MELEE RANGE TANK
    //    POSITIONS [][]
    //    I        I    II    III    IV      MELEE TANK
    //    II       I    II    III    IV      MELEE TANK
    //    III      I    II    III    IV      RANGED MAGE
    //    VI       I    II    III    IV      RANGED MAGE
    //    
    //
    // SETTING SPAWNPOINTS
    private int enemyCounter;

    //private Transform[,] spawnPoints = new Transform[4, 4];
    private Transform[] spawnPointsArray;
    List<Transform> spawnPointsList = new List<Transform>();
    public GameObject[] enemies;

    private void Start()
    {
        //setPositions(spawnPoints);

        spawnPointsArray = GetComponentsInChildren<Transform>();
        spawnPointsList = spawnPointsArray.ToList();
        spawnPointsList.Remove(spawnPointsList[0]);
        setEnemies();
    }
    // void setPositions(Transform [,] spawnPoints)
    // {
    //     for (int i = 0; i < 4; i++)
    //     {
    //         for (int j = 0; j < 4; j++)
    //         {
    //             string obj = "spawn" + i + j;
    //             spawnPoints[i, j] = GameObject.Find(obj).transform;
    //         }
    //     }
    // }

    // void setPositions(Transform[,] spawnPoints)
    // { 
    //     Transform[] spawns = GetComponentsInChildren<Transform>();
    //     int counter = 0;
    //     for (int i = 0; i < 4; i++)
    //     {
    //         for (int j = 0; j < 4; j++)
    //         {
    //             spawnPoints[i, j] = spawns[counter++];
    //         }
    //     }
    // }

    public void spawnEnemies(List<Transform> Spawnpoints, GameObject [] enemies)
    {
        for (int i = 0; i < Spawnpoints.Count; i++)
        {
            if (i < 4) Instantiate(enemies[3], Spawnpoints[i].position, Spawnpoints[i].rotation);
            if (i > 3 && i < 8) Instantiate(enemies[1], Spawnpoints[i].position, Spawnpoints[i].rotation);
            if (i > 7 && i < 12) Instantiate(enemies[0], Spawnpoints[i].position, Spawnpoints[i].rotation);
            if (i > 11 && i < 17) Instantiate(enemies[2], Spawnpoints[i].position, Spawnpoints[i].rotation);
        }
    }

    void setEnemies()
    {
        foreach (var spawnpoint in spawnPointsList)
        {
            enemyCounter++;
        }
    }
    public int getEmemies()
    {
        return enemyCounter;
    }

    public List<Transform> getSpawnpoints()
    {
        return spawnPointsList;
    }

}
