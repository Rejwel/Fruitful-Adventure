using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject currentHitObject;
    Money Money;
    
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;

    private void Start()
    {
        Money = FindObjectOfType<Money>();
    }

    private void Update()
    {
        origin = transform.position;
        direction = transform.forward;

        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask))
        {
            Money.AddMoney();
            Destroy(hit.transform.gameObject);
        }
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Debug.DrawLine(origin, origin+direction*currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
