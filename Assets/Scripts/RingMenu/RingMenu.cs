﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    public Ring Data;
    public RingCakePiece RingCakePiecePrefab;
    public float GapWidthDegree = 1f;
    public Action<string> callback;
    protected RingCakePiece[] Pieces;
    protected RingMenu Parent;
    private GroundCotroller Menu;
    private Inventory inv;
    public GameObject player;
    public string Path;

    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        
        var stepLength = 360f / Data.Elements.Length;
        var iconDist = Vector3.Distance(RingCakePiecePrefab.Icon.transform.position, RingCakePiecePrefab.CakePiece.transform.position);
        Menu = FindObjectOfType<GroundCotroller>();

        //Position it
        Pieces = new RingCakePiece[Data.Elements.Length];


        for (int i = 0; i < Data.Elements.Length; i++)
        {
            Pieces[i] = Instantiate(RingCakePiecePrefab, transform);
            Pieces[i].gameObject.AddComponent<TextMeshProUGUI>();
            //set root element
            Pieces[i].transform.localPosition = Vector3.zero;
            Pieces[i].transform.localRotation = Quaternion.identity;

            //set cake piece
            Pieces[i].CakePiece.fillAmount = 1f / Data.Elements.Length - GapWidthDegree / 360f;
            Pieces[i].CakePiece.transform.localPosition = Vector3.zero;
            Pieces[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2.0f + GapWidthDegree / 2.0f + i * stepLength);
            Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.6f);

            //set icon
            Pieces[i].Icon.transform.localPosition = Pieces[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength - (360/Data.Elements.Length), Vector3.forward) * Vector3.up * iconDist;
            Pieces[i].Icon.sprite = Data.Elements[i].Icon;      
        }
    }

    private void Update()
    {     
        var stepLength = 360f / Data.Elements.Length;
        var formula = 180f/Data.Elements.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength + formula);
        var activeElement = (int)(mouseAngle / stepLength);
        var path = Path + Data.Elements[activeElement].Name;     

        for (int i = 0; i < Data.Elements.Length; i++)
        {
            if (i == activeElement)
            {   
                switch (i)
                {
                    case 0:
                        changeState(i, Pieces, "Detecting Turret", inv.GetSlowingTurret().ToString());
                        break;
                    case 1:
                        changeState(i, Pieces, "Damage Trap", inv.GetDamageTrap().ToString());
                        break;
                    case 2:
                        changeState(i, Pieces, "Slow Trap", inv.GetSlowTrap().ToString());
                        break;
                    case 3:
                        changeState(i, Pieces, "Shooting Turret", inv.GetShootingTurret().ToString());
                        break;
                    case 4:
                        changeState(i, Pieces, "Trap Fence", inv.GetTrapFence().ToString());
                        break;
                }
                Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.7f); 
                
            }
            else
            {
                Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.4f);
            }
                
        }

        if (Input.GetMouseButtonDown(0) && Menu.Mode == GroundCotroller.ControllerMode.Menu) 
        {
            Menu.SetMode(GroundCotroller.ControllerMode.Build);
            Menu.SetMenu(true);
            Menu.SetPrefab(int.Parse(path));
            gameObject.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.Tab) && Menu.Mode == GroundCotroller.ControllerMode.Menu)
        {
            Menu.SetMode(GroundCotroller.ControllerMode.Play);
        }
                
    }

    private void changeState(int activeElement, RingCakePiece[] pieces, string name, string size)
    {
        foreach (var item in pieces)
        {
            item.GetComponent<TextMeshProUGUI>().text = "";
        }
        pieces[activeElement].GetComponent<TextMeshProUGUI>().text = name + "\n\n\t  " + size;
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;

}
