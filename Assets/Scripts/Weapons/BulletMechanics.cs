using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    Money Money;

    void Start()
    {
        Money = FindObjectOfType<Money>(); 
        Destroy(gameObject, 5);
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
            if (enemyHealth.currentHealth <= 0)
            {
                Money.AddMoney();
                enemy.Die();
            }
            Destroy(gameObject);
        }
    }
}
