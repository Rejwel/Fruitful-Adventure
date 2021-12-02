using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBullet : MonoBehaviour
{
    private Transform _target;
    private WaveManagerSubscriber WaveManager;

    //public GameObject impactEfect;    //odblokuje się kiedy będziemy mieli efekt trafienia pocisku ^^
    [SerializeField] private float speed = 50f;
    private void Awake()
    {
        WaveManager = FindObjectOfType<WaveManagerSubscriber>();
    }

    void Update()
    {
        if(_target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
    }

    /*void HitTarget()  
    {
        GameObject effectIns = Instantiate(impactEfect, transform.position,transform.rotation);
        Destroy(effectIns,1.5f);
        Destroy(gameObject);
    }*/
    
    public void Seek (Transform _target){
        this._target = _target;
    }
    
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag.Equals("Enemy"))
        {
            EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
            enemy.TakeDamage(20);
            //HitTarget();
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
