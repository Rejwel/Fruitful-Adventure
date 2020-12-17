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
    public Transform firePoint;
    public Rigidbody bullet;
    private float bulletSpeed = 500f;
    private float magazine;
    private int bullets;
    private float randomSpread;
    private string desc;
    private bool firstEmptyBullet;
    private Gun currentGun;
    private Weapon weapon;

    private float fireRate;
    private float emptyTime = 0.8f;
    private float reloadTime = 2f;
    
    private float nextTimeToFire = 0f;
    private float nextTimeToEmpty = 0f;
    private float nextTimeToReload = 0f;
    

    private TextMeshProUGUI  currentGunText;
    private TextMeshProUGUI currentAmmoText;


    private void Start()
    {
        firstEmptyBullet = true;
        currentGunText = GameObject.Find("CurrentGunText").GetComponent<TextMeshProUGUI>();
        currentAmmoText = GameObject.Find("Magazine").GetComponent<TextMeshProUGUI>();
        currentGun = GunContainer.GetGun(0);
        ChangeGun(GunContainer.GetGun(0));
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && magazine > 0) 
        {
            Shoot();
            currentAmmoText.text = "Ammo " + magazine;
        }
        else if (magazine == 0 && firstEmptyBullet)
        {
            firstEmptyBullet = false;
            nextTimeToEmpty = Time.time + emptyTime;
        }
        else if (Input.GetButton("Fire1") && magazine == 0 && Time.time >= nextTimeToEmpty)
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
        if (Input.GetKey(KeyCode.R) && Time.time >= nextTimeToReload)
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
        magazine--;
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
        magazine = currentGun.GetMagazine();
        fireRate = currentGun.GetFireRate();
        currentGunText.text = "Gun: " + currentGun.GetDesc();
        currentAmmoText.text = "Ammo " + magazine;
    }

    private void Empty(string sound)
    {
        nextTimeToEmpty = Time.time + emptyTime;
        AudioManager.playSound(sound);
    }

    private void Reload(Gun currentGun)
    {
        nextTimeToReload = Time.time + reloadTime;
        firstEmptyBullet = true;
        AudioManager.playSound("Gun_reload");
        nextTimeToFire = Time.time + reloadTime;
        magazine = currentGun.GetMagazine();
        currentAmmoText.text = "Ammo " + magazine;
    }
}
