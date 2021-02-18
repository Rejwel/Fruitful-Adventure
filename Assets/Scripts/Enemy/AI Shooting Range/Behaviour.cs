using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Behaviour : MonoBehaviour
{
    private Transform gracz;
    protected GameObject graczObiekt;
    private Vector3 pozycjaGraczaXYZ;
    private Quaternion rotacjaPocisku;

    public float katWidzenia = 160f;

    protected Vector3 hitPoint;



    // Start is called before the first frame update
    void Start()
    {
        graczObiekt = GameObject.FindWithTag("Player");
        gracz = graczObiekt.transform;
    }

    public bool namierzanie()       //pobranie aktualnego kąta obrotu wroga w stosunku do gracza
    {
        float angle = Quaternion.Angle(gracz.rotation, transform.rotation);
        if(angle >= katWidzenia)    //czy gracz jest widoczny
        {
            return true;
        }
        return false;
    }

    public Quaternion getRotacjaPocisku()    //na podstawie pozycji gracza ustala kierunek pozycji pocisku, do której ma zmierzać
    {
        pozycjaGraczaXYZ = new Vector3(gracz.position.x, gracz.position.y, gracz.position.z);
        rotacjaPocisku = Quaternion.LookRotation(pozycjaGraczaXYZ - transform.position);
        return rotacjaPocisku;
    }

    public bool celowanie(float zasieg)     //daje informacje czy przeciwnik na nas patrzy
    {
        Transform glowa = transform.Find("Head");
        Ray ray = new Ray(glowa.position, glowa.forward);   //pobiera promień w jakim kierunku patrzy przeciwnik
        RaycastHit hitinfo;

        if(Physics.Raycast(ray, out hitinfo, zasieg))       //Sprawdza czy promień w coś trafił
        {
            GameObject go = hitinfo.collider.gameObject;

            if(go.name.Equals(graczObiekt.name))
            {
                return true;
            }
        }
        return false;
    }

    public float getZasiegWzroku()
    {
        FollowCamera ai = (FollowCamera)GetComponent<FollowCamera>();
        if(ai != null)
        {
            //return ai.zasiegWzroku;
        }
        return 0;
    }
}
