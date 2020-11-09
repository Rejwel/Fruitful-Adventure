using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
