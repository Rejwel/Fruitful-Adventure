using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCotroller : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;
    public LayerMask terrain;
    private bool canRotate = false;

    private int currentPrefabIndex = -1;

    public GameObject currentPlaceableObject;
    public GameObject player;
    private float mouseWheelRotation;
    public bool hope=true;
    public GameObject WarningCanvas;


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
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
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

                    if (Physics.Raycast(ray, out hitInfo, 8f, terrain))
                    {
                        
                        currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
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
            currentPlaceableObject.tag = "ABC";
            currentPlaceableObject = null;
        }
    }

}