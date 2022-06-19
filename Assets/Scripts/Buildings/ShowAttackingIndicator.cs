using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAttackingIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _objectIndicator;

    public void Awake()
    {
        _objectIndicator = gameObject;
        Deactivate();
    }

    public void Deactivate()
    {
        _objectIndicator.SetActive(false);
    }
    
    public void Activate()
    {
        _objectIndicator.SetActive(true);
    }
}
