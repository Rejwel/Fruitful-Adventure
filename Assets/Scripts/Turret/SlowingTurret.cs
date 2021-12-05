using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTurret : MonoBehaviour
{
    private Transform _target;
    private bool _enoughAmmunition = true;
    private float _fireCountdown;
    private int _currentAmmountOfAmmunition;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform partToRotate;
    [SerializeField] private Transform firePoint;
    
    private TurretInfo _reloadTurret;
    private TurretInfoScript _turretInfoScript;
    
    [SerializeField] private int maximumAmmountOfAmmunition;
    [SerializeField] private float range = 15f;
    [SerializeField] private float turnSpeed = 1f;

    private void Awake()
    {
        _reloadTurret = FindObjectOfType<TurretInfo>();
        _turretInfoScript = FindObjectOfType<TurretInfoScript>();
    }
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        _currentAmmountOfAmmunition = maximumAmmountOfAmmunition;
    }
    
    void UpdateTarget() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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

    private void Update()
    {
        if (_currentAmmountOfAmmunition == 0)
        {
            _turretInfoScript.DisplayWarning();
            _enoughAmmunition = false;
        } 
        else
        {
            _turretInfoScript.DisplayInfoAmmo(_currentAmmountOfAmmunition.ToString(), maximumAmmountOfAmmunition.ToString());
            _enoughAmmunition = true;
        }

        if (_target != null)
        {
            Vector3 dir = _target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (Time.time >= _fireCountdown && _enoughAmmunition)
            {
                Shoot();
                _currentAmmountOfAmmunition -= 1;
                _fireCountdown = 6f / 1f + Time.time;
            }
        }
    }
    
    void Shoot()
    {
        GameObject slowingBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SlowingBullet bullet = slowingBullet.GetComponent<SlowingBullet>();

        if (bullet !=null) 
        {
            bullet.Seeking(_target);
        }
 
    }	

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
