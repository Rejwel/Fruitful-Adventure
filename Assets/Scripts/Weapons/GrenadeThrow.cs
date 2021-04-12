    using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject Gun;
    private GameObject Grenade;
    private PlayerShoot PlayerShoot;
    private bool GrenadeSelected { get; set; }

    private void Start()
    {
        Physics.IgnoreLayerCollision(21,13);
        Physics.IgnoreLayerCollision(21,20);
        Physics.IgnoreLayerCollision(21,21);
        PlayerShoot = FindObjectOfType<PlayerShoot>();
    }

    void Update()
    {
        PlayerShoot.enabled = GrenadeSelected ? false : true;
        if (Input.GetKeyDown(KeyCode.G))
        {
            Gun.SetActive(false);
            if (!GrenadeSelected)
            {
                Grenade = Instantiate(grenadePrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            }
            GrenadeSelected = !GrenadeSelected;
        }

        if (GrenadeSelected)
        {
            Grenade.transform.position = FirePoint.transform.position;
            if (Input.GetButtonDown("Fire1"))
            {
                PlayerShoot.AddDelay();
                ThrowGrenade();
                GrenadeSelected = false;
                Gun.SetActive(true);
            }
        }
        else
        {
            Gun.SetActive(true);
            Destroy(Grenade);
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
