using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 50f;
    [SerializeField] private float _jumpRange = 60f;
    [SerializeField] private float _jumpSpeed = 3f;
    
    private Transform _self;

    void Start()
    {
        _self = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (Time.timeScale != 0)
            RotateObject(_self);
    }

    private void RotateObject(Transform objectTransform)
    {
        objectTransform.SetPositionAndRotation(
            new Vector3(objectTransform.position.x, objectTransform.position.y + ((float)Math.Sin(Time.time * _jumpSpeed) * _jumpRange) / 1000, objectTransform.position.z), 
            Quaternion.Euler(Vector3.up * _rotateSpeed * Time.time));
    }
}
