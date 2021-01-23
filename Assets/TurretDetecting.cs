using UnityEngine;
using UnityEngine.UI;


public class TurretDetecting : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float fireRate = 1f;

    private float fireCountdown = 0f;
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;
    public GameObject progressText;
    public string CurrentBuilding;

    // Start is called before the first frame update
    void Start()
    {
        progressText = GameObject.Find("WarningText");
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
        if (target == null)
        {
            return;
        }

        //Target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Time.time >= fireCountdown)
        {
            progressText.GetComponent<Text>().text = "Wykryto wroga obok " + CurrentBuilding;
            fireCountdown = 1f / fireRate + Time.time;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}