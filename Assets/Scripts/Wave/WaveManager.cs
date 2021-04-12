using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public string waveCombo;
    private int wave = 0;
    private float enemiesLeft = 0;
    private float waveTime = 0;
    private float nextWaveTime;

    private bool waveEnded = false;

    private Text timerText;
    private Text waveCountText;
    private Text enemiesLeftText;
    private List<GameObject> Buildings { get; set; }
    public int BuildingCount;
    private int WhichBuildingToAttack { get; set; }
    public static GameObject AttackingBuilding { get; set; }
    private GameObject NextAttackingBuilding { get; set; }
    private EnemyRanged enemyType1;
    private EnemyMelee enemyType2;
    private string WaveTextGui;
    

    public Spawner [] spawners;
    public GameObject [] enemies;

    void Start()
    {
        timerText = GameObject.Find("TimerManager").GetComponent<Text>();
        waveCountText = GameObject.Find("WaveManager").GetComponent<Text>();
        enemiesLeftText = GameObject.Find("EnemiesRemain").GetComponent<Text>();
        enemyType1 = FindObjectOfType<EnemyRanged>();
        enemyType2 =  FindObjectOfType<EnemyMelee>();

        // get all buildings
        Buildings = GetSceneObjects(18);
        BuildingCount = Buildings.Count;
        
        AttackingBuilding = GetAttackPoint();
        AttackingBuilding.GetComponent<SphereCollider>().enabled = true;
        GetNextBuilding();
    }


    private void Update()
    {
        if(BuildingCount.Equals(0)) this.gameObject.SetActive(false);
        
        timerText.text = waveTime.ToString("f2");

        WaveTextGui = AttackingBuilding == null ? "Destroyed!" : AttackingBuilding.name;
        if (wave == 0)
        {
            waveCountText.text = "First Wave incoming, You have 60 seconds!" + "\n now attacking: " + WaveTextGui +
                                 "\n next attacking: " + NextAttackingBuilding.name;
        }
        else
        {
            if (BuildingCount != 1)
            {
                waveCountText.text = "Wave " + wave + "\n now attacking: " + WaveTextGui + "\n next attacking: " + NextAttackingBuilding.name;
            }
            else
            {
                waveCountText.text = "Wave " + wave + "\n This is your last building!: " + NextAttackingBuilding.name;
            }
        }
        
        enemiesLeftText.text = enemiesLeft.ToString();
        nextWaveTime = wave == 0 ? 60f : 90f;

        if (waveTime >= nextWaveTime && wave == 0)
        {
            waveTime = 0f;
            // getSpawns 1-4 (which spawners to activate), enemiesSpawnType type of spawn 1-11
            spawnEnemies(getSpawns(waveCombo), enemiesSpawnType(++wave));
        }
        else if (waveTime >= nextWaveTime)
        {
            GetBuildings();
            SetAttack();
            
            waveTime = 0f;
            spawnEnemies(getSpawns(waveCombo), enemiesSpawnType(++wave));
        }

        waveTime += Time.deltaTime;
    }

    private GameObject GetAttackPoint()
    {
        WhichBuildingToAttack = Random.Range(0, Buildings.Count);
        return Buildings[WhichBuildingToAttack];
    }

    public void SetAttack()
    {
        // setting new attack points
        if(AttackingBuilding != null)
            AttackingBuilding.GetComponent<SphereCollider>().enabled = false;
        
        AttackingBuilding = NextAttackingBuilding;
        AttackingBuilding.GetComponent<SphereCollider>().enabled = true;
        GetNextBuilding();
    }
    
    private void GetNextBuilding()
    {
        if (Buildings.Count.Equals(1))
        {
            AttackingBuilding = NextAttackingBuilding;
        }
        else
        {
           while (AttackingBuilding == NextAttackingBuilding || NextAttackingBuilding == null)
           {
               NextAttackingBuilding = GetAttackPoint();
           } 
        }
    }

    public void killEnemy()
    {
        enemiesLeft--;
    }
    
    public void GetBuildings()
    {
        Buildings.Clear();
        Buildings = GetSceneObjects(18);
        BuildingCount = Buildings.Count;
    }

    public List<Transform>[] getSpawns(string combo)
    {
        List<Transform>[] spawnersList = new List<Transform>[combo.ToString().Length];
        
        for (int i = 0; i < combo.Length; i++)
        {
            spawnersList[i] = spawners[int.Parse(combo.Substring(i, 1))].getSpawnpoints().ToList();
        }

        return spawnersList;
    }
    
    public void spawnEnemies(List<Transform>[] Spawnpoints, GameObject[] enemiesToSpawn)
    {
        for (int i = 0; i < Spawnpoints.Length; i++)
        {
            int j = 0;
            
            foreach (var spawnpoint in Spawnpoints[i])
            {
                if (enemiesToSpawn[j] == null);
                else
                {
                    enemiesLeft++;
                    Instantiate(enemiesToSpawn[j], spawnpoint.position, spawnpoint.rotation);
                }

                j++;
            }
        }
    }

    private static List<GameObject> GetSceneObjects(int layer)
    {
        return Resources.FindObjectsOfTypeAll<GameObject>()
            .Where(go => go.layer == layer).ToList();
    }

    public GameObject[] enemiesSpawnType(int x)
    {
        // 0 - mage
        // 1 - melee
        // 2 - range
        // 3 - tank
        GameObject[] enemiesToSpawn = new GameObject[16];
        switch (x)
        {
            case 1:
            {
                for (int i = 0; i < 4; i+=2)
                {
                    enemiesToSpawn[i] = enemies[1];
                }
                
                // this is for testing only!
                // for (int i = 0; i < 4; i++)
                // {
                //     enemiesToSpawn[i] = enemies[i];
                // }

                return enemiesToSpawn;
            }

            case 2:
            {
                enemiesToSpawn[1] = enemies[1];
                enemiesToSpawn[2] = enemies[1];
                enemiesToSpawn[7] = enemies[2];

                return enemiesToSpawn;
            }
            
            case 3:
            {
                enemiesToSpawn[1] = enemies[3];
                enemiesToSpawn[2] = enemies[1];
                enemiesToSpawn[4] = enemies[2];
                enemiesToSpawn[7] = enemies[0];

                return enemiesToSpawn;
            }
            
            case 4:
            {
                enemiesToSpawn[1] = enemies[3];
                enemiesToSpawn[2] = enemies[3];
                enemiesToSpawn[0] = enemies[1];
                enemiesToSpawn[3] = enemies[1];
                enemiesToSpawn[8] = enemies[0];
                enemiesToSpawn[11] = enemies[2];

                return enemiesToSpawn;
            }
            
            case 5:
            {
                enemiesToSpawn[0] = enemies[1];
                enemiesToSpawn[3] = enemies[1];
                enemiesToSpawn[1] = enemies[3];
                enemiesToSpawn[2] = enemies[3];
                enemiesToSpawn[12] = enemies[0];
                enemiesToSpawn[13] = enemies[2];
                enemiesToSpawn[14] = enemies[0];
                enemiesToSpawn[15] = enemies[2];

                return enemiesToSpawn;
            }
            
            case 6:
            {
                for(int i = 0; i < 8; i++)
                {
                    enemiesToSpawn[i] = enemies[1];
                }

                return enemiesToSpawn;
            }
            
            case 7:
            {
                for (int i = 0; i < 4; i++)
                {
                    enemiesToSpawn[i] = enemies[3];
                }
                enemiesToSpawn[4] = enemies[1];
                enemiesToSpawn[7] = enemies[1];
                enemiesToSpawn[7] = enemies[0];
                enemiesToSpawn[7] = enemies[0];

                return enemiesToSpawn;
            }
            
            case 8:
            {
                for (int i = 0; i < 4; i++)
                {
                    enemiesToSpawn[i] = enemies[3];
                }

                enemiesToSpawn[8] = enemies[0];
                enemiesToSpawn[9] = enemies[0];
                enemiesToSpawn[10] = enemies[0];
                enemiesToSpawn[11] = enemies[0];

                return enemiesToSpawn;
            }
            
            case 9:
            {
                for (int i = 0; i < 4; i++)
                {
                    enemiesToSpawn[i] = enemies[3];
                }
                
                enemiesToSpawn[8] = enemies[0];
                enemiesToSpawn[9] = enemies[0];
                enemiesToSpawn[10] = enemies[0];
                enemiesToSpawn[11] = enemies[0];
                enemiesToSpawn[12] = enemies[2];
                enemiesToSpawn[13] = enemies[2];
                enemiesToSpawn[14] = enemies[2];
                enemiesToSpawn[15] = enemies[2];

                return enemiesToSpawn;
            }
            
            case 10:
            {
                for (int i = 0; i < 16; i++)
                {
                    enemiesToSpawn[i] = enemies[Random.Range(0, 4)];
                }

                return enemiesToSpawn;
            }
            
            case 11:
            {
                print("boss");

                return enemiesToSpawn;
            }

            default:
                print("default spawnType");
                return enemiesToSpawn;
        }
    }

}
