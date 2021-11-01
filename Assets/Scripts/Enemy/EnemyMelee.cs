using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityEngine.iOS;

public class EnemyMelee : MonoBehaviour
{
    public NavMeshAgent enemy;
    private GameObject Player;
    private BuildingHealth BH { get; set; }
    private Rigidbody EnemyRB { get; set; }
    private GameObject WhatToAttack { get; set; }
    private bool IsAttacking { get; set; }
    private bool Attack = true;
    private float NextAttack = 0f;

    public LayerMask whatIsPlayer;
    public LayerMask buildingLayermask;
    private bool InRange;
    private bool InBuildingAttackingRange;
    private bool triggeredByPlayer = false;

    private void Start()
    {
        Player = FindObjectOfType<HealthPlayer>().gameObject;
        EnemyRB = GetComponent<Rigidbody>();
        WhatToAttack = WaveManager.AttackingBuilding;
    }
    
    void Update()
    {
        WhatToAttack = WaveManager.AttackingBuilding;
        InRange = Physics.CheckSphere(transform.position, 20, whatIsPlayer);
        InBuildingAttackingRange = Physics.CheckSphere(transform.position, 6, buildingLayermask);

        // attack building
        if (InBuildingAttackingRange && !triggeredByPlayer && WhatToAttack != null)
            AttackBuilding();
        else
            StopAttackingBuilding();
        
        
        if (InRange && !IsAttacking || WhatToAttack == null)
        {
            transform.LookAt(Player.transform);
            enemy.SetDestination(Player.transform.position);
        }
        else
        {
            if (NextAttack >= EnemyMechanics.AttackSpeed)
            {
                Attack = true;
                NextAttack = 0f;
            }
        
            if (!IsAttacking && !InRange)
            {
                triggeredByPlayer = false;
                enemy.SetDestination(WhatToAttack.transform.localPosition);
                transform.LookAt(WhatToAttack.transform);
            }
            
            if(Attack && BH != null)
            {
                BH.TakeDamage(20);
                if (BH.currentHealth <= 0 && BH.buildingDestroyed == false)
                {
                    // transform.LookAt(Player.transform);
                    // enemy.SetDestination(Player.transform.position);
                    BH.DestroyBuilding();
                }
                Attack = false;
            }
        }
        
        //add attack delay
        NextAttack += Time.deltaTime;
    }
    
    private void AttackBuilding()
    {
        if(WhatToAttack.GetComponent<BuildingReference>().GetBuilding().GetComponent<BuildingHealth>() != null)
            BH = WhatToAttack.GetComponent<BuildingReference>().GetBuilding().GetComponent<BuildingHealth>();
        IsAttacking = true;
        enemy.isStopped = true;
        EnemyRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void StopAttackingBuilding()
    {
        BH = null;
        IsAttacking = false;
        enemy.isStopped = false;
        EnemyRB.constraints = RigidbodyConstraints.None;
    }
    public void StartAttackingPlayer()
    {
        if (IsAttacking && InRange)
        {
            triggeredByPlayer = true;
            IsAttacking = false;
            enemy.isStopped = false;
            EnemyRB.constraints = RigidbodyConstraints.None;
            transform.LookAt(Player.transform);
            enemy.SetDestination(Player.transform.position);
        }
    }
}
