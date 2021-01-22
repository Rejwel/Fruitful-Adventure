using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColliderCheck : MonoBehaviour
{
    List<GameObject> childrenList = new List<GameObject>();
    private void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        
        for(int i = 0; i < children.Length; i++) {
            Transform child = children[i];
            if(child != transform && !child.CompareTag("Healthbar")) {
                childrenList.Add(child.gameObject);
            }
        }
        
        for(int i = 0; i < childrenList.Count; i++) {
            childrenList[i].AddComponent<DamageBuilding>();
        }
        
    }
}
