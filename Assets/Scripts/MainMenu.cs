using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        
       
    }

    public void QuitGame()
    {
        Debug.Log("Wychodze");
        Application.Quit();
    }
}
