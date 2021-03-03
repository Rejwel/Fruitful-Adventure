using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;        //za ile granat zrobi BUMMM
    public GameObject explosionEffect;      //eksplozja 

    public float closeArea = 2f;
    public float mediumArea = 4f;
    public float farArea = 6f;
    public float force = 200f;      //sila wybuchu, chyba xd

    bool hasExploded = false;   //czy granat zrobił BUUMM
    private Transform explosive;    //lokalizacja eksplozji
    private HealthEnemy givedamage;     //"dołączenie" innego skryptu 

    float countdown;        //odliczanie

    void Start()
    {
        countdown = delay;
    }

    private void Awake()
    {
        explosive = transform;
        givedamage = FindObjectOfType<HealthEnemy>();
    }

    void Update()       //odlicza te 3 sekundy, dzięki hasExploded wybucha tylko raz
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);  //"klonuje" nowy obiekt 

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);       //przechowuje dane przeciwników, któzy znaleźli się w obszarze wybuchu

        foreach (Collider nearbyObject in colliders)    //pęętlaa 
        {
            EnemyMechanics enemy = nearbyObject.GetComponent<EnemyMechanics>();
            HealthEnemy enemyHealth = nearbyObject.GetComponent<HealthEnemy>();
            Explosion explosion = FindObjectOfType<Explosion>();
            WaveManager WaveManager = FindObjectOfType<WaveManager>();

            float distance = Vector3.Distance(nearbyObject.transform.position, explosive.position);  //dystans między wybuchem a obiektem, który dostał
            if (nearbyObject.CompareTag("Enemy"))       //jeżeli tag tego przeciwnika równa się Enemy
            {

                if (distance <= closeArea)      //gdy przeciwnik jest bardzo blisko granatu
                {
                    nearbyObject.GetComponent<HealthEnemy>().TakeDamage(75);
                }
                else if (distance <= mediumArea) //gdy przeciwnik jest blisko granatu
                {
                    nearbyObject.GetComponent<HealthEnemy>().TakeDamage(55);

                }
                else if (distance <= farArea)   //gdy przeciwnik jest dość daleko od granatu
                {
                    nearbyObject.GetComponent<HealthEnemy>().TakeDamage(45);
                }

                if (enemyHealth.currentHealth <= 0)         //przeciwnik umiera
                {
                    nearbyObject.GetComponent<Collider>().enabled = false;
                    enemy.Die();
                    explosion.explode(nearbyObject.gameObject);
                    WaveManager.killEnemy();
                }

            }

        }

        Destroy(gameObject);    //granat "znika"
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 6f);
    }
}
