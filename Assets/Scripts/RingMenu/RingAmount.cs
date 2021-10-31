using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RingAmount : MonoBehaviour
{
    private Inventory inv;
    private RingMenu ring;

    public int currentAmountTurret;
    //public int currentAmountDetectingTurret;
    public TextMeshProUGUI Amount;

    void Start()
    {
        ring = FindObjectOfType<RingMenu>();
        inv = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        /*if(ring.SetTurret() == 0)
        { 
        currentAmountTurret = (int)inv.GameObjDictionary["Turret"];
       
        Amount.text = currentAmountTurret.ToString();
        }
        Amount.text = currentAmountDetectingTurret.ToString();
         currentAmountDetectingTurret = (int)inv.GameObjDictionary["TurretDetecting"];*/
    }
}
