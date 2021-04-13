using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Experimental.TerrainAPI;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Canvas HealthIndicator;
    public Color FlashColor;

    public HealthBarScript healthBar;
    public PlayerMovement thePlayer;
    private Inventory inv;
    private bool Damaged = false;
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
        FlashColor = new Color(255,255,255);
    }

    private void Update()
    {
        if (Damaged)
        {
            SetDamageScreen();
        }
        else
        {
            HealthIndicator.GetComponent<Image>().color = Color.Lerp(HealthIndicator.GetComponent<Image>().color,
                Color.clear, 3f * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision CollisionInfo)
    {
        if (CollisionInfo.collider.CompareTag("Enemy"))
        {
            AudioManager.playSound("enemyMelee");
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
            Damaged = true;
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            thePlayer.Knockback(direction);
        }
    }
    void SetDamageScreen()
    {
        Color tempColor = FlashColor;
        tempColor.a = 255;
        HealthIndicator.GetComponent<Image>().color = tempColor;
        Damaged = false;
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
            Damaged = true;
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth); 
        }
    }

}
