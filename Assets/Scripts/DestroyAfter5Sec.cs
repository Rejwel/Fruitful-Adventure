using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyAfter5Sec : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
