using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;        //za ile granat zrobi BUMMM

    public float closeArea = 2f;
    public float mediumArea = 4f;
    public float farArea = 6f;
    public float force = 200f;      //sila wybuchu, chyba xd

    bool hasExploded = false;   //czy granat zrobił BUUMM
    private EnemyMechanics givedamage;     //"dołączenie" innego skryptu 
    public GameObject FinalGrenade;

    float countdown;        //odliczanie

    void Start()
    {
        countdown = delay;
    }

    private void Awake()
    {
        givedamage = FindObjectOfType<EnemyMechanics>();
    }

    void Update()       //odlicza te 3 sekundy, dzięki hasExploded wybucha tylko raz
    {
        countdown -= Time.deltaTime;
        
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            Instantiate(FinalGrenade, this.gameObject.transform.position, this.gameObject.transform.rotation);
            hasExploded = true;
        }
    }

    void Explode()
    {
        int killed = 0;
        WaveManager WaveManager = FindObjectOfType<WaveManager>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);       //przechowuje dane przeciwników, któzy znaleźli się w obszarze wybuchu

        foreach (Collider nearbyObject in colliders)    //pęętlaa 
        {
            EnemyMechanics enemy = nearbyObject.GetComponent<EnemyMechanics>();
            Explosion explosion = FindObjectOfType<Explosion>();

            float distance = Vector3.Distance(nearbyObject.transform.position, transform.position);  //dystans między wybuchem a obiektem, który dostał
            if (nearbyObject.CompareTag("Enemy"))       //jeżeli tag tego przeciwnika równa się Enemy
            {

                if (distance <= closeArea)      //gdy przeciwnik jest bardzo blisko granatu
                {
                    nearbyObject.GetComponent<EnemyMechanics>().TakeDamage(75);
                }
                else if (distance <= mediumArea) //gdy przeciwnik jest blisko granatu
                {
                    nearbyObject.GetComponent<EnemyMechanics>().TakeDamage(55);

                }
                else if (distance <= farArea)   //gdy przeciwnik jest dość daleko od granatu
                {
                    nearbyObject.GetComponent<EnemyMechanics>().TakeDamage(45);
                }

                if (enemy.GetHealth() <= 0)         //przeciwnik umiera
                {
                    killed++;
                    nearbyObject.GetComponent<Collider>().enabled = false;
                    enemy.Die();
                    explosion.explode(nearbyObject.gameObject.transform);
                }
            }
        }
        for (int i = 0; i < killed; i++)
        {
            WaveManager.UpdateEnemyCounter();
        }
        
        Destroy(gameObject);    //granat "znika"
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 6f);
    }
    
}
