using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0f, 1f, 0f));
    }
}
