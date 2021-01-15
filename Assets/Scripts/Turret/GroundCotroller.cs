using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCotroller : MonoBehaviour
{
    [SerializeField]
    private GameObject placeableObjectPrefab;
    public LayerMask terrain;
    private bool canRotate = false;
    public LayerMask TurretOFF;
    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.A;

    public GameObject currentPlaceableObject;
    public GameObject player;
    private float mouseWheelRotation;
    public bool hope;

    private void Update()
    {
        
        HandleNewObjectHotkey();
        
        if (currentPlaceableObject != null)
        {
            
            if (currentPlaceableObject.tag.Equals("Turret"))
            {
                currentPlaceableObject.GetComponent<Turret>().enabled = false;
            }
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            if (hope)
            { 
                ReleaseIfClicked();
                
            }
        }
        
           
        
    }

    private void HandleNewObjectHotkey()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Input.GetKeyDown(newObjectHotkey))
        {
            if (currentPlaceableObject != null && Physics.Raycast(ray, out hitInfo, TurretOFF))
            {
                Destroy(currentPlaceableObject);
                player.GetComponent<PlayerShoot>().enabled = true;
            }
            else if (Physics.Raycast(ray, out hitInfo, 8f, terrain))
            {
                player.GetComponent<PlayerShoot>().enabled = false;
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
            }
        }
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

        

        if (currentPlaceableObject.tag.Equals("Turret"))
        {

            
            currentPlaceableObject.GetComponent<Turret>().enabled = true;
            
            
        }
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo, 8f, terrain))
        {
            currentPlaceableObject.tag = "ABC";
            currentPlaceableObject = null;
            
        }
        
    }


}