using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int CurrentMoney;
    public Text text;

    public void AddMoney()
    {
        CurrentMoney += 5;
        Debug.Log(CurrentMoney);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
