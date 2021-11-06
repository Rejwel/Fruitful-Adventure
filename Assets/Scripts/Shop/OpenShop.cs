using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenShop : MonoBehaviour
{
    private bool inShop = false;
    public GameObject inGameGui;
    public GameObject shopGui;
    public GameObject player;
    public GameObject text;
    public TextMeshProUGUI currentMoney;
    private Money money;
    public HealthBarScript healthBarPlayer;   
    public HealthBarScript healthBarMenu;   
    private HealthPlayer playerHealth;
    
    public Transform PlayerTransform;
    private Transform TempPlayerTransform;
    
    public GameObject FocusPoint;
    public GameObject StandingPoint;

    private float timeOnFocus = 0f;
    private float Delay = 1.2f;

    private GroundCotroller mode;

    private void Start()
    {
        TempPlayerTransform = PlayerTransform.transform;
        money = FindObjectOfType<Money>();
        playerHealth = FindObjectOfType<HealthPlayer>();
        mode = FindObjectOfType<GroundCotroller>();
    }

    private void Update()
    {
        if(inShop)
        {
            if(Time.time <=  timeOnFocus)
                FocusCamera();
        }

        if (Input.GetKey(KeyCode.E) && mode.Mode == GroundCotroller.ControllerMode.Play)
        {
            timeOnFocus = Time.time + Delay;
            inShop = true;
            player.GetComponent<PlayerShoot>().HoldFire = true;
            openShop();
            mode.SetShop(true);
        }

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Escape))
        {
            PlayerTransform.GetComponentInParent<CharacterController>().enabled = true;
            inShop = false;
            closeShop();
            player.GetComponent<PlayerShoot>().HoldFire = false;
            mode.SetShop(false);
        }

        if((mode.Mode == GroundCotroller.ControllerMode.Menu || mode.Mode == GroundCotroller.ControllerMode.Build) && !inShop)
        {
            text.SetActive(false);
        }
        else if(mode.Mode == GroundCotroller.ControllerMode.Play && !inShop)
        {
            text.SetActive(true);
        }
    }

    void FocusCamera()
    {
        PlayerTransform.GetComponentInParent<CharacterController>().enabled = false;
        PlayerTransform.transform.rotation = Quaternion.Lerp(PlayerTransform.transform.rotation, FocusPoint.transform.rotation, 3f * Time.deltaTime);
        PlayerTransform.position = Vector3.Lerp(PlayerTransform.position, StandingPoint.transform.position, 3f * Time.deltaTime);
    }

    void openShop()
    {
        // Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        text.SetActive(false);
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
        inGameGui.SetActive(true);
        shopGui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Time.timeScale = 1f;
    }
    
}
