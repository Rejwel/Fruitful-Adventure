using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider hit)
    {
        EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
        if (hit.tag.Equals("Enemy"))
        {
            enemy.Die();
            Destroy(gameObject);
        }
    }
}
