using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject openShop;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerMovement"))
            openShop.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PlayerMovement"))
            openShop.SetActive(false);
    }
}
