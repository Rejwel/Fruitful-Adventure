using System.Collections;
using UnityEngine;

public class FindingTurret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float fireRate = 1f;

    private float fireCountdown = 0f;
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "";
    public string Building;
    public string deafult;
    public TurretDetecting TurretInfo;

    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
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

    // Update is called once per frame
    void Update()
    {
        TurretInfo = FindObjectOfType<TurretDetecting>();
        if (target == null)
        {
            TurretInfo.CurrentBuilding = deafult;
            return;
        }

        if (Time.time >= fireCountdown)
        {
            TurretInfo.CurrentBuilding = Building;
            fireCountdown = 1f / fireRate + Time.time;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
