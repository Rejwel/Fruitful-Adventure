using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float czekaj = 2f;
    public float odliczanieDoStrzalu = 0f;
    public GameObject strzalaPrefab;
    public float predkosc = 5;


    private Behaviour wrogastrzal;

    private void Awake()
    {
        wrogastrzal = FindObjectOfType<Behaviour>();
    }

    public void strzal()
    {
        if(odliczanieDoStrzalu < czekaj)
        {
            odliczanieDoStrzalu += Time.deltaTime;
        }
        

        if(odliczanieDoStrzalu >=  czekaj && wrogastrzal.namierzanie() && wrogastrzal.celowanie(wrogastrzal.getZasiegWzroku()))
        {
            odliczanieDoStrzalu = 0;

            GameObject strzala;

            strzala = Instantiate(strzalaPrefab, transform.position + transform.forward, wrogastrzal.getRotacjaPocisku());
            strzala.GetComponent<Rigidbody>().AddForce(transform.forward * predkosc, ForceMode.Impulse);
            strzala.GetComponent<Rigidbody>().AddForce(transform.up * 1, ForceMode.Impulse);
        }
    }

    

}