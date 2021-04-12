using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Experimental.TerrainAPI;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarScript healthBar;
    public PlayerMovement thePlayer;
    private Inventory inv;
    public GameObject shield;
    
    public LayerMask PlayerLayerMask;
    public CharacterController Player { get; set; }

    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        thePlayer = FindObjectOfType<PlayerMovement>();
        Player = GetComponentInParent<CharacterController>();
    }
    

    void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.collider.CompareTag("Enemy"))
        {
            Vector3 hitDirection = transform.position - CollisionInfo.transform.position;
            TakeDamage(30, hitDirection);
        }
    }


    void TakeDamage(int damage, Vector3 direction)
    {
        if (inv.isShielded())
        {
            inv.removeShield();
            shield.SetActive(false);
            thePlayer.Knockback(direction);
        }
        else
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            thePlayer.Knockback(direction);
        }
        
    }

    public void TakePlayerDamage(int damage)
    {
        if (inv.isShielded())
        {
            inv.removeShield();
            shield.SetActive(false);
        }
        else
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth); 
        }
    }

}
