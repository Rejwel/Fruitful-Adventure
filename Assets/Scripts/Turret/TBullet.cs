using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBullet : MonoBehaviour
{
    private Transform target;
    private Explosion explosion;
    
    private WaveManagerSubscriber WaveManager;

    public GameObject impactEfect;
    public float speed = 70f;
    public void Seek (Transform _target){
        target = _target;
    }

    private void Awake()
    {
        explosion = FindObjectOfType<Explosion>();
        WaveManager = FindObjectOfType<WaveManagerSubscriber>();
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
        if (hit.tag.Equals("Enemy"))
        {
            EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
            enemy.TakeDamage(20);
            HitTarget();
            if (enemy.GetHealth() <= 0  && enemy != null)
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
