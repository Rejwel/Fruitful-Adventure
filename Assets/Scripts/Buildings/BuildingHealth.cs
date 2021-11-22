using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class BuildingHealth : MonoBehaviour
{
    public bool buildingDestroyed { get; set; }
    public int MaxHealth;
    public int currentHealth;
    public HealthBarScript healthBar;
    [SerializeField] private HealthBarScript _healthBarMap;
    public static LayerMask BuildingLayerMask;
    private WaveManagerSubscriber WaveManager;

    private GameObject[] Buildings;
    public GameObject BrokenBuilding;

    void Start()
    {
        BuildingLayerMask = LayerMask.NameToLayer("BuildingToAttack");
        Buildings = GetSceneObjects(BuildingLayerMask);
        WaveManager = FindObjectOfType<WaveManagerSubscriber>();
        buildingDestroyed = false;
        InitColliders();
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        _healthBarMap.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        _healthBarMap.SetHealth(currentHealth);
    }
    
    private GameObject [] GetSceneObjects(int layer)
    {
        return Resources.FindObjectsOfTypeAll<GameObject>()
            .Where(go => go.layer == layer).ToArray();
    }

    public void DestroyBuilding()
    {
        Buildings = GetSceneObjects(BuildingLayerMask);
        if (GetThisBuilding() == WaveManagerSubscriber.AttackingBuilding)
        {
            WaveManagerSubscriber.AttackingBuilding = null;
        }
        Destroy(GetThisBuilding());
        WaveManager.BuildingCount--;
        WaveManagerSubscriber.AttackingBuilding = null;
        buildingDestroyed = true;

        Destroy(this.gameObject);
        Instantiate(BrokenBuilding, transform.position, transform.rotation);
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
            if(childrenList[i].GetComponent<MeshCollider>() != null)
                childrenList[i].GetComponent<MeshCollider>().convex = false;
        }
    }
}
