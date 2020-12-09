using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBullet : MonoBehaviour
{
    private Transform target;

    Money Money;
    private WaveManager WaveManager;

    public GameObject impactEfect;
    public float speed = 70f;
    public void Seek (Transform _target){
        target = _target;
    }

    private void Start()
    {
        WaveManager = FindObjectOfType<WaveManager>();
        Money = FindObjectOfType<Money>(); 
    }

    void Update()
    {
        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // if(dir.magnitude <= distanceThisFrame){
        //     //HitTarget();
        //     return;
        // }
        transform.Translate (dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEfect, transform.position,transform.rotation);
        Destroy(effectIns,1.5f);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider hit)
    {
        EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
        HealthEnemy enemyHealth = hit.GetComponent<HealthEnemy>();
        if (hit.tag.Equals("Enemy"))
        {
            // print("bullet: " + gameObject.transform.position);
            // print("enemy: " + hit.transform.position);
            
            enemyHealth.TakeDamage(20);
            HitTarget();
            if (enemyHealth.currentHealth <= 0)
            {
                enemy.Die();
                Money.AddMoney();
                WaveManager.killEnemy();
            }
            Destroy(gameObject);
        }
    }
}
