using UnityEngine;
using UnityEngine.AI;

public class EnemyShootRange : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask whatIsPlayer;
    public Transform player;
    public GameObject projectile;

    //Attacking
    //private float timeBetweenAttacksShort = 2;
    //private float timeBetweenAttacksLong = 3;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float shortAttack, longAttack;
    public bool playerInShortRange;
    public bool playerInLongRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInShortRange = Physics.CheckSphere(transform.position, shortAttack, whatIsPlayer);
        playerInLongRange = Physics.CheckSphere(transform.position, longAttack, whatIsPlayer);

        if (playerInShortRange || playerInLongRange)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            if(playerInShortRange == true && playerInLongRange == true)  //short
            { 

            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 4f, ForceMode.Impulse);
            rb.AddForce(transform.up * 1f, ForceMode.Impulse);

                GetComponent<NavMeshAgent>().speed = 3;
                agent.SetDestination(transform.position);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else if(playerInShortRange == false && playerInLongRange == true)  //long
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 6f, ForceMode.Impulse);
                rb.AddForce(transform.up * 1f , ForceMode.Impulse);

                GetComponent<NavMeshAgent>().speed = 5;
                agent.SetDestination(transform.position);  //enemy stoppes when he shoots

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, longAttack);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shortAttack);
    }

}