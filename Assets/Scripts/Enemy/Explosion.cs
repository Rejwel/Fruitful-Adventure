using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Explosion : MonoBehaviour
{
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;
    public float explosionRadius = 4f;
    public float explosionForce = 100f;
    public float explosionUpward = 0.2f;

    void Start()
    {
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    public void explode(GameObject enemy)
    {

        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z, enemy);
                }
            }
        }
        
        Vector3 explosionPos = enemy.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, enemy.transform.position, explosionRadius, explosionUpward);
            }
        }
    }

    public void createPiece(int x, int y, int z, GameObject enemy)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        piece.transform.position = enemy.transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
        piece.gameObject.layer = 11;
        piece.AddComponent<Rigidbody>();
        piece.AddComponent<MoneyDisappear>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
}
