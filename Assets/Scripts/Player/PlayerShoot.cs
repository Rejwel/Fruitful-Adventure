using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    [Header("Misc")]
    public Transform firePoint;
    public GameObject bullet;
    private Inventory inventory;
    
    private Gun currentGun;
    private int currentGunIndex;

    public Vector3 upRecoil;
    public Vector3 bulletOffset;
    private Vector3 originalRotationOfFirepoint;
    private float bulletSpread;
    private float bulletSpeed = 500f;
    private int[] magazine = new int[4];
    private int stashAmmo;
    private int bullets;
    private string desc;
    private bool firstEmptyBullet;
    private bool reloading = false;

    [Header("Timers")]
    private float fireRate;
    private float emptyTime = 0.8f;
    private float reloadTime = 2f;
    private float nextTimeToFire = 0f;
    private float nextTimeToEmpty = 0f;
    private float nextTimeToReload = 0f;
    
    [Header("TMP")]
    private TextMeshProUGUI  currentGunText;
    private TextMeshProUGUI currentAmmoText;
    private TextMeshProUGUI  currentStashAmmoText;

    private void Start()
    {
        originalRotationOfFirepoint = firePoint.transform.localEulerAngles;
        firstEmptyBullet = true;
        inventory = FindObjectOfType<Inventory>();
        currentStashAmmoText = GameObject.Find("StashAmmo").GetComponent<TextMeshProUGUI>();
        currentGunText = GameObject.Find("CurrentGunText").GetComponent<TextMeshProUGUI>();
        currentAmmoText = GameObject.Find("Magazine").GetComponent<TextMeshProUGUI>();
        SetStartingAmmo();
        
        currentGun = inventory.currentGuns[0];
        currentGunIndex = currentGun.GetId();
        ChangeGun(currentGun);
    }

    void Update()
    {
        // RELOADING CANCEL
        if (reloading && Time.time >= nextTimeToFire)
        {
            LowerStashAmmo(currentGun);
            reloading = false;
        }

        // SHOOTING
        if (Input.GetButtonDown("Fire1")) ReturnToOriginalRecoil();
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && magazine[currentGun.GetId()] > 0) 
        {
            Shoot();
        }
        else if (magazine[currentGun.GetId()] == 0 && firstEmptyBullet)
        {
            firstEmptyBullet = false;
            nextTimeToEmpty = Time.time + emptyTime;
        }
        else if (Input.GetButton("Fire1") && magazine[currentGun.GetId()] == 0 && Time.time >= nextTimeToEmpty)
        {
            if (currentGun.GetDesc().Equals("Pistol") && !reloading)
            {
                Empty("Pistol_empty");
            }
            else if (currentGun.GetDesc() != "Pistol" && !reloading)
            {
                Empty("Gun_empty");
            }
        }

        // RELOAD GUN
        if (Input.GetKey(KeyCode.R) && Time.time >= nextTimeToReload && inventory.bulletAmmount[currentGun.GetId()] > 0 && magazine[currentGun.GetId()] != currentGun.GetMagazine())
        {
            Reload(currentGun);
        }

        //SCROLL GUN INPUT
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            currentGunIndex += (Convert.ToInt32(Input.GetAxis("Mouse ScrollWheel") * 10));
            if (currentGunIndex < 0)
            {
                currentGunIndex = 0;
            }
            else if (currentGunIndex > inventory.currentGuns.Count-1)
            {
                currentGunIndex = inventory.currentGuns.Count-1;
            }
            currentGun = inventory.currentGuns[currentGunIndex];
            ChangeGun(currentGun);
        }
        
        //KEYBOARD GUN INPUT
        if (Input.GetKeyDown("1"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(0) != null)
            {
                currentGun = inventory.currentGuns[0];
                ChangeGun(currentGun);
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(1) != null)
            {
                currentGun = inventory.currentGuns[1];
                ChangeGun(currentGun);
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(2) != null)
            {
                currentGun = inventory.currentGuns[2];
                ChangeGun(currentGun);
            }
        }
        if (Input.GetKeyDown("4"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(3) != null)
            {
                currentGun = inventory.currentGuns[3];
                ChangeGun(currentGun);
            }
        }
    }

    void Shoot()
    {
        AddRecoil();
        magazine[currentGun.GetId()]--;
        currentAmmoText.text = "Ammo " + magazine[currentGun.GetId()];
        fireRate = currentGun.GetFireRate();
        bullets = currentGun.GetBullets();
        desc = currentGun.GetDesc();
        nextTimeToFire = Time.time + fireRate;
        GameObject InstantiateBullet;
        AudioManager.playSound(desc);
        for (int i = 0; i < bullets; i++)
        {
            Vector3 forwardVector = Vector3.forward;
            
            float deviation = Random.Range(0f, bulletSpread);
            float angle = Random.Range(0f, 360f);
            
            forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
            forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
            forwardVector = firePoint.transform.rotation * forwardVector;
            
            InstantiateBullet = Instantiate(bullet, firePoint.position, transform.rotation * Quaternion.Euler(new Vector3(270,0,90)));
            InstantiateBullet.GetComponent<Rigidbody>().AddForce(forwardVector * bulletSpeed);
        }
    }

    public Gun GetCurrentGun()
    {
        return currentGun;
    }

    private void ChangeGun(Gun currentGun)
    {
        AudioManager.stopSound();
        reloading = false;
        
        firstEmptyBullet = true;
        fireRate = currentGun.GetFireRate();
        bulletSpread = currentGun.GetSpread();

        if(inventory.bulletAmmount[currentGun.GetId()] > 100000)
        {
            currentStashAmmoText.text = "∞";
        }
        else
        {
            currentStashAmmoText.text = inventory.bulletAmmount[currentGun.GetId()].ToString();
        }

        currentAmmoText.text = currentGun.GetMagazine().ToString();
        
        currentGunText.text = "Gun: " + currentGun.GetDesc();
        currentAmmoText.text = "Ammo " + magazine[currentGun.GetId()];
    }

    private void Empty(string sound)
    {
        nextTimeToEmpty = Time.time + emptyTime;
        AudioManager.playSound(sound);
    }

    private void Reload(Gun currentGun)
    {
        reloading = true;
        nextTimeToReload = Time.time + reloadTime;
        firstEmptyBullet = true;
        AudioManager.playSound("Gun_reload");
        nextTimeToFire = Time.time + reloadTime;
    }

    private void LowerStashAmmo(Gun currentGun)
    {
        int ammoToSubstract;
        
        if (inventory.bulletAmmount[currentGun.GetId()] + magazine[currentGun.GetId()] <= currentGun.GetMagazine())
        {
            magazine[currentGun.GetId()] += inventory.bulletAmmount[currentGun.GetId()];
            inventory.bulletAmmount[currentGun.GetId()] = 0;
            currentAmmoText.text = "Ammo " + magazine[currentGun.GetId()];
        }
        else if (inventory.bulletAmmount[currentGun.GetId()] + magazine[currentGun.GetId()] > currentGun.GetMagazine())
        {
            ammoToSubstract = currentGun.GetMagazine() - magazine[currentGun.GetId()];
            magazine[currentGun.GetId()] = currentGun.GetMagazine();
            inventory.bulletAmmount[currentGun.GetId()] -= ammoToSubstract;
            currentAmmoText.text = "Ammo " + currentGun.GetMagazine();
        }
        if(inventory.bulletAmmount[currentGun.GetId()] > 10000)
        {
            currentStashAmmoText.text = "∞";
        }
        else
        {
            currentStashAmmoText.text = inventory.bulletAmmount[currentGun.GetId()].ToString();
        }
    }

    private void SetStartingAmmo()
    {
        for (int i = 0; i < 4; i++)
        {
            magazine[i] = GunContainer.GetGun(i).GetMagazine();
        }
    }
    
    private void AddRecoil()
    {
        firePoint.transform.localEulerAngles += upRecoil;
    }

    private void ReturnToOriginalRecoil()
    {
        firePoint.transform.localEulerAngles = originalRotationOfFirepoint;
    }
}
