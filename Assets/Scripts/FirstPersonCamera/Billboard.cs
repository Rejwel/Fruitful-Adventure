using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Billboard : MonoBehaviour
{
    private Transform Player;

    private void Start()
    {
        Player = FindObjectOfType<HealthPlayer>().transform;
    }

    void LateUpdate()
    {
        transform.LookAt(Player);
        transform.Rotate(0, 180, 0);
    }
}
