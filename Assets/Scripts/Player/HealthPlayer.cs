using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarScript healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
    void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.collider.CompareTag("Enemy"))
        {
            TakeDamage(30);
        }
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
