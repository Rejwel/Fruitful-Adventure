using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyAfter5Sec : MonoBehaviour
{
    private Vector3 scaleChange;
    
    private void Start()
    {
        Invoke("StartDestroy", Random.Range(5f,7f));
    }

    private void Update()
    {
        if(scaleChange != new Vector3())
            this.gameObject.transform.localScale += scaleChange * Time.deltaTime * 50f;
        if(this.gameObject.transform.localScale.x <= 0) Destroy(this.gameObject);
    }

    private void StartDestroy()
    {
        scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
    }
}
