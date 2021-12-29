using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int CurrentMoney;
    [SerializeField] private int earnedMoney;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI earnedMoneyText;

    public void AddMoney()
    {
        CurrentMoney += 3;
        earnedMoney += 3;
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

    public int GetEarnedMoney()
    {
        earnedMoneyText.text = "Earned money: " + earnedMoney.ToString();
        return earnedMoney;
    }

}
