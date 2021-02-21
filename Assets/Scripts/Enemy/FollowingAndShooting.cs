using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public class FollowingAndShooting : MonoBehaviour
{
    //Following 
    public NavMeshAgent agent;
    public Transform Player;
    private Rigidbody enemyRb;
    private bool follow = false;
    private Quaternion rotation;
    
    //Shooting
    public float czekaj = 2f;
    public float odliczanieDoStrzalu = 0f;
    public GameObject strzalaPrefab;
    public float predkosc = 7;
    public bool patrzNaGracza = false;
    private Quaternion rotacjaPocisku;

    //Wild Moooves 
    private Vector3 pozycjaGraczaXYZ;  
    public float katWidzenia = 160f;
    public float predkoscObrotu = 6.0f;
    public bool gladkiObrot = true;
    public float predkoscRuchu = 5.0f;
    private Transform mojObiekt;
    private EnemyCamera ScriptCamera;

    //Distance from Player
    public LayerMask whatIsPlayer;
    public bool playerInShortRange;
    public bool playerInLongRange;
    public bool playerInCenterRange;
    public float shortAttack, longAttack, centerPoint;

   

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //Following
        ScriptCamera = FindObjectOfType<EnemyCamera>();
        StartCoroutine(HoldNavAgent());

        enemyRb = gameObject.GetComponent<Rigidbody>();
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").transform;
        }

        mojObiekt = transform;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        
    }
    

    void Update()
    {
        // 3 positions of enemy attacking player
        playerInShortRange = Physics.CheckSphere(transform.position, shortAttack, whatIsPlayer);
        playerInCenterRange = Physics.CheckSphere(transform.position, centerPoint, whatIsPlayer);
        playerInLongRange = Physics.CheckSphere(transform.position, longAttack, whatIsPlayer);

        if (playerInShortRange || playerInCenterRange)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;  //when player is close he moves back
            Vector3 newPos = transform.position + dirToPlayer;
            agent.SetDestination(newPos);
            
            // shoot if possible
            strzal();
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
        transform.LookAt(Player);
    }


    //Shooting Part
    public void strzal()    //Funkcja odpowiadająca za strzał
    {
        if (odliczanieDoStrzalu < czekaj)
        {
            odliczanieDoStrzalu += Time.deltaTime;  //licznik do kolejnego strzału
        }


        if (odliczanieDoStrzalu >= czekaj && namierzanie())
        {
            odliczanieDoStrzalu = 0;

            GameObject pocisk;

            pocisk = Instantiate(strzalaPrefab, transform.position + transform.forward, getRotacjaPocisku());
            pocisk.GetComponent<Rigidbody>().AddForce(transform.forward * predkosc, ForceMode.Impulse);
            pocisk.GetComponent<Rigidbody>().AddForce(transform.up * 1.4f, ForceMode.Impulse);
        }
    }

    public bool namierzanie()       //pobranie aktualnego kąta obrotu wroga w stosunku do gracza
    {
        float angle = Quaternion.Angle(Player.rotation, transform.rotation);
        if (angle >= katWidzenia)    //czy gracz jest widoczny
        {
            return true;
        }
        return false;
    }

    public Quaternion getRotacjaPocisku()    //na podstawie pozycji gracza ustala kierunek pozycji pocisku, do której ma zmierzać
    {
        pozycjaGraczaXYZ = new Vector3(Player.position.x, Player.position.y, Player.position.z);
        rotacjaPocisku = Quaternion.LookRotation(pozycjaGraczaXYZ - transform.position);
        return rotacjaPocisku;
    }

    private void wykonajAtak()
    {
           strzal();       
    }

    private void patrzNaMnie()      //Obrót samego przeciwnika w stronę gracza
    {
        if (gladkiObrot && patrzNaGracza == true)
        {
            Quaternion rotation = Quaternion.LookRotation(pozycjaGraczaXYZ - mojObiekt.position);
            mojObiekt.rotation = Quaternion.Slerp(mojObiekt.rotation, rotation, Time.deltaTime * predkoscObrotu);
            obrotGlowy();
        }
        else if (!gladkiObrot && patrzNaGracza == true)
        {
            transform.LookAt(pozycjaGraczaXYZ);     //Błyskawiczny obrót wroga
        }
    }


    public void obrotGlowy()   //Obracamy głowę wroga w stronę gracza
    {
        Transform glowa = transform.Find("Head");

        if (glowa != null)
        {
            Vector3 graczXYZ = new Vector3(Player.position.x, Player.position.y, Player.position.z);
            Quaternion wStroneGracza = Quaternion.LookRotation(graczXYZ - glowa.position);
            glowa.rotation = Quaternion.Slerp(glowa.rotation, wStroneGracza, Time.deltaTime * predkoscObrotu);
        }

    }

    public bool celowanie(float zasieg)     //daje informacje czy przeciwnik na nas patrzy
    {
        Transform glowa = transform.Find("Head");
        Ray ray = new Ray(glowa.position, glowa.forward);   //pobiera promień w jakim kierunku patrzy przeciwnik
        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, zasieg))       //Sprawdza czy promień w coś trafił
        {
            GameObject go = hitinfo.collider.gameObject;

            if (go.name.Equals(Player.name))
            {
                return true;
            }
        }
        return false;
    }


    //Following Part
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.CompareTag("Bullet") || other.collider.CompareTag("Enemy"))
    //     {
    //         Invoke("StopMoving", 0.2f);
    //     }
    // }

    public void StopMoving()
    {
        enemyRb.velocity = Vector3.zero;
        agent.SetDestination(Player.position);
    }

    public IEnumerator HoldNavAgent()
    {
        yield return new WaitForSeconds(0.5f);
        agent.enabled = true;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        follow = true;
    }
}
