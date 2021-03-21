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
    private Transform Player;
    private Rigidbody enemyRb;
    private bool follow = false;
    private Quaternion rotation;
    
    //Shooting
    public float czekaj = 2f;
    private float odliczanieDoStrzalu = 1f;
    public GameObject strzalaPrefab;
    public float predkosc = 7;
    public bool patrzNaGracza = false;
    private Quaternion rotacjaPocisku;

    //Wild Moooves 
    private Vector3 pozycjaGraczaXYZ;

    //Distance from Player
    private CharacterController PlayerTransform;
    private bool InRange;
    private bool IsAttacking;
    public LayerMask PlayerLayer;
    public LayerMask BuildingLayer;
    public bool playerInShortRange;
    public bool playerInLongRange;
    public bool playerInCenterRange;
    public float shortAttack, longAttack, centerPoint;
    
    private GameObject WhatToAttack { get; set; }




    private void Awake()
    {
        WhatToAttack = WaveManager.AttackingBuilding;
        Player = WhatToAttack.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        PlayerTransform = FindObjectOfType<HealthPlayer>().Player;
        enemyRb = gameObject.GetComponent<Rigidbody>();
        
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        
    }
    

    void Update()
    {
        print(WhatToAttack);
        WhatToAttack = WaveManager.AttackingBuilding;
        //if (Player == null) Player = WaveManager.AttackingBuilding.transform;
        InRange = Physics.CheckSphere(transform.position, 20, PlayerLayer);
        if (InRange && !IsAttacking || WhatToAttack.GetComponent<BuildingReference>().GetBuilding().GetComponent<BuildingHealth>().buildingDestroyed)
        {
            // 3 positions of enemy attacking player
            playerInShortRange = Physics.CheckSphere(transform.position, shortAttack, PlayerLayer);
            playerInCenterRange = Physics.CheckSphere(transform.position, centerPoint, PlayerLayer);
            playerInLongRange = Physics.CheckSphere(transform.position, longAttack, PlayerLayer);

            pozycjaGraczaXYZ = new Vector3(PlayerTransform.transform.position.x, PlayerTransform.transform.position.y, PlayerTransform.transform.position.z);
            patrzNaGracza = false;

            if (playerInShortRange || playerInCenterRange)
            {
                strzal();
                patrzNaGracza = true;
                Vector3 dirToPlayer = transform.position - PlayerTransform.transform.position;  //when player is close he moves back
                Vector3 newPos = transform.position + dirToPlayer;
                agent.SetDestination(newPos);
            }
            else if (playerInLongRange)
            {
                agent.SetDestination(PlayerTransform.transform.position);
            }
            else
            {
                agent.SetDestination(PlayerTransform.transform.position);
            }
            transform.LookAt(PlayerTransform.transform.position);
        }
        else
        {
            // 3 positions of enemy attacking player
            playerInShortRange = Physics.CheckSphere(transform.position, shortAttack, BuildingLayer);
            playerInCenterRange = Physics.CheckSphere(transform.position, centerPoint, BuildingLayer);
            playerInLongRange = Physics.CheckSphere(transform.position, longAttack, BuildingLayer);

            pozycjaGraczaXYZ = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            patrzNaGracza = false;

            if (playerInShortRange || playerInCenterRange)
            {
                IsAttacking = true;
                strzal();
                patrzNaGracza = true;
                Vector3 dirToPlayer = transform.position - Player.transform.position;  //when player is close he moves back
                Vector3 newPos = transform.position + dirToPlayer;
                agent.SetDestination(newPos);
            }
            else if (playerInLongRange)
            {
                agent.SetDestination(Player.transform.position);
            }
            else
            {
                agent.SetDestination(Player.transform.position);
            }
            transform.LookAt(Player.transform.position);
        }
    }

    //Shooting Part
    public void strzal()    //Funkcja odpowiadająca za strzał
    {

        if (odliczanieDoStrzalu < czekaj)
        {
            odliczanieDoStrzalu += Time.deltaTime;  //licznik do kolejnego strzału
        }


        if (odliczanieDoStrzalu >= czekaj)
        {
            odliczanieDoStrzalu = 0;
            GameObject pocisk;

            pocisk = Instantiate(strzalaPrefab, transform.position + transform.forward, getRotacjaPocisku());
            pocisk.GetComponent<Rigidbody>().AddForce(transform.forward * predkosc, ForceMode.Impulse);
            pocisk.GetComponent<Rigidbody>().AddForce(transform.up * 1.4f, ForceMode.Impulse);
            
        }
    }

    public Quaternion getRotacjaPocisku()    //na podstawie pozycji gracza ustala kierunek pozycji pocisku, do której ma zmierzać
    {
        pozycjaGraczaXYZ = new Vector3(Player.position.x, Player.position.y, Player.position.z);
        rotacjaPocisku = Quaternion.LookRotation(pozycjaGraczaXYZ - transform.position);
        return rotacjaPocisku;
    }
}
