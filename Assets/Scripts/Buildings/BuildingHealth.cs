using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class BuildingHealth : MonoBehaviour
{
    public bool buildingDestroyed { get; set; }
    public int MaxHealth;
    public int currentHealth;
    public HealthBarScript healthBar;
    public static LayerMask BuildingLayerMask;
    private WaveManager WaveManager;

    private GameObject[] Buildings;

    void Start()
    {
        BuildingLayerMask = LayerMask.NameToLayer("BuildingToAttack");
        Buildings = GetSceneObjects(BuildingLayerMask);
        WaveManager = FindObjectOfType<WaveManager>();
        buildingDestroyed = false;
        InitColliders();
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
    private GameObject [] GetSceneObjects(int layer)
    {
        return Resources.FindObjectsOfTypeAll<GameObject>()
            .Where(go => go.layer == layer).ToArray();
    }

    public void DestroyBuilding()
    {
        Buildings = GetSceneObjects(BuildingLayerMask);
        if (GetThisBuilding() == WaveManager.AttackingBuilding)
        {
            WaveManager.AttackingBuilding = null;
        }
        Destroy(GetThisBuilding());
        WaveManager.BuildingCount--;
        WaveManager.AttackingBuilding = null;
        buildingDestroyed = true;

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
    }

    private GameObject GetThisBuilding()
    {
        foreach (var building in Buildings)
        {
            if (building.GetComponent<BuildingReference>().Building.name == this.gameObject.name)
            {
                return building;
            }
        }
        return null;
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
