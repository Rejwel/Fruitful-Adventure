
using UnityEngine;
using UnityEngine.AI;

public class EnemyShootMage : MonoBehaviour
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

    public void AttackPlayer()
    {
        

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here

            if (playerInShortRange == true && playerInLongRange == true)  //short
            {
                agent.SetDestination(transform.position);
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 6.5f, ForceMode.Impulse);   //the power of the shot
                GetComponent<NavMeshAgent>().speed = 4;     //speed when we are in short distance

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacksShort);
            }

            if (playerInShortRange == false && playerInLongRange == true)  //long
            {
                agent.SetDestination(transform.position);
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 7.5f, ForceMode.Impulse);  //the power of the shot
                GetComponent<NavMeshAgent>().speed = 5;  //speed when we are in long distance

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacksLong);
            }

            if (playerInShortRange == false && playerInLongRange == false)  //idle
            {
                GetComponent<NavMeshAgent>().speed = 6;    //speed when we are in really long distance
            }
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    




}

