using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfoScript : MonoBehaviour
{
    public Text information;

    // Update is called once per frame
    void Update()
    {
        Vector3 informationPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        information.transform.position = informationPosition;
    }
}
