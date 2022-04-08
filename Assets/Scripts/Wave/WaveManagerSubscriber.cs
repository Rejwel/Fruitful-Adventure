using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveManagerSubscriber : MonoBehaviour
{
    private int _wave = 0;
    private float _enemiesLeft = 0;
    private Text _waveCountText;
    private Text _enemiesLeftText;
    private string WaveTextGui;
    
    [SerializeField] private WaveManager waveManager;
    public static GameObject AttackingBuilding { get; set; }
    private GameObject NextAttackingBuilding { get; set; }
    private int WhichBuildingToAttack { get; set; }
    private List<GameObject> Buildings { get; set; }
    public int BuildingCount;
    
    [SerializeField] private float meleeEnemiesSpawnScaleFactor; // 20 -> 100 max, jump [5]
    [SerializeField] private float rangedEnemiesSpawnScaleFactor; // 5 -> 100 max, jump [5]
    [SerializeField] private float enemiesBetterTypeScaleFactor; // 20 -> 65 max, jump [5]
    [SerializeField] private string waveCombo; // "0123" | "0" -> 0 spawner on
    [SerializeField] private Spawner[] spawners;
    [SerializeField] private GameObject[] enemies;
    
    void Awake()
    {
        waveManager = GetComponent<WaveManager>();
        waveManager.OnStartSetup += SetSetupParameters;
        waveManager.OnStartSetup += GetAllStartingBuildings;
        waveManager.OnStartSetup += GetStartingAttackPoints;
        waveManager.TurnOff += TurnOffWaveManager;
        waveManager.MainLogic += MainLogic;
    }
    
    // EVENTS FOR WAVEMANAGER
    private void GetStartingAttackPoints()
    {
        AttackingBuilding = GetFirstAttackPoint();
        AttackingBuilding.GetComponent<SphereCollider>().enabled = true;
        GetNextBuilding();
        waveManager.OnStartSetup -= GetStartingAttackPoints;
    }

    private void GetAllStartingBuildings()
    {
        Buildings = GetSceneObjects(18);
        BuildingCount = Buildings.Count;
        waveManager.OnStartSetup -= GetAllStartingBuildings;
    }

    private void SetSetupParameters()
    {
        waveCombo = "0123";
        meleeEnemiesSpawnScaleFactor = 20;
        rangedEnemiesSpawnScaleFactor = 5;
        enemiesBetterTypeScaleFactor = 20;
        _waveCountText = GameObject.Find("WaveManager").GetComponent<Text>();
        _enemiesLeftText = GameObject.Find("EnemiesRemain").GetComponent<Text>();
        waveManager.OnStartSetup -= SetSetupParameters;
    }

    private void TurnOffWaveManager()
    {
        if (BuildingCount.Equals(0))
        {
            GetComponent<WaveManager>().enabled = false;
            waveManager.MainLogic -= MainLogic;
            waveManager.TurnOff -= TurnOffWaveManager;
        }
    }

    private void MainLogic()
    {
        WaveTextGui = AttackingBuilding == null ? "Destroyed!" : AttackingBuilding.name;

        if (_wave == 0)
        {
            _waveCountText.text = "First Wave incoming, prepare your defense!" + " Press (F) to start!" +
                                  "\n now attacking: " + WaveTextGui +
                                  "\n next attacking: " + NextAttackingBuilding.name;
            if (Input.GetKeyDown(KeyCode.F))
            {
                SpawnFirstWave();
            }
        }
        else if (_wave == 11)
        {
            _waveCountText.text = "Last wave " + _wave + "\n now attacking: " + WaveTextGui;
        }
        else
        {
            if (_enemiesLeft == 0)
            {
                _enemiesLeftText.text = "All enemies defeated!\nPress (F) to start next wave!";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    IncreaseSpawnFactor();
                    IncreaseBetterTypeScaleFactor();
                    NextWave();
                }
            }
            else
            {
                _enemiesLeftText.text = "Enemies left : " + _enemiesLeft;
            }

            if (BuildingCount != 1)
            {
                _waveCountText.text = _enemiesLeft == 0
                    ? _waveCountText.text = "Wave " + _wave + "\n next attacking: " + NextAttackingBuilding.name
                    : _waveCountText.text = "Wave " + _wave + "\n now attacking: " + WaveTextGui + "\n next attacking: " +
                                            NextAttackingBuilding.name;
            }
            else
                _waveCountText.text = "Wave " + _wave + "\n This is your last building!: " + NextAttackingBuilding.name;
        }
    }

    // HELPERS
    public void GetBuildings()
    {
        Buildings.Clear();
        Buildings = GetSceneObjects(18);
        BuildingCount = Buildings.Count;
    }
    
    private static List<GameObject> GetSceneObjects(int layer)
    {
        return Resources.FindObjectsOfTypeAll<GameObject>()
            .Where(go => go.layer == layer).ToList();
    }


    private GameObject GetFirstAttackPoint()
    {
        WhichBuildingToAttack = Random.Range(0, Buildings.Count);
        return Buildings[WhichBuildingToAttack];
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
                NextAttackingBuilding = GetFirstAttackPoint();
            }
        }
    }
    
    private void SpawnFirstWave()
    {
        spawnEnemies(getSpawns(waveCombo), enemiesSpawnType());
        _wave++;
    }

    private void NextWave()
    {
        GetBuildings();
        SetAttack();
        spawnEnemies(getSpawns(waveCombo), enemiesSpawnType());
        _wave++;
    }

    public void UpdateEnemyCounter()
    {
        _enemiesLeft--;
    }

    public void SetAttack()
    {
        // setting new attack points
        if (AttackingBuilding != null)
            AttackingBuilding.GetComponent<SphereCollider>().enabled = false;

        AttackingBuilding = NextAttackingBuilding;
        AttackingBuilding.GetComponent<SphereCollider>().enabled = true;
        GetNextBuilding();
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
                if (!(enemiesToSpawn[j] == null))
                {
                    _enemiesLeft++;
                    Instantiate(enemiesToSpawn[j], spawnpoint.position, spawnpoint.rotation);
                }

                j++;
            }
        }
    }

    
    private bool IsEquitableForMeleeEnemiesSpawn()
    {
        int spawnScaleFactor = Random.Range(0, 100);
        if (Enumerable.Range(0, (int) meleeEnemiesSpawnScaleFactor).Contains(spawnScaleFactor))
            return true;
        return false;
    }
    
    private bool IsEquitableForRangedEnemiesSpawn()
    {
        int spawnScaleFactor = Random.Range(0, 100);
        if (Enumerable.Range(0, (int) rangedEnemiesSpawnScaleFactor).Contains(spawnScaleFactor))
            return true;
        return false;
    }
    
    

    private bool IsEquitableForBetterEnemySpawn()
    {
        int spawnScaleFactor = Random.Range(0, 100);
        if (Enumerable.Range(0, (int) enemiesBetterTypeScaleFactor).Contains(spawnScaleFactor))
            return true;
        return false;
    }

    private void IncreaseSpawnFactor()
    {
        meleeEnemiesSpawnScaleFactor =
            meleeEnemiesSpawnScaleFactor < 100 ? meleeEnemiesSpawnScaleFactor += 5 : meleeEnemiesSpawnScaleFactor;
        rangedEnemiesSpawnScaleFactor =
            rangedEnemiesSpawnScaleFactor < 100 ? rangedEnemiesSpawnScaleFactor += 5 : rangedEnemiesSpawnScaleFactor;
    }
    
    private void IncreaseBetterTypeScaleFactor()
    {
        enemiesBetterTypeScaleFactor =
            enemiesBetterTypeScaleFactor < 65 ? enemiesBetterTypeScaleFactor += 5 : enemiesBetterTypeScaleFactor;
    }

    public GameObject[] enemiesSpawnType()
    {
        // 0 - mage
        // 1 - melee
        // 2 - range
        // 3 - tank
        GameObject[] enemiesToSpawn = new GameObject[16];
        
        // soon there will be also boss spawn
        if (_wave != 4) 
        {
        // front spawn, only melee and tanks
        for (int i = 0; i < 8; i++)
        {
            enemiesToSpawn[i] = IsEquitableForMeleeEnemiesSpawn()
                ? enemiesToSpawn[i] = IsEquitableForBetterEnemySpawn() ? enemies[3] : enemies[1]
                : null;
        }
        // back spawn, only mage and range
        for (int i = 8; i < 16; i++)
        {
            enemiesToSpawn[i] = IsEquitableForRangedEnemiesSpawn()
                ? enemiesToSpawn[i] = IsEquitableForBetterEnemySpawn() ? enemies[0] : enemies[2]
                : null;
        }
        }
        // check if spawn equals null
        foreach (var spawn in enemiesToSpawn)
        {
            if (spawn != null) return enemiesToSpawn;
        }

        return enemiesSpawnType();
    }
}
