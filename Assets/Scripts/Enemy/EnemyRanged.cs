using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public class EnemyRanged : MonoBehaviour
{
    //Following 
    public NavMeshAgent agent;
    private GameObject Player;
    private Rigidbody enemyRb;
    private Quaternion rotation;
    
    //Shooting
    public float czekaj = 2f;
    private float odliczanieDoStrzalu = 1f;
    public GameObject strzalaPrefab;
    public float predkosc = 7;
    public bool patrzNaGracza = false;
    private Quaternion rotacjaPocisku;

    private Vector3 AttackingObjectPosition;

    //Distance from Player
    private CharacterController PlayerTransform;
    private bool InRange;
    private bool IsAttacking;
    public LayerMask PlayerLayer;
    public LayerMask BuildingLayer;
    private bool InShortRange;
    private bool InLongRange;
    private bool InCenterRange;
    public float shortAttack, longAttack, centerPoint;
    
    private GameObject WhatToAttack { get; set; }


    private void Awake()
    {
        WhatToAttack = WaveManager.AttackingBuilding;
        agent = GetComponent<NavMeshAgent>();
        Player = FindObjectOfType<HealthPlayer>().gameObject;
    }

    void Update()
    {
        //print(WhatToAttack);
        
        WhatToAttack = WaveManager.AttackingBuilding;
        
        InRange = Physics.CheckSphere(transform.position, 20, PlayerLayer);

        if (InRange || WhatToAttack == null)
        {
            Attack(Player, PlayerLayer);
        }
        else if (WhatToAttack != null)
        {
            Attack(WhatToAttack, BuildingLayer);
        }
    }

    void Attack(GameObject AttackingObject, LayerMask AttackingObjectLayer)
    {
        // 3 positions of enemy attacking player
        InShortRange = Physics.CheckSphere(transform.position, shortAttack, AttackingObjectLayer);
        InCenterRange = Physics.CheckSphere(transform.position, centerPoint, AttackingObjectLayer);
        InLongRange = Physics.CheckSphere(transform.position, longAttack, AttackingObjectLayer);

        AttackingObjectPosition = new Vector3(AttackingObject.transform.position.x, AttackingObject.transform.position.y, AttackingObject.transform.position.z);

        if (InShortRange || InCenterRange)
        {
            strzal(AttackingObject);
            Vector3 dirToPlayer = transform.position - AttackingObject.transform.position;  //when player is close he moves back
            Vector3 newPos = transform.position + dirToPlayer;
            agent.SetDestination(newPos);
        }
        else if (InLongRange)
        {
            agent.SetDestination(AttackingObject.transform.position);
        }
        else
        {
            agent.SetDestination(AttackingObject.transform.position);
        }
        transform.LookAt(AttackingObject.transform.position);
    }
    
    //Shooting Part
    private void strzal(GameObject AttackObject)    //Funkcja odpowiadająca za strzał
    {

        if (odliczanieDoStrzalu < czekaj)
        {
            odliczanieDoStrzalu += Time.deltaTime;  //licznik do kolejnego strzału
        }


        if (odliczanieDoStrzalu >= czekaj)
        {
            if(gameObject.name.Equals("Range(Clone)")) AudioManager.playSound("enemyRanged");
            else if(gameObject.name.Equals("Mage(Clone)")) AudioManager.playSound("enemyMage");
            
            odliczanieDoStrzalu = 0;
            GameObject pocisk;

            pocisk = Instantiate(strzalaPrefab, transform.position + transform.forward, getRotacjaPocisku(AttackObject));
            pocisk.GetComponent<Rigidbody>().AddForce(transform.forward * predkosc, ForceMode.Impulse);
            pocisk.GetComponent<Rigidbody>().AddForce(transform.up * 1.4f, ForceMode.Impulse);
            
        }
    }
    private Quaternion getRotacjaPocisku(GameObject AttackObject)    //na podstawie pozycji gracza ustala kierunek pozycji pocisku, do której ma zmierzać
    {
        AttackingObjectPosition = new Vector3(AttackObject.transform.position.x, AttackObject.transform.position.y, AttackObject.transform.position.z);
        return Quaternion.LookRotation(AttackingObjectPosition - transform.position);
    }
    
    public void SetAttackToPlayer()
    {
        agent.SetDestination(PlayerTransform.transform.position);
        transform.LookAt(PlayerTransform.transform.position);
    }
}
