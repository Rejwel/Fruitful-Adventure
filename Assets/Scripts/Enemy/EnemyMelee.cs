using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityEngine.Experimental.TerrainAPI;

public class EnemyMelee : MonoBehaviour
{
    public NavMeshAgent enemy;
    private GameObject Player;
    private BuildingHealth BH { get; set; }
    private DefendingDestroyable DD;
    private Rigidbody EnemyRB { get; set; }
    private GameObject WhatToAttack { get; set; }
    private bool IsAttacking { get; set; }
    private bool Attack = true;
    private float NextAttack = 0f;
    [SerializeField] private int enemyDamage;
    
    
    public LayerMask whatIsPlayer;
    public LayerMask buildingLayermask;
    private bool InRangeOfPlayer;
    private bool InBuildingAttackingRange;
    private bool triggeredByPlayer = false;
    private NavMeshPath _thisEnemyNavMeshPath;
    private bool _pathToPlayerIsPossible;
    
    [SerializeField] private bool inRangeOfDefendingStructure;
    [SerializeField] private bool inAttackRangeOfDefendingStructure;
    [SerializeField] private LayerMask defendingStructureLayer;
    [SerializeField] private GameObject defendingStructure;
    
    
    

    private void Awake()
    {
        enemyDamage = 20;
        _thisEnemyNavMeshPath = new NavMeshPath();
        Player = FindObjectOfType<HealthPlayer>().gameObject;
        EnemyRB = GetComponent<Rigidbody>();
        WhatToAttack = WaveManagerSubscriber.AttackingBuilding;
    }

    void Update()
    {
        SphereCheckers();
        StartStopAttacking();
            
    
        if (_pathToPlayerIsPossible && InRangeOfPlayer && !IsAttacking || WhatToAttack == null)
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
    
            if (!IsAttacking && !InRangeOfPlayer || InRangeOfPlayer && !_pathToPlayerIsPossible)
            {
                triggeredByPlayer = false;
                if (inRangeOfDefendingStructure)
                {
                    defendingStructure = FindClosestDefendingStructure();
                    if (defendingStructure != null)
                    {
                        transform.LookAt(defendingStructure.transform);
                        enemy.SetDestination(defendingStructure.transform.position);
                    }
                }
                else
                {
                    enemy.SetDestination(WhatToAttack.transform.localPosition);
                    transform.LookAt(WhatToAttack.transform);
                }
            }
            
            if(Attack && DD != null)
            {
                DD.TakeDamage(enemyDamage);
                if (DD.GetHealth() <= 0)
                {
                    DD.DestroyStructure();
                }
                Attack = false;
            }
            else if(Attack && BH != null)
            {
                BH.TakeDamage(enemyDamage);
                if (BH.currentHealth <= 0 && BH.buildingDestroyed == false)
                {
                    BH.DestroyBuilding();
                }
                Attack = false;
            }
        }

        NextAttack += Time.deltaTime;
    }

    private void SphereCheckers()
    {
        _pathToPlayerIsPossible = enemy.CalculatePath(Player.transform.position, _thisEnemyNavMeshPath);
        WhatToAttack = WaveManagerSubscriber.AttackingBuilding;
        InRangeOfPlayer = Physics.CheckSphere(transform.position, 20, whatIsPlayer);
        InBuildingAttackingRange = Physics.CheckSphere(transform.position, 6, buildingLayermask);
        inRangeOfDefendingStructure = Physics.CheckSphere(transform.position, 20, defendingStructureLayer);
        inAttackRangeOfDefendingStructure = Physics.CheckSphere(transform.position, 2, defendingStructureLayer);
    }

    private void StartStopAttacking()
    {
        if (inAttackRangeOfDefendingStructure && !triggeredByPlayer)
            AttackStructure();
        else if (InBuildingAttackingRange && !triggeredByPlayer && WhatToAttack != null)
            AttackBuilding();
        else
        {
            StopAttackingBuilding();
            StopAttackingStructure();
        }
    }
    
    private void AttackBuilding()
    {
        if(WhatToAttack.GetComponent<BuildingHealth>() != null)
            BH = WhatToAttack.GetComponent<BuildingHealth>();
        IsAttacking = true;
        enemy.isStopped = true;
        EnemyRB.constraints = RigidbodyConstraints.FreezeAll;
    }
    
    private void AttackStructure()
    {
        if(defendingStructure != null && defendingStructure.GetComponent<DefendingDestroyable>() != null)
            DD = defendingStructure.GetComponent<DefendingDestroyable>();
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
    
    private void StopAttackingStructure()
    {
        DD = null;
        IsAttacking = false;
        enemy.isStopped = false;
        EnemyRB.constraints = RigidbodyConstraints.None;
    }
    public void StartAttackingPlayer()
    {
        if (IsAttacking && InRangeOfPlayer)
        {
            triggeredByPlayer = true;
            IsAttacking = false;
            enemy.isStopped = false;
            EnemyRB.constraints = RigidbodyConstraints.None;
            transform.LookAt(Player.transform);
            enemy.SetDestination(Player.transform.position);
        }
    }

    private GameObject FindClosestDefendingStructure()
    {
        List<GameObject> defStructures = Player.GetComponent<Inventory>().getDefendingStructures();
        float distance = float.MaxValue;
        GameObject attackingStructure = null;
        if (defStructures.Count > 0)
        {
            foreach (var structure in defStructures)
            {
                if ((structure.transform.position - enemy.transform.position).magnitude < distance)
                {
                    distance = (structure.transform.position - enemy.transform.position).magnitude;
                    attackingStructure = structure;
                }
            }
        }
        return attackingStructure;
    }
}
