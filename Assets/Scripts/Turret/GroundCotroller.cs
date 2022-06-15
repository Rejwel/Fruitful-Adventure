﻿using System;
using System.Linq;
using TMPro;
using UnityEngine;


public class GroundCotroller : MonoBehaviour
{

    [SerializeField] private GameObject[] placeableObjectPrefabs;
    [SerializeField] private LayerMask terrain;
    [SerializeField] private LayerMask placableObjects;
    [SerializeField] private LayerMask defendingStructures;
    [SerializeField] private GameObject currentPlaceableObject;
    [SerializeField] private GameObject player;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private float mouseWheelRotation;
    [SerializeField] private bool Menu;
    [SerializeField] private Inventory inv;
    
    [SerializeField] private bool isShop;
    [SerializeField] private int placedTurrets;
    [SerializeField] private TextMeshProUGUI placedTurretsText;
    //Ring Menu Controller
    [SerializeField] private RingMenu MainMenuInstance;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Prefab;
    private PauseMenu _pauseMenu;
    public ControllerMode Mode {  get; set; }

    private void Awake()
    {
        _pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void Start()
    {
        SetMode(ControllerMode.Play);
        inv = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isShop)
        {     
            SetMode(ControllerMode.Menu);
            ClearCurrentObject();
        }
        if (Mode == ControllerMode.Build)
        {
            if (currentPlaceableObject == null) HandleNewObjectHotkey();
            else
            {
                MoveCurrentObjectToMouse();
                RotateFromMouseWheel();
                ReleaseIfClicked();
            }
            
            // if clicked right button then exit
            if(Input.GetMouseButtonDown(1))
            {
                SetMode(ControllerMode.Play);
                ClearCurrentObject();
            }
        }
    }
    
