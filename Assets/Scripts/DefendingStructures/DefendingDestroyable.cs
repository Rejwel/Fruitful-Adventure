using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendingDestroyable : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBarScript healthBar;
    [SerializeField] private HealthBarScript healthBarMap;
    [SerializeField] private GameObject brokenStructure;
    [SerializeField] private Inventory playerInventory;


    private void Awake()
    {
        playerInventory = FindObjectOfType<Inventory>();
    }

    void Start()
    {
        InitColliders();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBarMap.SetMaxHealth(maxHealth);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthBarMap.SetHealth(currentHealth);
    }
    
    public int GetHealth()
    {
        return currentHealth;
    }

    public void DestroyStructure()
    {
        Destroy(gameObject);
        playerInventory.defendingStructures.Remove(gameObject);
        Instantiate(brokenStructure, transform.position, transform.rotation);
    }
    
    private void InitColliders()
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
            if(childrenList[i].GetComponent<MeshCollider>() != null)
                childrenList[i].GetComponent<MeshCollider>().convex = false;
        }
    }
}
