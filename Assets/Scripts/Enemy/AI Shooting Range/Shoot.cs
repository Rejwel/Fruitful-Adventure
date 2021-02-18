using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float czekaj = 2f;
    public float odliczanieDoStrzalu = 0f;
    public GameObject strzalaPrefab;
    public float predkosc = 7;


    private Behaviour wrogastrzal;

    private void Awake()
    {
        wrogastrzal = FindObjectOfType<Behaviour>();
    }

    public void strzal()    //Funkcja odpowiadająca za strzał
    {
        if(odliczanieDoStrzalu < czekaj)
        {
            odliczanieDoStrzalu += Time.deltaTime;  //licznik do kolejnego strzału
        }
        

        if(odliczanieDoStrzalu >=  czekaj && wrogastrzal.namierzanie() )
        {
            odliczanieDoStrzalu = 0;

            GameObject strzala;

            strzala = Instantiate(strzalaPrefab, transform.position + transform.forward, wrogastrzal.getRotacjaPocisku());
            strzala.GetComponent<Rigidbody>().AddForce(transform.forward * predkosc, ForceMode.Impulse);
            strzala.GetComponent<Rigidbody>().AddForce(transform.up * 1.4f, ForceMode.Impulse);
        }
    }

    

}