using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.TerrainAPI;

public class SlowTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyMechanics>();
        if (enemy.CompareTag("Enemy") && enemy.GetSpeed() > 0)    
        {
            enemy.SetSpeed(2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<EnemyMechanics>();
        if (enemy.CompareTag("Enemy"))
        {
            enemy.SetSpeed(-2);
        }
    }
    
}

