using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBullet : MonoBehaviour
{
    private Transform firepoint;
    private HealthPlayer givedamage;
    private BuildingHealth BH { get; set; }
   

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
        if (other.gameObject.layer == 19)
        {
            BH = other.gameObject.GetComponentInParent<BuildingHealth>();
            BH.TakeDamage(35);
            if (BH.currentHealth <= 0)
            {
                BH.DestroyBuilding();
            }
        }
        Destroy(gameObject);
    }
}







