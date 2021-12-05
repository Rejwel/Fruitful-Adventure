using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingBullet : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float speed = 30f;
    [SerializeField] private GameObject trap;
    
    void Update()
    {
        if (_target == null) {
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
    }
    
    public void Seeking (Transform _target)
    {
        this._target = _target;
    }
    
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            Debug.Log("Trafiono wroga! Mammma Mia!");
            //EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
            Destroy(gameObject);

            GameObject itIsATrap = Instantiate(trap, transform.position, transform.rotation);
            itIsATrap.transform.localScale = new Vector3(10, 0.01f, 10);
            Destroy(itIsATrap,2.5f);
        }
    }
    
    
    
    
    
}
