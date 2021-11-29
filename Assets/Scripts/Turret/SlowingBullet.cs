using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class SlowingBullet : MonoBehaviour
{
    private Transform target;

    private WaveManagerSubscriber WaveManager;

    public GameObject impactSlowingEfect;
    [SerializeField] private float speed = 40f;
    public void Seek (Transform _target){
        target = _target;
    }

    private void Awake()
    {
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
        GameObject effectIns = Instantiate(impactSlowingEfect, transform.position,transform.rotation);
        Destroy(effectIns,1.5f);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Enemy") && hit.GetComponent<NavMeshAgent>() != null)
        {
            //work in progress
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
