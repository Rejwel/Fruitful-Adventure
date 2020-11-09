using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(Player.position);
    }
}
