using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowCamera : MonoBehaviour
{
    public float predkoscObrotu = 6.0f;
    public bool gladkiObrot = true;
    public float predkoscRuchu = 5.0f;
    public float zasiegWzroku = 30f;
    public float odstepGracza = 2f;

    private Transform mojObiekt;
    private Transform gracz;
    private bool patrzNaGracza = false;
    private Vector3 pozycjaGraczaXYZ;

    private Shoot strzalLuk;

    public NavMeshAgent agent;
    public LayerMask whatIsPlayer;
    public bool playerInShortRange;
    public bool playerInLongRange;
    public Transform Player;
    public float shortAttack, longAttack;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        mojObiekt = transform;

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        strzalLuk = (Shoot)gameObject.GetComponent<Shoot>();
    }

    
    void Update()
    {

        playerInShortRange = Physics.CheckSphere(transform.position, shortAttack, whatIsPlayer);
        playerInLongRange = Physics.CheckSphere(transform.position, longAttack, whatIsPlayer);

        if (playerInShortRange || playerInLongRange)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;  //when player is close he moves back
            Vector3 newPos = transform.position + dirToPlayer;
            agent.SetDestination(newPos);

            GetComponent<NavMeshAgent>().speed = 3;
        }

        gracz = GameObject.FindWithTag("Player").transform;

        pozycjaGraczaXYZ = new Vector3(gracz.position.x, mojObiekt.position.y, gracz.position.z);

        float dist = Vector3.Distance(mojObiekt.position, gracz.position);

        patrzNaGracza = false;

        if(dist <= zasiegWzroku && dist > odstepGracza)
        {
            patrzNaGracza = true;

            mojObiekt.position = Vector3.MoveTowards(mojObiekt.position, pozycjaGraczaXYZ, predkoscRuchu * Time.deltaTime);
            wykonajAtak();
        }
        else if(dist <= odstepGracza)
        {
            patrzNaGracza = true;
            wykonajAtak();
        }
    }

    private void wykonajAtak()
    {
        if(strzalLuk != null)
        { 
        strzalLuk.strzal();
        }
    }

    private void obrotGlowy()
    {
        Transform glowa = transform.Find("Head");

        if(glowa != null)
        {
            Vector3 graczXYZ = new Vector3(gracz.position.x, gracz.position.y, gracz.position.z);
            Quaternion wStroneGracza = Quaternion.LookRotation(graczXYZ - glowa.position);
            glowa.rotation = Quaternion.Slerp(glowa.rotation, wStroneGracza, Time.deltaTime * predkoscObrotu);
        }
        
    }

    private void patrzNaMnie()
    {
        if(gladkiObrot && patrzNaGracza == true)
        {
            Quaternion rotation = Quaternion.LookRotation(pozycjaGraczaXYZ - mojObiekt.position);
            mojObiekt.rotation = Quaternion.Slerp(mojObiekt.rotation, rotation, Time.deltaTime * predkoscObrotu);
            obrotGlowy();
        }
        else if (!gladkiObrot && patrzNaGracza == true)
        {
            transform.LookAt(pozycjaGraczaXYZ);
        }
    }

}
