using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    Money Money;
    private WaveManager WaveManager;
    private PlayerShoot player;
    private Gun gun;

    void Start()
    {
        player = FindObjectOfType<PlayerShoot>();
        WaveManager = FindObjectOfType<WaveManager>();
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
            gun = player.GetCurrentGun();
            enemyHealth.TakeDamage(gun.GetDamage());
            
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