    private void HandleNewObjectHotkey()         
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        player.GetComponent<PlayerShoot>().HoldFire = true;
        if (Physics.Raycast(ray, out hitInfo, 15f, terrain) && Prefab != null)
        {
            currentPlaceableObject = Instantiate(Prefab, hitInfo.transform.position, hitInfo.transform.rotation);
        }
    }

    public void SetMenu(bool menu)
    {
        Menu = menu;
    }
    public void SetShop(bool shop)
    {
        isShop = shop;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (!Physics.Raycast(ray, out hitInfo, 14f, placableObjects) 
            && !Physics.Raycast(ray, out hitInfo, 14f, defendingStructures) 
            && Physics.Raycast(ray, out hitInfo, 14f, terrain) 
            && hitInfo.normal == Vector3.up)
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = player.transform.rotation;
        }
        else
        {
            Destroy(currentPlaceableObject);
        }
    }


    private void RotateFromMouseWheel()
    {
        currentPlaceableObject.transform.Rotate(Vector3.up, 0f);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 12f);
    }
    
    private void ClearCurrentObject()
    {
        Destroy(currentPlaceableObject);
        currentPlaceableObject = null;
    }

    private void PlaceCurrentObject(int prefabIndex, RaycastHit where)
    {
        Transform savedTransform = currentPlaceableObject.transform;
        ClearCurrentObject();
        SetPrefab(prefabIndex);
        currentPlaceableObject = Instantiate(Prefab);
        currentPlaceableObject.transform.position = where.point;
        currentPlaceableObject.transform.rotation = savedTransform.rotation;
        AddIfIsDefendingStructureObject();
        currentPlaceableObject = null;
        Destroy(currentPlaceableObject);
        Prefab = null;
        SetMode(ControllerMode.Play);
    }

    private void AddIfIsDefendingStructureObject()
    {
        if (currentPlaceableObject.layer.Equals(25))
        {
            inv.defendingStructures.Add(currentPlaceableObject);
        }
    }

    public void ReleaseIfClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Input.GetMouseButtonDown(0) && !Physics.Raycast(ray, out hitInfo, 14f, placableObjects) && Physics.Raycast(ray, out hitInfo, 14f, terrain))
        {
            if (Prefab != null && Prefab.name.Equals("TurretTransparent"))
            {
                PlaceCurrentObject(0, hitInfo);
                placedTurrets++;
                inv.RemoveShootingTurret();
            }
            else if (Prefab != null && Prefab.name.Equals("SlowingTurretTransparent"))
            {
                PlaceCurrentObject(1, hitInfo);
                placedTurrets++;
                inv.RemoveSlowingTurret();
            }
            else if (Prefab != null && Prefab.name.Equals("SlowTrapTransparent"))
            {
                PlaceCurrentObject(5, hitInfo);
                inv.RemoveSlowTrap();
            }
            else if (Prefab != null && Prefab.name.Equals("DamageTrapTransparent"))
            {
                PlaceCurrentObject(7, hitInfo);
                inv.RemoveDamageTrap();
            }
            else if (Prefab != null && Prefab.name.Equals("FenceTrapTransparent"))
            {
                PlaceCurrentObject(9, hitInfo);
                inv.RemoveTrapFence();
            }
        }
    }

    public void SetMode(ControllerMode mode)
    {
        Mode = mode;
        if (mode != ControllerMode.Menu && MainMenuInstance != null)
            Destroy(MainMenuInstance);

        switch (mode)
        {
            case ControllerMode.Build:
                _pauseMenu.SetIsInBuildMode(true);
                Canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLook.enabled = true;
                player.GetComponent<PlayerShoot>().HoldFire = true;
                player.GetComponent<PlayerShoot>().AddDelay();             
                break;
            case ControllerMode.Menu:
                _pauseMenu.SetIsInBuildMode(false);
                Canvas.SetActive(true);               
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseLook.enabled = false;
                player.GetComponent<PlayerShoot>().HoldFire = true;
                player.GetComponent<PlayerShoot>().AddDelay();
                break;
            case ControllerMode.Play:
                _pauseMenu.SetIsInBuildMode(false);
                Canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLook.enabled = true;
                player.GetComponent<PlayerShoot>().HoldFire = false;
                player.GetComponent<PlayerShoot>().AddDelay();
                break;            
        }
    }
    
    public enum ControllerMode
    {
        Play,
        Build,
        Menu
    }

    public void SetPrefab(int number)
    {
        switch (number)
        {
            // 0 in index of turret In Array
            case 0:
                Prefab = inv.GetShootingTurret() > 0 ? placeableObjectPrefabs[number] : null;
                if (!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 1 in index of turretSlowing InArray
            case 1:
                Prefab = inv.GetSlowingTurret() > 0 ? placeableObjectPrefabs[number] : null;
                if (!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 2 in index of turretTransparent In Array
            case 2:
                Prefab = inv.GetShootingTurret() > 0 ? placeableObjectPrefabs[number] : null;
                if (!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 3 in index of turretDetectingTransparent In Array
            case 3:
                Prefab = inv.GetSlowingTurret() > 0 ? placeableObjectPrefabs[number] : null;
                if (!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 4 in index of SlowTrapTransparent In Array
            case 4:
                Prefab = inv.GetSlowTrap() > 0 ? placeableObjectPrefabs[number] : null;
                if(!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 5 in index of SlowTrap In Array
            case 5:
                Prefab = inv.GetSlowTrap() > 0 ? placeableObjectPrefabs[number] : null;
                if (!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 6 in index of DamageTrapTransparent In Array
            case 6:
                Prefab = inv.GetDamageTrap() > 0 ? placeableObjectPrefabs[number] : null;
                if(!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 7 in index of DamageTrap In Array
            case 7:
                Prefab = inv.GetDamageTrap() > 0 ? placeableObjectPrefabs[number] : null;
                if (!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 8 in index of TrapFence In Array
            case 8:
                Prefab = inv.GetTrapFence() > 0 ? placeableObjectPrefabs[number] : null;
                if (!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            // 9 in index of TrapFence In Array
            case 9:
                Prefab = inv.GetTrapFence() > 0 ? placeableObjectPrefabs[number] : null;
                if (!Prefab)
                {
                    SetMode(ControllerMode.Play);
                }
                break;
            default:
                Prefab = null;
                SetMode(ControllerMode.Play);
                Debug.Log("Error with setting numbers in prefab array");
                break;
        }
    }

    public int GetPlacedTurrets()
    {
        placedTurretsText.text = "Placed turrets: " + placedTurrets.ToString();
        return placedTurrets;
    }
}