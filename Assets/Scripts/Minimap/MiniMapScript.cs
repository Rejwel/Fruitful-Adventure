using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    private Vector3 _defaultTransform;
    private Vector3 _defaultScale;

    private void Start()
    {
        _defaultTransform = transform.localPosition;
        _defaultScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            transform.localPosition = _defaultTransform + new Vector3(80,-80,0);
            transform.localScale = new Vector3(3.2f, 3.2f, 1);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            transform.localPosition = _defaultTransform;
            transform.localScale = _defaultScale;
        }
        
    }
}
