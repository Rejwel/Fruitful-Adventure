using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.iOS;

public class BulletMechanics : MonoBehaviour
{
    private WaveManagerSubscriber WaveManager;
    private PlayerShoot player;
    private Gun gun;

    void Start()
    {
        Physics.IgnoreLayerCollision(15,15);
        Physics.IgnoreLayerCollision(15, 20);
        //explosion = FindObjectOfType<Explosion>();
        player = FindObjectOfType<PlayerShoot>();
        WaveManager = FindObjectOfType<WaveManagerSubscriber>();
        gameObject.tag = "Bullet";
        Destroy(gameObject, 5);
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            if(hit.GetComponent<EnemyMelee>() != null) hit.GetComponent<EnemyMelee>().StartAttackingPlayer();
            EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
            gun = player.GetCurrentGun();
            enemy.TakeDamage(gun.GetDamage());
            
            if (enemy.GetHealth() <= 0 && enemy != null)
            {
                hit.GetComponent<Collider>().enabled = false;
                StartCoroutine(ExplodeEnemy(hit));
                WaveManager.UpdateEnemyCounter();
                enemy.Die();
            }
            Destroy(gameObject);
        } 
       
    }

    IEnumerator ExplodeEnemy(Collider hit)
    {
        Explosion explosion = hit.GetComponent<Explosion>();
        EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
        Transform EnemyTransform = enemy.transform;
        hit.GetComponent<Collider>().enabled = false;
        if(explosion != null)
            explosion.explode(EnemyTransform);
        yield return null;
    }
}
