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
            // player.GetComponent<PlayerShoot>().enabled = false;
            // player.GetComponent<GrenadeThrow>().enabled = false;
            player.GetComponent<PlayerShoot>().HoldFire = true;
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
        for (int i = 0; i < inv.LengthOfTurrets(); i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 9 - i))
            {
                hope = true;
                WarningCanvas.SetActive(false);
                if (PressedKeyOfCurrentPrefab(i))
                {
                    Destroy(currentPlaceableObject);
                    currentPrefabIndex = -1;
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
                        currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                        currentPrefabIndex = i;
                    }
                    else {
                        currentPlaceableObject = Instantiate(placeableObjectPrefabs[i], location.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
                        currentPrefabIndex = i;
                    }
                }
                break;
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