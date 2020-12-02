using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Transform [,] spawnPoints;
    private Transform spawnPoint;
    public GameObject[] enemies;
    private void Start()
    {
        spawnPoints = new Transform[4, 4];
        setPositions(spawnPoints);
        spawnEnemies(spawnPoints);
    }

    void setPositions(Transform [,] spawnPoints)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                string obj = "spawn" + i + j;
                spawnPoints[i, j] = GameObject.Find(obj).transform;
            }
        }
    }

    void spawnEnemies(Transform [,] spawnPoints)
    { 
        // | 0 - mage | 1 - melee | 2 - ranged | 3 - tank | ARRAY INDEX | 
        for (int i = 0; i < 4; i++)
        {
            print(spawnPoints[0,i]);
            Instantiate(enemies[3], spawnPoints[0, i].position, spawnPoints[0, i].rotation);
        }
        
        for (int i = 0; i < 4; i++)
        {
            print(spawnPoints[1,i]);
            Instantiate(enemies[1], spawnPoints[1, i].position, spawnPoints[1, i].rotation);
        }
        
        for (int i = 0; i < 4; i++)
        {
            print(spawnPoints[2,i]);
            Instantiate(enemies[0], spawnPoints[2, i].position, spawnPoints[2, i].rotation);
        }
        
        for (int i = 0; i < 4; i++)
        {
            print(spawnPoints[3,i]);
            Instantiate(enemies[2], spawnPoints[3, i].position, spawnPoints[3, i].rotation);
        }
    }
    
    
}
