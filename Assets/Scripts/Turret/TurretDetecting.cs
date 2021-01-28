using UnityEngine;
using UnityEngine.UI;


public class TurretDetecting : MonoBehaviour
{
    private Transform target;
    private AreaControl AC;

    [Header("Attributes")]
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public string Turret = "TurretDetecting";
    public GameObject progressText;
    public string CurrentBuilding = "cos";
    public float shortestDistanceToBuilding;
    public GameObject House;

    private void Awake()
    {
        AC = FindObjectOfType<AreaControl>();
        progressText = AC.WarningCanvas;
        House = GameObject.Find("house");
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = House;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            
        }
        else
        {
            target = null;
            
        }
    }

    void UpdateBuilding()
    {
        GameObject[] Building = GameObject.FindGameObjectsWithTag(Turret);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = House;
        foreach (GameObject enemy in Building)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        // print(nearestEnemy);
        if (nearestEnemy!=null)
        {
            CurrentBuilding = nearestEnemy.GetComponent<FindingTurret>().Building;
            shortestDistanceToBuilding = shortestDistance;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBuilding();
        // print(CurrentBuilding);
        // print(shortestDistanceToBuilding);
        if (target == null)
        {
            AC.CloseCanvas();
            return;
        }
        
        AC.OpenCanvas();
        if (shortestDistanceToBuilding > range)
        {
            AC.DefaultWarning();
        }
        else
        {
            AC.BuildingWarning(CurrentBuilding);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}