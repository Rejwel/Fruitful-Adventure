using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    

    [Header("Attributes")]
    public float fireRate = 1f;

    public TurretInfo reloadTurret;

    public float maximumAmmountOfAmmunition;
    private float currentAmmountOfAmmunition;
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


    private void Awake()
    {
        money = FindObjectOfType<Money>();
        reloadTurret = FindObjectOfType<TurretInfo>();
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
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerMovement"))
        {
            reloadTurret.OpenCanvas();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerMovement"))
            reloadTurret.CloseCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadTurret.issOpen()) {
            Debug.Log("weszlo jak w maslo");
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("K mi dziala");
                if (money.CurrentMoney >= 30)
                {
                    currentAmmountOfAmmunition = maximumAmmountOfAmmunition;
                    enoughAmmunition = true;
                    money.RemoveMoney(30);
                    Debug.Log("Kupiono amunicje");
                }
                else
                {
                    Debug.Log("Zbyt malo $$$");
                }
            }
        }
        
        if(target == null){
            return;
        }
        
        //Target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(Time.time >= fireCountdown && enoughAmmunition == true){
            Shoot();
            currentAmmountOfAmmunition -= 1;
            fireCountdown = 1f / fireRate + Time.time;
        }

        if(currentAmmountOfAmmunition == 0)
        {
            enoughAmmunition = false;
            Debug.Log("Brak amunicji");
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
		TBullet bullet = bulletGO.GetComponent<TBullet>();

		if(bullet !=null){
			bullet.Seek(target);
		}
	}	

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
