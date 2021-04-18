﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int CurrentMoney;
    public Text Score;

    public void AddMoney()
    {
        CurrentMoney += 3; 
        Score.text = CurrentMoney.ToString();
    }
    
    public void AddMoneyAmmount(int money)
    {
        CurrentMoney += money; 
        Score.text = CurrentMoney.ToString();
    }

    public void RemoveMoney(int value)
    {
        CurrentMoney -= value;
        Score.text = CurrentMoney.ToString();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
