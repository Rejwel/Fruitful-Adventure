﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    private WaveManager WaveManager;
    private Explosion explosion;
    private PlayerShoot player;
    private Gun gun;

    void Start()
    {
        Physics.IgnoreLayerCollision(15,15);
        explosion = FindObjectOfType<Explosion>();
        player = FindObjectOfType<PlayerShoot>();
        WaveManager = FindObjectOfType<WaveManager>();
        gameObject.tag = "Bullet";
        Destroy(gameObject, 5);
        gameObject.GetComponent<SphereCollider>().enabled = true;
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
                hit.GetComponent<Collider>().enabled = false;
                enemy.Die();
                explosion.explode(hit.gameObject);
                WaveManager.killEnemy();
            }
            Destroy(gameObject);
        }
    }
    
    
}
