using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

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
    private bool InRange;

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

        // WhatToAttack.GetComponent<BuildingReference>().GetBuilding().GetComponent<BuildingHealth>().buildingDestroyed
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
        NextAttack += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (WaveManager.AttackingBuilding != null && other.gameObject.layer == 19 && other.transform.parent.gameObject.Equals(WaveManager.AttackingBuilding.GetComponent<BuildingReference>().Building))
        {
            IsAttacking = true;
            enemy.isStopped = true;
            EnemyRB.constraints = RigidbodyConstraints.FreezeAll;
            BH = other.gameObject.GetComponentInParent<BuildingHealth>();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 19)
        {
            BH = null;
            IsAttacking = false;
            enemy.isStopped = false;
            EnemyRB.constraints = RigidbodyConstraints.None;
        }
    }
    
}
