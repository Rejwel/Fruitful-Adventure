using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform _target;
    public GameObject[] BulletObjects;
    private int _numberOfBullets;
    [Header("Attributes")]
    public float fireRate = 1f;

    public TurretInfo reloadTurret;
    public TurretInfoScript turretInfoScript;

    public int maximumAmmountOfAmmunition;
    private int currentAmmountOfAmmunition;
    private bool enoughAmmunition = true;

    private float fireCountdown = 0f;
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject BulletPrefab;
    public Transform firePoint;
    public Money money;
    private float basicMagazine;
    private float basicFireRate;
    
    private void Awake()
    {
        basicMagazine = maximumAmmountOfAmmunition;
        basicFireRate = fireRate;
        money = FindObjectOfType<Money>();
        reloadTurret = FindObjectOfType<TurretInfo>();
        turretInfoScript = FindObjectOfType<TurretInfoScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        currentAmmountOfAmmunition = maximumAmmountOfAmmunition;
        enoughAmmunition = true;
    }

    void UpdateTarget(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies){
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance){
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range){
            _target = nearestEnemy.transform;
        } else {
            _target = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            reloadTurret.OpenCanvas();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            reloadTurret.CloseCanvas();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentAmmountOfAmmunition == 0)
        {
            turretInfoScript.DisplayWarning();
            enoughAmmunition = false;
        } else
        {
            turretInfoScript.DisplayInfoAmmo(currentAmmountOfAmmunition.ToString(), maximumAmmountOfAmmunition.ToString());
        }

        if (reloadTurret.issOpen()) { 
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (money.CurrentMoney >= 30 && currentAmmountOfAmmunition != maximumAmmountOfAmmunition)
                {
                    currentAmmountOfAmmunition = maximumAmmountOfAmmunition;
                    enoughAmmunition = true;
                    money.RemoveMoney(30);
                    
                }
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (money.CurrentMoney >= 30 &&  fireRate != basicFireRate*2f)
                {
                    fireRate *= 2f;
                    money.RemoveMoney(30);

                }
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (money.CurrentMoney >= 30 && maximumAmmountOfAmmunition != 2*basicMagazine)
                {
                    maximumAmmountOfAmmunition *= 2;
                    money.RemoveMoney(30);

                }
            }
        }
        
        if(_target == null){
            return;
        }
        
        //Target lock on
        Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(Time.time >= fireCountdown && enoughAmmunition == true){
            Shoot();
            currentAmmountOfAmmunition -= 1;
            fireCountdown = 1f / fireRate + Time.time;
        }

        
    }

    void Shoot()
    {
            GameObject bulletGO = Instantiate(BulletObjects[_numberOfBullets++], firePoint.position, firePoint.rotation);
            TBullet bullet = bulletGO.GetComponent<TBullet>();

            if(bullet !=null) {
                bullet.Seek(_target);
            }
            if (_numberOfBullets == BulletObjects.Length) _numberOfBullets = 0;
    }	

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
