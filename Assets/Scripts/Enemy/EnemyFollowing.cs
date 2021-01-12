using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public class EnemyFollowing : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    private Rigidbody enemyRb;
    private bool follow = false;
   
    

    private void Start()
    {
        StartCoroutine(HoldNavAgent());
        
        enemyRb = gameObject.GetComponent<Rigidbody>();
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").transform;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Bullet") || other.collider.CompareTag("Enemy"))
        {
            Invoke("StopMoving", 0.2f);
        }
    }

    void Update()
    {
        if(follow)
            enemy.SetDestination(Player.position);
      
    }
    
    public void StopMoving()
    {
        enemyRb.velocity = Vector3.zero;
        enemy.SetDestination(Player.position);
    }
    
    public IEnumerator HoldNavAgent() 
    { 
        yield return new WaitForSeconds(0.5f);
        enemy.enabled = true;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        follow = true;
    }
}
