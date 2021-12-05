using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class OpenShop : MonoBehaviour
{
    private bool inShop = false;
    public GameObject inGameGui;
    public GameObject shopGui;
    public GameObject text;
    public TextMeshProUGUI currentMoney;
    private Money money;
    public HealthBarScript healthBarPlayer;   
    public HealthBarScript healthBarMenu;   
    private HealthPlayer playerHealth;
    
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private GameObject player;
    private Transform savedPlayerTransform;
    
    
    public GameObject FocusPoint;
    public GameObject StandingPoint;

    private float timeOnFocus = 0f;
    private float Delay = 1.2f;

    private GroundCotroller mode;

    private void Awake()
    {
        mouseLook = cameraHolder.GetComponent<MouseLook>();
        playerController = FindObjectOfType<CharacterController>();
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
            playerController.enabled = true;
            mouseLook.CancelLookingAtObject();

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
        playerController.GetComponent<CharacterController>().enabled = false;
        mouseLook.LookAtObject(FocusPoint);
        player.transform.position = Vector3.Lerp(player.transform.position, StandingPoint.transform.position, 3f * Time.deltaTime);
    }

    void openShop()
    {
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
