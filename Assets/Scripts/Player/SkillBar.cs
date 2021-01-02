using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public Image dash;
    public Image shield;
    public Image dJump;

    public GameObject shieldProp;

    private Inventory inv;

    private void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (inv.CanDash())
            dash.color = Color.white;
        else
            dash.color = Color.gray;
        
        if (inv.CanDoubleJump())
            dJump.color = Color.white;
        else
            dJump.color = Color.gray;
        
        if (inv.isShielded())
        {
            shield.color = Color.white;
            shieldProp.SetActive(true);
        }
        else
            shield.color = Color.gray;
    }
}
