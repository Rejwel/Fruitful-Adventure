using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public class EnemyFollowing : MonoBehaviour
{
    public NavMeshAgent enemy;

    private GameObject WhatToAttack { get; set; }
    
    private void Start()
    {
        WhatToAttack = WaveManager.AttackingBuilding;
        StartCoroutine(HoldNavAgent());
    }

    void Update()
    {
        enemy.SetDestination(WhatToAttack.transform.localPosition);
    }
    
    public IEnumerator HoldNavAgent() 
    { 
        yield return new WaitForSeconds(0.5f);
        enemy.enabled = true;
    }
}
