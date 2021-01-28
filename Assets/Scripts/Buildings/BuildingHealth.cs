using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    public bool buildingDestroyed = false;
    public int MaxHealth;
    public int currentHealth;
    public HealthBarScript healthBar;
    
    void Start()
    {
        InitColliders();
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void DestroyBuilding()
    {
        List<GameObject> childrenList = new List<GameObject>();
            Transform[] children = GetComponentsInChildren<Transform>();
            for(int i = 0; i < children.Length; i++) {
                Transform child = children[i];
                if(child != transform && !child.CompareTag("Healthbar")) {
                    childrenList.Add(child.gameObject);
                }
                else if(child.CompareTag("Healthbar"))
                {
                    Destroy(children[i].gameObject);
                }
            }
            for(int i = 0; i < childrenList.Count; i++)
            {
                childrenList[i].AddComponent<DestroyAfter5Sec>();
                childrenList[i].GetComponent<MeshCollider>().convex = true;
                childrenList[i].isStatic = false;
                childrenList[i].AddComponent<Rigidbody>().AddForce(Vector3.forward*10f);
            }
        buildingDestroyed = true;
    }

    public void InitColliders()
    {
        List<GameObject> childrenList = new List<GameObject>();
        Transform[] children = GetComponentsInChildren<Transform>();
        for(int i = 0; i < children.Length; i++) {
            Transform child = children[i];
            if(child != transform && !child.CompareTag("Healthbar")) {
                childrenList.Add(child.gameObject);
            }
        }
        for(int i = 0; i < childrenList.Count; i++)
        {
            childrenList[i].GetComponent<MeshCollider>().convex = false;
        }
    }
}
