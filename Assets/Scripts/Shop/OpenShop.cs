using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenShop : MonoBehaviour
{
    public GameObject inGameGui;
    public GameObject shopGui;
    public GameObject player;
    public GameObject text;
    public Text currentMoney;
    private Money money;
    public HealthBarScript healthBarPlayer;   
    public HealthBarScript healthBarMenu;   
    private HealthPlayer playerHealth;

    private void Start()
    {
        money = FindObjectOfType<Money>();
        playerHealth = FindObjectOfType<HealthPlayer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            openShop();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            closeShop();
        }
    }

    void openShop()
    {
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        text.SetActive(false);
        player.GetComponent<PlayerShoot>().enabled = false;
        inGameGui.SetActive(false);
        shopGui.SetActive(true);
        
        //healthbar for shop
        healthBarMenu.SetMaxHealth(playerHealth.maxHealth);
        healthBarMenu.getHealthBar();
    }

    void closeShop()
    {
        healthBarPlayer.getHealthBar();

        currentMoney.text = money.CurrentMoney.ToString();
        text.SetActive(true);
        player.GetComponent<PlayerShoot>().enabled = true;
        inGameGui.SetActive(true);
        shopGui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        
        Time.timeScale = 1f;
    }
    
}
