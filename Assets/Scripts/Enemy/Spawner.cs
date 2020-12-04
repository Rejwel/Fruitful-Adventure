using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEditorInternal;
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
    private int waveCounter;
    private int enemyCounter;

    private float prepareTime = 2f;
    private float waveTime = 2f;
    
    
    private bool isPreparation = true;
    private bool spawning = false;
    private bool lastSpawn = true;

    private Transform [,] spawnPoints = new Transform[4, 4];
    private Transform [] spawnPointsArray;
    List<Transform> spawnPointsList = new List<Transform>();
    public GameObject [] enemies;
    private Text timer;
    private Text waveCount;
    private Text enemiesLeft;
    private void Start()
    {
        waveCounter = 1;
        
        timer = GameObject.Find("TimerManager").GetComponent<Text>();
        waveCount = GameObject.Find("WaveManager").GetComponent<Text>();
        enemiesLeft = GameObject.Find("EnemiesRemain").GetComponent<Text>();

        enemiesLeft.text = enemyCounter.ToString();
        //spawnPoints = new Transform[4, 4];
        setPositions(spawnPoints);

        spawnPointsArray = GetComponentsInChildren<Transform>();
        spawnPointsList = spawnPointsArray.ToList();
        spawnPointsList.Remove(spawnPointsList[0]);
        
        //spawnEnemies(spawnPointsList, enemies);
    }

    private void Update()
    {
        enemiesLeft.text = enemyCounter.ToString();

            if (waveCounter == 3 && enemyCounter == 0)
            {
                waveCount.text = "you Won!";
                timer.text = "";
            }
            else if (waveCounter == 3 && enemyCounter > 0)
            {
                waveCount.text = "Last wave, Kill all the enemies!";
                timer.text = "";
            }
            else
            {
                if (spawning && isPreparation == false)
                {
                    spawnEnemies(spawnPointsList, enemies);
                    spawning = false;
                }

                if (waveTime <= 0f && isPreparation == false)
                {
                    isPreparation = true;
                    waveCounter++;
                }
                if(waveCounter == 3) spawnEnemies(spawnPointsList, enemies);
                else
                {
                    if (prepareTime > 0f && isPreparation) 
                    {
                        waveTime = 2f;
                        waveCount.text = $"Prepare for the {waveCounter} Wave";
                        prepareTime -= Time.deltaTime;
                        timer.text = prepareTime.ToString("f2");
                        spawning = true;
                    }
                    else
                    {
                        isPreparation = false;
                        prepareTime = 2f;
                        waveCount.text = waveCounter + " Wave incoming";
                        timer.text = waveTime.ToString("f2"); 
                        waveTime -= Time.deltaTime;
                    } 
                }
            }
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
            //Instantiate(enemies[Random.Range(0,4)], spawnpoint.position, spawnpoint.rotation);
            Instantiate(enemies[1], spawnpoint.position, spawnpoint.rotation);
            enemyCounter++;
        }
    }

    public void EnemyKill()
    {
        enemyCounter--;
    }
    
    
}
