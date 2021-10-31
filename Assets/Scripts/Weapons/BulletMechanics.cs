using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletMechanics : MonoBehaviour
{
    private WaveManager WaveManager;
    private Explosion explosion;
    private PlayerShoot player;
    private Gun gun;

    void Start()
    {
        Physics.IgnoreLayerCollision(15,15);
        Physics.IgnoreLayerCollision(15, 20);
        explosion = FindObjectOfType<Explosion>();
        player = FindObjectOfType<PlayerShoot>();
        WaveManager = FindObjectOfType<WaveManager>();
        gameObject.tag = "Bullet";
        Destroy(gameObject, 5);
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag.Equals("Enemy"))
        {
            if(hit.GetComponent<EnemyMelee>() != null) hit.GetComponent<EnemyMelee>().StartAttackingPlayer();
            EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
            gun = player.GetCurrentGun();
            enemy.TakeDamage(gun.GetDamage());
            
            if (enemy.GetHealth() <= 0 && enemy != null)
            {
                hit.GetComponent<Collider>().enabled = false;
                Transform EnemyTransform = enemy.transform;
                explosion.explode(EnemyTransform);
                WaveManager.UpdateEnemyCounter();
                enemy.Die();
            }
            Destroy(gameObject);
        }
    }
    
}
