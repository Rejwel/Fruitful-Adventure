using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMechanics : MonoBehaviour
{
    public static readonly float AttackSpeed = 3f;
    public static readonly float PlayerRange = 30f;
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBarScript healthBar;
    private Rigidbody enemyRb;
    private Explosion _explosion;
    public GameObject Money;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        enemyRb = gameObject.GetComponent<Rigidbody>();
        gameObject.AddComponent<Explosion>();
       
        if (gameObject.layer.Equals(7))
        {
            gameObject.GetComponent<Explosion>().cubesInRow = 4;
            gameObject.GetComponent<Explosion>().explosionForce = 60f;
            gameObject.GetComponent<Explosion>().explosionRadius = 20f;
        }
        else if (gameObject.layer.Equals(10))
        {
            gameObject.GetComponent<Explosion>().cubesInRow = 2;
            gameObject.GetComponent<Explosion>().explosionForce = 50f;
            gameObject.GetComponent<Explosion>().explosionRadius = 5f;
        }
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
    public float GetSpeed()
    {
        return gameObject.GetComponent<NavMeshAgent>().speed;
    }

    public void ReduceSpeed(float speed)
    {
        gameObject.GetComponent<NavMeshAgent>().speed -= speed;
    }
}
