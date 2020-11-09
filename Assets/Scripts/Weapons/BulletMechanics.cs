using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    private Rigidbody thisRb;
    private Vector3 bulletForce;
    private float force = 1000f;

    void Awake()
    {
        thisRb = gameObject.GetComponent<Rigidbody>();
        bulletForce = new Vector3(0f, 0f, force);
    }

    void Start()
    {
        thisRb.AddForce(bulletForce);
    }

}
