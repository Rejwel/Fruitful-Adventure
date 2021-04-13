using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretInfoScript : MonoBehaviour
{
    public TextMeshProUGUI informationOfAmmo;

    public void Awake()
    {
        informationOfAmmo = GameObject.Find("TurretAmmo").GetComponent<TextMeshProUGUI>();
    }

    public void DisplayInfoAmmo(string curr,string max)
    {
        informationOfAmmo.color = Color.white;
        informationOfAmmo.fontSize = 8;
        informationOfAmmo.text = curr + "/" + max;
    }

    public void DisplayWarning()
    {
        informationOfAmmo.color = Color.red;
        informationOfAmmo.fontSize = 24;
        informationOfAmmo.text = "!";
    }
}
