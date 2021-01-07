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
    
    private Transform[] spawnPointsArray;
    List<Transform> spawnPointsList = new List<Transform>();

    private void Start()
    {

        spawnPointsArray = GetComponentsInChildren<Transform>();
        spawnPointsList = spawnPointsArray.ToList();
        spawnPointsList.Remove(spawnPointsList[0]);
    }

    public List<Transform> getSpawnpoints()
    {
        return spawnPointsList;
    }

}
