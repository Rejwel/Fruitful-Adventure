using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    public Ring Data;
    public RingCakePiece RingCakePiecePrefab;
    public float GapWidthDegree = 1f;
    public Action<string> callback;
    protected RingCakePiece[] Pieces;
    protected RingMenu Parent;
    [HideInInspector]
    public string Path;


    void Start()
    {
        var stepLength = 360f / Data.Elements.Length;
        var iconDist = Vector3.Distance(RingCakePiecePrefab.Icon.transform.position, RingCakePiecePrefab.CakePiece.transform.position);

        //Position it
        Pieces = new RingCakePiece[Data.Elements.Length];

        for (int i = 0; i < Data.Elements.Length; i++)
        {
            Pieces[i] = Instantiate(RingCakePiecePrefab, transform);
            //set root element
            Pieces[i].transform.localPosition = Vector3.zero;
            Pieces[i].transform.localRotation = Quaternion.identity;

            //set cake piece
            Pieces[i].CakePiece.fillAmount = 1f / Data.Elements.Length - GapWidthDegree / 360f;
            Pieces[i].CakePiece.transform.localPosition = Vector3.zero;
            Pieces[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2.0f + GapWidthDegree / 2.0f + i * stepLength);
            Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.5f);

            //set icon
            Pieces[i].Icon.transform.localPosition = Pieces[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            Pieces[i].Icon.sprite = Data.Elements[i].Icon;

        }
    }

    private void Update()
    {
        var stepLength = 360f / Data.Elements.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength / 2f);
        var activeElement = (int)(mouseAngle / stepLength);
        for (int i = 0; i < Data.Elements.Length; i++)
        {
            if (i == activeElement)
                Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.75f);
            else
                Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.5f);
        }


        if (Input.GetMouseButtonDown(0))
        {
            var path = Path + Data.Elements[activeElement].Name;

            callback?.Invoke(path);

            gameObject.SetActive(false);
        }
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;
}

