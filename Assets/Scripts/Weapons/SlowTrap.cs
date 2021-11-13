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
        if (other.tag == "Enemy")
        {
            var defaultSpeed = other.GetComponent<NavMeshAgent>().speed;
            other.GetComponent<NavMeshAgent>().speed = defaultSpeed - 2;
            //Debug.Log(other.GetComponent<NavMeshAgent>().speed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Mage(Clone)" || other.name == "Melee(Clone)")
        {
            other.GetComponent<NavMeshAgent>().speed = 4;
        }
        if (other.name == "Tank(Clone)")
        {
            other.GetComponent<NavMeshAgent>().speed = 3;
        }
        if (other.name == "Range(Clone)")
        {
            other.GetComponent<NavMeshAgent>().speed = 5;
        }
    }

   
}
