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

    private Transform [][] spawnPoints;
    private Transform spawnPoint;
    private void Start()
    {
        spawnPoint = this.transform;
        setPositions(spawnPoints);
    }

    void setPositions(Transform [][] spawnPoints)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                string obj = "spawn" + i + j;
                //print(obj);
                //print(GameObject.Find(obj).transform);
                //spawnPoints[i][j] = GameObject.Find(obj).transform;
                //print(spawnPoints[i][j]);
            }
        }
    }
    
    
}
