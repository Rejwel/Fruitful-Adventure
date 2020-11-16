using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider hit)
    {
        EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
        HealthEnemy enemyHealth = hit.GetComponent<HealthEnemy>();
        
        if (hit.tag.Equals("Enemy"))
        {
            enemyHealth.TakeDamage(20);
            if(enemyHealth.currentHealth <= 0) enemy.Die();
            Destroy(gameObject);
        }
    }
}
