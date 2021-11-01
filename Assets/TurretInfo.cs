using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfo : MonoBehaviour
{
    public GameObject TurretCanvas;
    public bool isOpen { get; set; }

    public void Awake()
    {
        TurretCanvas.SetActive(false);
        isOpen = false;
    }

    public void OpenCanvas()
    {
        TurretCanvas.SetActive(true);
        isOpen = true;
    }

    public void CloseCanvas()
    {
        TurretCanvas.SetActive(false);
        isOpen = false;
    }

    public bool issOpen()
    {
        return isOpen;
    }

}
