using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public Rigidbody bullet;
    private float bulletSpeed = 500f;
    
    private float fireRate = 0.5f;
    private float nextTimeToFire = 0f;


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        nextTimeToFire = Time.time + fireRate;
        Rigidbody bulletRigidbody;
        bulletRigidbody = Instantiate(bullet, firePoint.position, firePoint.rotation) as Rigidbody;
        bulletRigidbody.AddForce(firePoint.forward * bulletSpeed);
    }
}
