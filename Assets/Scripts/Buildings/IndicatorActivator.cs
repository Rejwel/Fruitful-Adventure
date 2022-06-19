using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorActivator : MonoBehaviour
{

    [SerializeField] private GameObject _indicator;


    public void ActivateIndicator()
    {
        _indicator.GetComponent<ShowAttackingIndicator>().Activate();
    }
    
    public void DeactivateIndicator()
    {
        _indicator.GetComponent<ShowAttackingIndicator>().Deactivate();
    }
}
