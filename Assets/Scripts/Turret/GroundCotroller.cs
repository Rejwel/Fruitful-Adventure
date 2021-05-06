using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundCotroller : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;
    private uint[] placeableObjectPrefabsCount;
    

    public LayerMask terrain;
    private bool canRotate = false;

    private int currentPrefabIndex = -1;

    public GameObject currentPlaceableObject;
    public GameObject player;
    private float mouseWheelRotation;
    public bool hope = true;
    private bool Menu = false;
    private bool Empty = false;
    public GameObject WarningCanvas;
    public Transform location;
    public GameObject Turret;
    private Inventory inv;
    

    //Ring Menu Controller
    protected RingMenu MainMenuInstance;
    public RingMenu MainMenuPrefab;
    public GameObject Canvas;
    [HideInInspector]
    public ControllerMode Mode;

    private void Start()
    {
        SetMode(ControllerMode.Play);
        inv = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        HandleNewObjectHotkey();
        
        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            if (hope)
            {
                WarningCanvas.SetActive(false);
                ReleaseIfClicked();
            }
            else
            {
                WarningCanvas.SetActive(true);
            }
        }


        //Cases of clicking Q

            if (Input.GetKeyDown(KeyCode.Q))
            {        
                SetMode(ControllerMode.Menu);     
            }

            if (Input.GetKeyUp(KeyCode.Q) && Menu)
            {
                if (!Empty)
                    SetMode(ControllerMode.Build);
                else if (Empty)
                    SetMode(ControllerMode.Play);
            }

            if (Input.GetKeyUp(KeyCode.Q) && !Menu)
            {            
                SetMode(ControllerMode.Play);
            }


            if(Mode == ControllerMode.Build)
            {
                if(Input.GetMouseButtonDown(1))
                {
                    SetMode(ControllerMode.Play);
                    Destroy(currentPlaceableObject);
                }
            }

    }
    

    private void HandleNewObjectHotkey()        //Magic happens here 
    {
        WarningCanvas.SetActive(false);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Mode == ControllerMode.Build)
        { 
            if (inv.GameObjDictionary["Turret"] > 0)
            {
                player.GetComponent<PlayerShoot>().HoldFire = true;
                hope = true;
                WarningCanvas.SetActive(false);
                /*if (PressedKeyOfCurrentPrefab(0))
                {
                    player.GetComponent<PlayerShoot>().HoldFire = false;
                    Destroy(currentPlaceableObject);
                }
                else */
                {
                    if (currentPlaceableObject != null)
                    {
                        Destroy(currentPlaceableObject);
                    }

                    if (Physics.Raycast(ray, out hitInfo, 15f, terrain))
                    {
                        Turret = GameObject.FindGameObjectWithTag("Turret");
                        currentPlaceableObject = Instantiate(placeableObjectPrefabs[0]);
                        currentPrefabIndex = 0;
                    }
                    else {
                        currentPlaceableObject = Instantiate(placeableObjectPrefabs[0], location.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
                        currentPrefabIndex = 0;
                    }
                }
            }
            else if (inv.GameObjDictionary["TurretDetecting"] > 0)
            {
                player.GetComponent<PlayerShoot>().HoldFire = true;
                hope = true;
                WarningCanvas.SetActive(false);
                /*if (PressedKeyOfCurrentPrefab(1))
                {
                    player.GetComponent<PlayerShoot>().HoldFire = false;
                    Destroy(currentPlaceableObject);
                }
                else */
                {
                    if (currentPlaceableObject != null)
                    {
                        Destroy(currentPlaceableObject);
                    }

                    if (Physics.Raycast(ray, out hitInfo, 15f, terrain))
                    {
                        Turret = GameObject.FindGameObjectWithTag("Turret");
                        currentPlaceableObject = Instantiate(placeableObjectPrefabs[1]);
                        currentPrefabIndex = 1;
                    }
                    else {
                        currentPlaceableObject = Instantiate(placeableObjectPrefabs[1], location.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
                        currentPrefabIndex = 1;
                    }
                }
            }
        }
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    public void SetMenu(bool menu)
    {
        Menu = menu;
    }
    public void SetEmpty(bool empty)
    {
        Empty = empty;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 20f, terrain))
        {
            canRotate = true;
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            canRotate = false;
        }
    }


    private void RotateFromMouseWheel()
    {
        currentPlaceableObject.transform.Rotate(Vector3.up, 0f);
        if (canRotate)
        {
            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }
    }

    public void ReleaseIfClicked()  //We are in Build mode, we have the object in our hand, we place it LPM  
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo, 8f, terrain))
        {
            uint tmpCount = 0;
            string CurrentObj = string.Concat(currentPlaceableObject.ToString().TakeWhile(x => x != '('));
            
            tmpCount = inv.GameObjDictionary[CurrentObj];
            inv.GameObjDictionary.Remove(CurrentObj);
            
            if(currentPlaceableObject.ToString().Split('(')[0].Equals("Turret")) inv.RemoveShootingTurret();
            if(currentPlaceableObject.ToString().Split('(')[0].Equals("TurretDetecting")) inv.RemoveDetectingTurret();

            inv.GameObjDictionary.Add(CurrentObj, --tmpCount);
            
            currentPlaceableObject.tag = "ABC";
            currentPlaceableObject = null;
            player.GetComponent<PlayerShoot>().AddDelay();
            player.GetComponent<PlayerShoot>().HoldFire = false;
        }
    }


    public void MenuClick(string path)
    {
        var paths = path.Split('/');   
    }

    public void SetMode(ControllerMode mode)                //We set our modes  
    {
        Mode = mode;
        if (mode != ControllerMode.Menu && MainMenuInstance != null)
            Destroy(MainMenuInstance);

        switch (mode)
        {
            case ControllerMode.Build:
                Canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                player.GetComponent<PlayerShoot>().HoldFire = true;
                player.GetComponent<PlayerShoot>().AddDelay();
                break;
            case ControllerMode.Menu:
                Canvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Camera.main.GetComponent<MouseLook>().enabled = false;
                player.GetComponent<PlayerShoot>().HoldFire = true;
                player.GetComponent<PlayerShoot>().AddDelay();
                break;
            case ControllerMode.Play:
                Canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                player.GetComponent<PlayerShoot>().HoldFire = false;
                break;
        }
    }


    public enum ControllerMode
    {
        Play,
        Build,
        Menu
    }
}