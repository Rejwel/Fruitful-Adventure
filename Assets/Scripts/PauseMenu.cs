using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject AreYouSureMenu;
    public GameObject AreYouSureQuit;
    public GameObject player;

    public GameObject GUI;

    void Awake()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        GUI = GameObject.Find("GUI");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);

        GUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<PlayerShoot>().enabled = true;

    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);

        GUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<PlayerShoot>().enabled = false;
    }

    public void SureMenu()
    {
        pauseMenuUI.SetActive(false);
        AreYouSureMenu.SetActive(true);
    }

    public void SureQuit()
    {
        pauseMenuUI.SetActive(false);
        AreYouSureQuit.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Wyjdz");
        Application.Quit();
    }    

    public void NoSure()
    {
        pauseMenuUI.SetActive(true);
        AreYouSureMenu.SetActive(false); 
        AreYouSureQuit.SetActive(false);
    }

}
