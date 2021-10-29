using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DamageBuilding : MonoBehaviour
{
    private BuildingHealth _buildingHealth;

    private void Start()
    {
        _buildingHealth = GetComponentInParent<BuildingHealth>();
    }

    //private void OnCollisionEnter(Collision other)
    //{
        // if (other.gameObject.CompareTag("Bullet"))
        // {
        //     if (_buildingHealth.currentHealth <= 0 && !_buildingHealth.buildingDestroyed)
        //     {
        //         _buildingHealth.DestroyBuilding();
        //     }
        //     else
        //     {
        //         _buildingHealth.TakeDamage(100);
        //     }
        //     Destroy(other.gameObject);
        // }
        
    //}
}
