using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaControl : MonoBehaviour
{
    public GameObject WarningCanvas;
    void Awake()
    {
        WarningCanvas.SetActive(false);
    }

    public void OpenCanvas()
    {
        WarningCanvas.SetActive(true);
    }
    
    public void CloseCanvas()
    {
        WarningCanvas.SetActive(false);
    }

    public void DefaultWarning()
    {
        WarningCanvas.GetComponent<TextMeshProUGUI>().text = "Wykryto wroga obok lasu!";
    }
    
    public void BuildingWarning(string building)
    {
        WarningCanvas.GetComponent<TextMeshProUGUI>().text = "Wykryto wroga obok " + building;
    }


}
