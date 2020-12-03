using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private Transform [,] spawnPoints;
    private Transform [] spawnPointsArray;
    List<Transform> spawnPointsList = new List<Transform>();
    public GameObject [] enemies;
    private void Start()
    {
        spawnPoints = new Transform[4, 4];
        setPositions(spawnPoints);

        spawnPointsArray = GetComponentsInChildren<Transform>();
        spawnPointsList = spawnPointsArray.ToList();
        spawnPointsList.Remove(spawnPointsList[0]);
        
        spawnEnemies(spawnPointsList, enemies);
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

    void spawnEnemies(List<Transform> Spawnpoints, GameObject [] enemies)
    {
        foreach (var spawnpoint in Spawnpoints)
        {
            print(spawnpoint.name);
            Instantiate(enemies[Random.Range(0,4)], spawnpoint.position, spawnpoint.rotation);
        }
    }
    
    
}
