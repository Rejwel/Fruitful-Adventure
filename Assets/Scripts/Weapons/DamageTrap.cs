using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    [SerializeField] private float counter = 1f;
    [SerializeField] private float wait = 2f;
    [SerializeField] private WaveManagerSubscriber _waveManager;
    [SerializeField] private List<GameObject> _enemies;
    

    private void Awake()
    {
        _waveManager = FindObjectOfType<WaveManagerSubscriber>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemies.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        if (counter < wait)
        {
            counter += Time.deltaTime;
        }
        else if (counter >= wait)
        { 
            counter = 0f;
            foreach (var enemy in _enemies)
            {
                if (enemy != null && enemy.GetComponent<EnemyMechanics>() != null)
                {
                    enemy.GetComponent<EnemyMechanics>().TakeDamage(30);
                }
            }
        }
        foreach (var enemy in _enemies)
        {
            if (enemy != null && enemy.GetComponent<EnemyMechanics>() != null && enemy.GetComponent<EnemyMechanics>().GetHealth() <= 0)
            {
                enemy.GetComponent<Collider>().enabled = false;
                StartCoroutine(ExplodeEnemy(enemy.GetComponent<Collider>()));
                _waveManager.UpdateEnemyCounter();
                enemy.GetComponent<EnemyMechanics>().Die();
            }
        }
    }

    IEnumerator ExplodeEnemy(Collider hit)
    {
        Explosion explosion = hit.GetComponent<Explosion>();
        EnemyMechanics enemy = hit.GetComponent<EnemyMechanics>();
        Transform EnemyTransform = enemy.transform;
        hit.GetComponent<Collider>().enabled = false;
        if(explosion != null)
            explosion.explode(EnemyTransform);
        yield return null;
    }
}
