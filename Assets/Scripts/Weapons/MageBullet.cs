using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBullet : MonoBehaviour
{
    private Transform firepoint;
    [SerializeField] private int mageDamage;
    private HealthPlayer givedamage;
    private EnemyRanged EnemyRanged;
    private BuildingHealth BH { get; set; }
    private DefendingDestroyable DD { get; set; }

    private void Awake()
    {
        mageDamage = 35;
        Physics.IgnoreLayerCollision(15,15);
        Physics.IgnoreLayerCollision(15, 20);
        EnemyRanged = FindObjectOfType<EnemyRanged>();
        Destroy(gameObject, 2);
        firepoint = GetComponent<Transform>();
        givedamage = FindObjectOfType<HealthPlayer>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 25 && other.gameObject.GetComponentInParent<DefendingDestroyable>() != null)
        {
            DD = other.gameObject.GetComponentInParent<DefendingDestroyable>();
            DD.TakeDamage(mageDamage);
            if (DD.GetHealth() <= 0)
            {
                DD.DestroyStructure();
            }
            Destroy(gameObject);
        }
        else if (WaveManagerSubscriber.AttackingBuilding != null && other.gameObject.layer == 19 && other.transform.parent.gameObject.Equals(WaveManagerSubscriber.AttackingBuilding))
        {
            BH = other.gameObject.GetComponentInParent<BuildingHealth>();
            BH.TakeDamage(mageDamage);
            if (BH.currentHealth <= 0 && BH.buildingDestroyed == false)
            {
                BH.DestroyBuilding();
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 13)
        {
            givedamage.TakePlayerDamage(mageDamage/2);
            Destroy(gameObject);
        }
    }
}







