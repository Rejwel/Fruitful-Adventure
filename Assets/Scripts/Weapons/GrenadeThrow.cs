    using System;
using System.Collections;
using System.Collections.Generic;
    using TMPro;
    using UnityEditor;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class GrenadeThrow : MonoBehaviour
{

    public float throwForce = 13f;
    public GameObject grenadePrefab;
    public GameObject FirePoint;
    public GameObject GrenadeUp;
    public GameObject GrenadeDown;
    private GameObject Grenade;
    private PlayerShoot PlayerShoot;
    private bool GrenadeSelected { get; set; }
    
    private TextMeshProUGUI  currentGrenades;
    private Inventory inventory;

    private void Start()
    {
        Physics.IgnoreLayerCollision(21,13);
        Physics.IgnoreLayerCollision(21,20);
        Physics.IgnoreLayerCollision(21,21);
        PlayerShoot = FindObjectOfType<PlayerShoot>();
        inventory = FindObjectOfType<Inventory>();
        currentGrenades = GameObject.Find("GrenadesText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        currentGrenades.text = $"Grenades : {inventory.GetGrenades()}";
        PlayerShoot.enabled = GrenadeSelected ? false : true;

        if(inventory.GetGrenades() > 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                PlayerShoot.HoldFire = true;
                if (!GrenadeSelected)
                {
                    Grenade = Instantiate(grenadePrefab, FirePoint.transform.position, FirePoint.transform.rotation);
                }

                GrenadeSelected = !GrenadeSelected;
                if (!GrenadeSelected)
                {
                    PlayerShoot.HoldFire = false;
                    Destroy(Grenade);
                }
            }

            if (GrenadeSelected)
            {
                Grenade.transform.position = FirePoint.transform.position;
                if (Input.GetButtonDown("Fire1"))
                {
                    PlayerShoot.AddDelay();
                    ThrowGrenade();
                    GrenadeSelected = false;
                    PlayerShoot.HoldFire = false;
                    inventory.RemoveGrenade();
                    Destroy(Grenade);
                }
            }
        }
        
    }

    void ThrowGrenade()
    {
        Vector3 forwardVector = Vector3.forward;
        forwardVector = FirePoint.transform.rotation * forwardVector;
        
        GameObject GrenadeUpPart = Instantiate(GrenadeUp, FirePoint.transform.position, FirePoint.transform.rotation);
        GameObject GrenadeDownPart = Instantiate(GrenadeDown, FirePoint.transform.position, FirePoint.transform.rotation);

        GrenadeDownPart.GetComponent<Rigidbody>().AddForce(forwardVector * throwForce, ForceMode.Impulse);
        GrenadeUpPart.GetComponent<Rigidbody>().AddForce(forwardVector * throwForce / 3 + new Vector3(-3f,-3f,0f), ForceMode.Impulse);

    }
}
