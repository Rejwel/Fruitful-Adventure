using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    private Rigidbody enemyRb;
    
    private void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Bullet"))
        {
            Invoke("StopMoving", 0.2f);
        }
    }

    void Update()
    {
        enemy.SetDestination(Player.position);
    }
    
    public void StopMoving()
    {
        enemyRb.velocity = Vector3.zero;
        enemy.SetDestination(Player.position);
    }
}
