using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public Rigidbody bullet;
    private float bulletSpeed = 500f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bulletRigidbody;
            bulletRigidbody = Instantiate(bullet, firePoint.position, firePoint.rotation) as Rigidbody;
            bulletRigidbody.AddForce(firePoint.forward * bulletSpeed);
        }
    }
}
