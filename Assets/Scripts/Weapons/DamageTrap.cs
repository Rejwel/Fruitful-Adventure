using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    [SerializeField] private float counter = 1f;
    [SerializeField] private float wait = 2f;

    //private bool _isDamage = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (counter < wait)
            {
                counter += Time.deltaTime;
            }
            else if (counter >= wait)
            { 
                counter = 0f;
                other.GetComponent<EnemyMechanics>().TakeDamage(30);
            }

            if (other.GetComponent<EnemyMechanics>().GetHealth() <= 0) 
            {
                other.GetComponent<EnemyMechanics>().Die();
                other.GetComponent<Explosion>().explode(other.transform);
            }
        }
    }
    
}
