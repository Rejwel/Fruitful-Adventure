
using UnityEngine;
using UnityEngine.AI;

public class EnemyShootRange : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsPlayer;


    //Attacking
    private float timeBetweenAttacksShort = 2;
    private float timeBetweenAttacksLong = 3;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float shortAttack, longAttack;
    public bool playerInShortRange, playerInLongRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInShortRange = Physics.CheckSphere(transform.position, shortAttack, whatIsPlayer);
        playerInLongRange = Physics.CheckSphere(transform.position, longAttack, whatIsPlayer);


        if (playerInLongRange || playerInShortRange) AttackPlayer();
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here

            if (playerInShortRange == true && playerInLongRange == true)  //short
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 5f, ForceMode.Impulse);
                rb.AddForce(transform.up * 1.2f, ForceMode.Impulse);
                GetComponent<NavMeshAgent>().speed = 2;

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacksShort);
            }

            if (playerInShortRange == false && playerInLongRange == true)   //long
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 7f, ForceMode.Impulse);
                rb.AddForce(transform.up * 1.6f, ForceMode.Impulse);
                GetComponent<NavMeshAgent>().speed = 4;

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacksLong);
            }

            if (playerInShortRange == false && playerInLongRange == false)  //idle
            {
                GetComponent<NavMeshAgent>().speed = 6;
            }


        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }





}

