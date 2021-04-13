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
    public bool hope=true;
    public GameObject WarningCanvas;
    public Transform location;
    public GameObject Turret;
    private Inventory inv;

    private void Start()
    {
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
    }
    
    private void HandleNewObjectHotkey()
    {
        WarningCanvas.SetActive(false);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Input.GetKeyDown(KeyCode.Alpha9) && inv.GameObjDictionary["Turret"] > 0)
        {
            player.GetComponent<PlayerShoot>().HoldFire = true;
            hope = true;
            WarningCanvas.SetActive(false);
            if (PressedKeyOfCurrentPrefab(0))
            {
                player.GetComponent<PlayerShoot>().HoldFire = false;
                Destroy(currentPlaceableObject);
            }
            else
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
        else if (Input.GetKeyDown(KeyCode.Alpha8) && inv.GameObjDictionary["TurretDetecting"] > 0)
        {
            player.GetComponent<PlayerShoot>().HoldFire = true;
            hope = true;
            WarningCanvas.SetActive(false);
            if (PressedKeyOfCurrentPrefab(1))
            {
                player.GetComponent<PlayerShoot>().HoldFire = false;
                Destroy(currentPlaceableObject);
            }
            else
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

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
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

    public void ReleaseIfClicked()
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
            else inv.RemoveDetectingTurret();
            
            inv.GameObjDictionary.Add(CurrentObj, --tmpCount);

            
            
            currentPlaceableObject.tag = "ABC";
            currentPlaceableObject = null;
            player.GetComponent<PlayerShoot>().AddDelay();
            player.GetComponent<PlayerShoot>().HoldFire = false;
            
        }
    }

}