using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBullet : MonoBehaviour
{
    private Transform target;
    private Explosion explosion;
    
    private WaveManager WaveManager;

    public GameObject impactEfect;
    public float speed = 70f;
    public void Seek (Transform _target){
        target = _target;
    }

    private void Start()
    {
        explosion = FindObjectOfType<Explosion>();
        WaveManager = FindObjectOfType<WaveManager>();
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
        if (hit.tag.Equals("Enemy"))
        {
            // print("bullet: " + gameObject.transform.position);
            // print("enemy: " + hit.transform.position);
            
            enemy.TakeDamage(20);
            HitTarget();
            if (enemy.GetHealth() <= 0)
            {
                enemy.Die();
                explosion.explode(hit.gameObject);
                WaveManager.killEnemy();
            }
            Destroy(gameObject);
        }
    }
}
