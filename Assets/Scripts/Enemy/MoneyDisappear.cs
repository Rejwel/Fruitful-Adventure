using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoneyDisappear : MonoBehaviour
{
    private void Start()
    {
        Invoke("destroyMoney", 20);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Physics.IgnoreCollision(other.collider, this.GetComponent<Collider>());
        }
    }

    void destroyMoney()
    {
        Destroy(this.gameObject);
    }
}
