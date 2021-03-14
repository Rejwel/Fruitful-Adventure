using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour
{
    public static readonly float AttackSpeed = 3f;
    public static readonly float PlayerRange = 30f;
    public void Die()
    {
        Destroy(gameObject);
    }

}
