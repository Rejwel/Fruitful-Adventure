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
    private int bullets;
    private float randomSpread;
    private string desc;

    private Weapon weapon;

    private float fireRate;
    private Gun currentGun;
    private float nextTimeToFire = 0f;

    private TextMeshProUGUI  currentGunText;


    private void Start()
    {
        currentGunText = GameObject.Find("CurrentGunText").GetComponent<TextMeshProUGUI>();
        currentGun = GunContainer.GetGun(0);
        fireRate = currentGun.GetFireRate();
        currentGunText.text = "Gun: " + currentGun.GetDesc();
    }

    void Update()
    {
        
        
        
        
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
        }
        if (Input.GetKeyDown("1"))
        {
            currentGun = GunContainer.GetGun(0);
            currentGunText.text = "Gun: " + currentGun.GetDesc();
        }
        if (Input.GetKeyDown("2"))
        {
            currentGun = GunContainer.GetGun(1);
            currentGunText.text = "Gun: " + currentGun.GetDesc();
        }
        if (Input.GetKeyDown("3"))
        {
            currentGun = GunContainer.GetGun(2);
            currentGunText.text = "Gun: " + currentGun.GetDesc();
        }
        if (Input.GetKeyDown("4"))
        {
            currentGun = GunContainer.GetGun(3);
            currentGunText.text = "Gun: " + currentGun.GetDesc();
        }
    }

    void Shoot()
    {
        fireRate = currentGun.GetFireRate();
        bullets = currentGun.GetBullets();
        desc = currentGun.GetDesc();

        //randomSpread = Random.Range(-currentGun.GetSpread(), currentGun.GetSpread());
        //firePoint.rotation = Quaternion.Euler(new Vector3(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z + randomSpread));
        
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
            //bulletRigidbody.AddForce(firePoint.forward * bulletSpeed);
            bulletRigidbody.AddForce(forwardVector * bulletSpeed);
        }
    }

    public Gun GetCurrentGun()
    {
        return currentGun;
    }
}
