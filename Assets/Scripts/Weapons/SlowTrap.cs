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
        if (other.CompareTag("Enemy")  && other.GetComponent<NavMeshAgent>() != null && other.GetComponent<NavMeshAgent>().speed > 0)
        {
            other.GetComponent<NavMeshAgent>().speed -= 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<NavMeshAgent>() != null)
        {
            other.GetComponent<NavMeshAgent>().speed += 2;
        }
    }
    
}
