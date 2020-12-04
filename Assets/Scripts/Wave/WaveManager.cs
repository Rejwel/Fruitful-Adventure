using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    private int waveCounter;
    private float prepareTime = 5f;
    private float waveTime = 5f;
    
    private bool isPreparation = true;
    private bool spawning = false;
    private bool lastSpawn = true;
    
    private Text timer;
    private Text waveCount;
    private Text enemiesLeft;

    private Spawner spawner;
    
    public GameObject [] enemies;
    
    void Start()
    {
        spawner = GameObject.FindObjectOfType<Spawner>();
        timer = GameObject.Find("TimerManager").GetComponent<Text>();
        waveCount = GameObject.Find("WaveManager").GetComponent<Text>();
        enemiesLeft = GameObject.Find("EnemiesRemain").GetComponent<Text>();
        
        waveCounter = 1;
    }

    private void Update()
    {
        
            enemiesLeft.text = spawner.getEmemies().ToString();
    
                if (waveCounter == 3 && spawner.getEmemies() == 0)
                {
                    waveCount.text = "you Won!";
                    timer.text = "";
                }
                else if (waveCounter == 3 && spawner.getEmemies() > 0)
                {
                    waveCount.text = "Last wave, Kill all the enemies!";
                    timer.text = "";
                }
                else
                {
                    if (spawning && isPreparation == false)
                    {
                        spawner.spawnEnemies(spawner.getSpawnpoints(), enemies);
                        spawning = false;
                    }
    
                    if (waveTime <= 0f && isPreparation == false)
                    {
                        isPreparation = true;
                        waveCounter++;
                    }
                    if(waveCounter == 3) spawner.spawnEnemies(spawner.getSpawnpoints(), enemies);
                    else
                    {
                        if (prepareTime > 0f && isPreparation) 
                        {
                            waveTime = 5f;
                            waveCount.text = $"Prepare for the {waveCounter} Wave";
                            prepareTime -= Time.deltaTime;
                            timer.text = prepareTime.ToString("f2");
                            spawning = true;
                        }
                        else
                        {
                            isPreparation = false;
                            prepareTime = 5f;
                            waveCount.text = waveCounter + " Wave";
                            timer.text = waveTime.ToString("f2"); 
                            waveTime -= Time.deltaTime;
                        } 
                    }
                }
        }
}
