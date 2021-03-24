using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    List<GameObject> childrenList = new List<GameObject>();

    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        
        
        foreach (Transform child in children)
        {
            if (child.tag.Equals("Grenade"))
                childrenList.Add(child.gameObject);
        }
        for (int i = 0; i < childrenList.Count; i++)
        {
            childrenList[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, Random.Range(0f, 100f),0)*4f);
        }
    }
    
}
