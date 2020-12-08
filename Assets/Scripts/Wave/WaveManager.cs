using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int lastWave = 3;
    private int waveCounter;
    public float prepareTimer = 60f;
    public float waveTimer = 60f;
    private float prepareTime;
    private float waveTime;
    
    
    private float enemyCounter = 0;

    private bool isPreparation = true;
    private bool spawning = false;
    private bool lastSpawn = true;
    
    private Text timer;
    private Text waveCount;
    private Text enemiesLeft;

    private Spawner [] spawners;
    
    public GameObject [] enemies;
    
    void Start()
    {
        prepareTime = prepareTimer;
        waveTime = waveTimer;
        spawners = GameObject.FindObjectsOfType<Spawner>();
        timer = GameObject.Find("TimerManager").GetComponent<Text>();
        waveCount = GameObject.Find("WaveManager").GetComponent<Text>();
        enemiesLeft = GameObject.Find("EnemiesRemain").GetComponent<Text>();
        
        waveCounter = 1;
    }

    private void Update()
    {
        enemiesLeft.text = enemyCounter.ToString();
        
        if (waveCounter == lastWave && enemyCounter == 0)
        {
            waveCount.text = "you Won!";
            timer.text = "";
        }
        else if (waveCounter == lastWave && enemyCounter > 0)
        {
            waveCount.text = "Last wave, Kill all the enemies!";
            timer.text = "";
        }
        else
        {
            if (spawning && isPreparation == false)
            {
                enemyCounter = 0;
                foreach (var spawner in spawners)
                {
                    spawner.spawnEnemies(spawner.getSpawnpoints(), enemies);
                    enemyCounter += spawner.getEmemies();
                }
                spawning = false;
            }

            if (waveTime <= 0f && isPreparation == false)
            {
                isPreparation = true;
                waveCounter++;
            }

            if (waveCounter == lastWave)
            {
                enemyCounter = 0;
                foreach (var spawner in spawners)
                {
                    spawner.spawnEnemies(spawner.getSpawnpoints(), enemies);
                    enemyCounter += spawner.getEmemies();
                }
            }
            else
            {
                if (prepareTime > 0f && isPreparation) 
                {
                    waveTime = setTimer(waveTimer);
                    waveCount.text = $"Prepare for the {waveCounter} Wave";
                    prepareTime -= Time.deltaTime;
                    timer.text = prepareTime.ToString("f2");
                    spawning = true;
                }
                else
                {
                    isPreparation = false;
                    prepareTime = setTimer(prepareTimer);
                    waveCount.text = waveCounter + " Wave";
                    timer.text = waveTime.ToString("f2"); 
                    waveTime -= Time.deltaTime;
                } 
            }
        }
    }
    float setTimer(float time)
    {
        return time;
    }

}
