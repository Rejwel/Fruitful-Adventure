using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    [Header("Misc")]
    public Transform firePoint;
    public Rigidbody bullet;
    private Inventory inventory;
    private Gun currentGun;
    private Weapon weapon;
    private float bulletSpeed = 500f;
    private int[] magazine = new int[4];
    private int stashAmmo;
    private int bullets;
    private float randomSpread;
    private string desc;
    private bool firstEmptyBullet;

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
        firstEmptyBullet = true;
        inventory = FindObjectOfType<Inventory>();
        currentStashAmmoText = GameObject.Find("StashAmmo").GetComponent<TextMeshProUGUI>();
        currentGunText = GameObject.Find("CurrentGunText").GetComponent<TextMeshProUGUI>();
        currentAmmoText = GameObject.Find("Magazine").GetComponent<TextMeshProUGUI>();
        SetStartingAmmo();
        currentGun = GunContainer.GetGun(0);
        ChangeGun(currentGun);
    }

    void Update()
    {
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
            if (currentGun.GetDesc().Equals("Pistol"))
            {
                Empty("Pistol_empty");
            }
            else if (currentGun.GetDesc() != "Pistol")
            {
                Empty("Gun_empty");
            }
        }
        if (Input.GetKey(KeyCode.R) && Time.time >= nextTimeToReload && inventory.bulletAmmount[currentGun.GetId()] > 0 && magazine[currentGun.GetId()] != currentGun.GetMagazine())
        {
            Reload(currentGun);
        }
        if (Input.GetKeyDown("1"))
        {
            currentGun = GunContainer.GetGun(0);
            ChangeGun(currentGun);
        }
        if (Input.GetKeyDown("2"))
        {
            currentGun = GunContainer.GetGun(1);
            ChangeGun(currentGun);
        }
        if (Input.GetKeyDown("3"))
        {
            currentGun = GunContainer.GetGun(2);
            ChangeGun(currentGun);
        }
        if (Input.GetKeyDown("4"))
        {
            currentGun = GunContainer.GetGun(3);
            ChangeGun(currentGun);
        }
    }

    void Shoot()
    {
        magazine[currentGun.GetId()]--;
        currentAmmoText.text = "Ammo " + magazine[currentGun.GetId()];
        fireRate = currentGun.GetFireRate();
        bullets = currentGun.GetBullets();
        desc = currentGun.GetDesc();

        nextTimeToFire = Time.time + fireRate;
        float maxDeviation = 2f;
        Vector3 forwardVector = Vector3.forward;
        float deviation = Random.Range(0f, maxDeviation);
        float angle = Random.Range(0f, 360f);
        forwardVector = Quaternion.AngleAxis(randomSpread, Vector3.up) * forwardVector;
        forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
        forwardVector = firePoint.transform.rotation * forwardVector;
        
        for (int i = 0; i < bullets; i++)
        {
            AudioManager.playSound(desc);
            Rigidbody bulletRigidbody;
            bulletRigidbody = Instantiate(bullet, firePoint.position, firePoint.rotation) as Rigidbody;
            bulletRigidbody.AddForce(forwardVector * bulletSpeed);
        }
    }

    public Gun GetCurrentGun()
    {
        return currentGun;
    }

    private void ChangeGun(Gun currentGun)
    {
        firstEmptyBullet = true;
        fireRate = currentGun.GetFireRate();

        currentStashAmmoText.text = inventory.bulletAmmount[currentGun.GetId()].ToString();
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
        LowerStashAmmo(currentGun);
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
        currentStashAmmoText.text = inventory.bulletAmmount[currentGun.GetId()].ToString();
    }

    private void SetStartingAmmo()
    {
        Gun gun;
        for (int i = 0; i < 4; i++)
        {
            magazine[i] = GunContainer.GetGun(i).GetMagazine();
        }
    }
}
