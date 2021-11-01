using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public delegate void GameSetup();
    public event GameSetup OnStartSetup;
    public event GameSetup TurnOff;
    public event GameSetup MainLogic;

    void Start()
    {
        OnStartSetup?.Invoke();
    }
    private void Update()
    {
        TurnOff?.Invoke();
        MainLogic?.Invoke();
    }
}