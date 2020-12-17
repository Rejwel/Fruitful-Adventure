using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Diagnostics;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarScript healthBar;
    public PlayerMovement thePlayer;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        thePlayer = FindObjectOfType<PlayerMovement>();
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
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        thePlayer.Knockback(direction);
    }

    public void TakePlayerDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
