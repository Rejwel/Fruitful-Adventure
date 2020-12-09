using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoneyDisappear : MonoBehaviour
{
    private Vector3 scaleChange;
    private void Start()
    {
        Invoke("startDestroyMoney", 15);
    }

    private void Update()
    {
        if(scaleChange != new Vector3())
            this.gameObject.transform.localScale += scaleChange * Time.deltaTime * 50f;
        if(this.gameObject.transform.localScale.x <= 0) Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Physics.IgnoreCollision(other.collider, this.GetComponent<Collider>());
        }
    }

    void startDestroyMoney()
    {
        scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
    }
}
