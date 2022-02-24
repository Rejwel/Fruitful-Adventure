using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume(); 
            }
            else if (!GameIsPaused)
            {
                
            }
           
        }
        
    }

    public void Resume()
    {
        Debug.Log("RESUMEE");
    }

    public void Exit()
    {
        
    }
    
    
    
    
}
