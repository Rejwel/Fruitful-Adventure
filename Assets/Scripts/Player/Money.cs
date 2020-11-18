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
        CurrentMoney += 5;
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
