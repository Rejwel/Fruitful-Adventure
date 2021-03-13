using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public class EnemyFollowing : MonoBehaviour
{
    public NavMeshAgent enemy;
    private Rigidbody EnemyRB { get; set; }
    private GameObject WhatToAttack { get; set; }
    private bool IsAttacking { get; set; }

    private void Start()
    {
        EnemyRB = GetComponent<Rigidbody>();
        WhatToAttack = WaveManager.AttackingBuilding;
        StartCoroutine(HoldNavAgent());
    }

    void Update()
    {
        if(!IsAttacking)
            enemy.SetDestination(WhatToAttack.transform.localPosition);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 19)
        {
            IsAttacking = true;
            enemy.Stop();
            EnemyRB.constraints = RigidbodyConstraints.FreezeAll;
            
            BuildingHealth BH = other.gameObject.GetComponentInParent<BuildingHealth>();
            BH.TakeDamage(20);
            if (BH.currentHealth <= 0)
            {
                BH.DestroyBuilding();
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 19)
        {
            IsAttacking = false;
        }
    }

    public IEnumerator HoldNavAgent() 
    { 
        yield return new WaitForSeconds(0.5f);
        enemy.enabled = true;
    }
}
