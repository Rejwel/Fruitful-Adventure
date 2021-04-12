using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Explosion : MonoBehaviour
{
    public float cubeSize = 0.18f;
    public int cubesInRow = 2;
    private EnemyMechanics EM;
    private GameObject SugarCube;

    float cubesPivotDistance;
    Vector3 cubesPivot;
    public float explosionRadius = 5f;
    public float explosionForce = 50f;
    public float explosionUpward = 3f;

    void Start()
    {
        EM = FindObjectOfType<EnemyMechanics>();
        SugarCube = EM.Money;
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    public void explode(Transform EnemyTransform)
    {

        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z, EnemyTransform);
                }
            }
        }
        
        Vector3 explosionPos = EnemyTransform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, EnemyTransform.position, explosionRadius, explosionUpward);
            }
        }
    }

    public void createPiece(int x, int y, int z, Transform EnemyTransform)
    {
        Instantiate(SugarCube, EnemyTransform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot, transform.rotation);
    }
}
