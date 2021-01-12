using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBullet : MonoBehaviour
{
    private Transform firepoint;
    private HealthPlayer givedamage;
   

    void Start()
    {
        Destroy(gameObject, 2);
        
    }

    private void Update()
    {
        
    }

    private void Awake()
    {
        firepoint = GetComponent<Transform>();
        givedamage = FindObjectOfType<HealthPlayer>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Physics.IgnoreCollision(other.collider, this.GetComponent<Collider>());
           

        }
        if (other.transform.gameObject.tag.Equals("Player"))
        {
            givedamage.TakePlayerDamage(35);
            Destroy(gameObject);
        }

        
    }

    


}







