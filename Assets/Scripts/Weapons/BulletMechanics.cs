using System;
using System.Collections;
using System.Collections.Generic;
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
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider hit)
    {
        EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
        if (hit.tag.Equals("Enemy"))
        {
            // print("bullet: " + gameObject.transform.position);
            // print("enemy: " + hit.transform.position);
            gun = player.GetCurrentGun();
            enemy.TakeDamage(gun.GetDamage());
            
            if (enemy.GetHealth() <= 0)
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
