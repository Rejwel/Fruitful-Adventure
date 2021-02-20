using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    private Transform gracz;
    protected GameObject graczObiekt;
    protected Vector3 hitPoint;
    public float predkoscObrotu = 6.0f;
    public bool gladkiObrot = true;
    public float predkoscRuchu = 5.0f;
    private Transform mojObiekt;
    private FollowingAndShooting ShootScript;
    private Vector3 pozycjaGraczaXYZ;

    void Start()
    {
        graczObiekt = GameObject.FindWithTag("Player");
        gracz = graczObiekt.transform;

        mojObiekt = transform;
        
    }

   

   

   
}
