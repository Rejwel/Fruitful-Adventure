using System;
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

    [HideInInspector]
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
            Pieces[i].Icon.transform.localPosition = Pieces[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            Pieces[i].Icon.sprite = Data.Elements[i].Icon;      
        }
    }

    private void Update()
    {     
        var stepLength = 360f / Data.Elements.Length;       //How many degrees does CakePiece take 
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + (stepLength + 175f) / 2f);  //Counts at what angle the cursor is 
        var activeElement = (int)(mouseAngle / stepLength);
        var path = Path + Data.Elements[activeElement].Name;  //path, so far it`s 0 (Turret) or 1 (Detecting Turret)        


        for (int i = 0; i < Data.Elements.Length; i++)
        {
            if (i == activeElement)
            {   
                switch (i)
                {
                    case 0:
                        changeState(i, Pieces, "Detecting Turret", inv.GetDetectingTurret().ToString());
                        break;
                    case 1:
                        changeState(i, Pieces, "Shooting Turret", inv.GetShootingTurret().ToString());
                        break;
                    case 2:
                        changeState(i, Pieces, "Detecting Turret", inv.GetDetectingTurret().ToString());
                        break;
                    case 3:
                        changeState(i, Pieces, "Shooting Turret", inv.GetShootingTurret().ToString());
                        break;
                }
                Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.7f); 
                
            }
            else
            {
                Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.4f);
            }
                
        }

        if (Input.GetMouseButtonDown(0) && Menu.Mode == GroundCotroller.ControllerMode.Menu) //We are in the menu and we clicked LPM 
        {
            Menu.SetMode(GroundCotroller.ControllerMode.Build);
            Menu.SetMenu(true);
            Menu.SetPrefab(int.Parse(path));

            if (path == "0" && inv.GameObjDictionary["Turret"] == 0)
            {
                Menu.SetMode(GroundCotroller.ControllerMode.Play);
            }
            else if (path == "1" && inv.GameObjDictionary["TurretDetecting"] == 0)
            {
                Menu.SetMode(GroundCotroller.ControllerMode.Play);
            }
            else
            {
                Menu.SetMode(GroundCotroller.ControllerMode.Build);
            }
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
