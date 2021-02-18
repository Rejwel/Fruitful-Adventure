using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMage : MonoBehaviour
{
    public float czekaj = 2f;       //Co ile mozna wykonać strzał
    public float odliczanieDoStrzalu = 0f;
    public GameObject strzalaPrefab;
    public float predkosc = 5;


    private BehaviourMage wrogastrzal;

    private void Awake()
    {
        wrogastrzal = FindObjectOfType<BehaviourMage>();
    }



    public void strzal()            //Funkcja odpowiadająca za strzał
    {
        if (odliczanieDoStrzalu < czekaj)
        {
            odliczanieDoStrzalu += Time.deltaTime;          //licznik do kolejnego strzału
        }


        if (odliczanieDoStrzalu >= czekaj && wrogastrzal.namierzanie() && wrogastrzal.celowanie(wrogastrzal.getZasiegWzroku()))
        {
            odliczanieDoStrzalu = 0;

            GameObject strzala;

            strzala = Instantiate(strzalaPrefab, transform.position + transform.forward, wrogastrzal.getRotacjaPocisku());
            strzala.GetComponent<Rigidbody>().AddForce(transform.forward * predkosc, ForceMode.Impulse);
        }
    }


}
