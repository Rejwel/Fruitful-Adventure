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
    public LayerMask whatIsPlayer;
    public bool playerInShortRange;
    public bool playerInLongRange;
    public bool playerInCenterRange;
    public float shortAttack, longAttack, centerPoint;
    
    private GameObject WhatToAttack { get; set; }

   

    private void Awake()
    {
        WhatToAttack = WaveManager.AttackingBuilding;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(HoldNavAgent());
        enemyRb = gameObject.GetComponent<Rigidbody>();
        
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        
    }
    

    void Update()
    {
        if (Player == null) Player = WaveManager.AttackingBuilding.transform;
        // 3 positions of enemy attacking player
        playerInShortRange = Physics.CheckSphere(transform.position, shortAttack, whatIsPlayer);
        playerInCenterRange = Physics.CheckSphere(transform.position, centerPoint, whatIsPlayer);
        playerInLongRange = Physics.CheckSphere(transform.position, longAttack, whatIsPlayer);

        pozycjaGraczaXYZ = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        patrzNaGracza = false;

        if (playerInShortRange || playerInCenterRange)
        {
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
        
        // transform of object set to looking destination
        transform.LookAt(Player.transform.position);
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

    public IEnumerator HoldNavAgent()
    {
        yield return new WaitForSeconds(0.5f);
        agent.enabled = true;
        Player = WhatToAttack.transform;
        follow = true;
    }
}
