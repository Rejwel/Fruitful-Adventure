using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBullet : MonoBehaviour
{
    private Transform firepoint;
    private HealthPlayer givedamage;
    private EnemyRanged EnemyRanged;
    private BuildingHealth BH { get; set; }

    private void Awake()
    {
        Physics.IgnoreLayerCollision(15,15);
        Physics.IgnoreLayerCollision(15, 20);
        EnemyRanged = FindObjectOfType<EnemyRanged>();
        Destroy(gameObject, 2);
        firepoint = GetComponent<Transform>();
        givedamage = FindObjectOfType<HealthPlayer>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (WaveManager.AttackingBuilding != null && other.gameObject.layer == 19 && other.transform.parent.gameObject.Equals(WaveManager.AttackingBuilding.GetComponent<BuildingReference>().Building))
        {
            BH = other.gameObject.GetComponentInParent<BuildingHealth>();
            BH.TakeDamage(35);
            if (BH.currentHealth <= 0 && BH.buildingDestroyed == false)
            {
                BH.DestroyBuilding();
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 13)
        {
            givedamage.TakePlayerDamage(35/2);
            Destroy(gameObject);
        }
    }
}







