using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour
{
    public static readonly float AttackSpeed = 3f;
    public static readonly float PlayerRange = 30f;
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBarScript healthBar;
    private Rigidbody enemyRb;
    public GameObject Money;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        enemyRb = gameObject.GetComponent<Rigidbody>();
        gameObject.AddComponent<Explosion>();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            enemyRb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            enemyRb.constraints = RigidbodyConstraints.None;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
