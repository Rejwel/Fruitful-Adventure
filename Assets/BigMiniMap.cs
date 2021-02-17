using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMiniMap : MonoBehaviour
{
    public GameObject MiniMap;
    public  static bool MiniMapOpen = false;
    public GameObject GUI;

    private void Awake()
    {
        MiniMap.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MiniMapOpen==true)
            {
                Close();
            } else if (MiniMapOpen == false && PauseMenu.GameIsPaused == false)
            {
                Open();
            }
        }
    }


    public void Open() 
    {
        MiniMap.SetActive(true);
        GUI.SetActive(false);
        MiniMapOpen = true;
        Time.timeScale = 0f;
    }

    public void Close()
    {
        MiniMap.SetActive(false);
        GUI.SetActive(true);
        MiniMapOpen = false;
        Time.timeScale = 1f;
    }

}
