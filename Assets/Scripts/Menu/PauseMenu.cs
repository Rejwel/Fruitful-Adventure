using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameOver = false;
    public GameObject pauseMenuUI;
    public GameObject AreYouSureMenu;
    public GameObject AreYouSureQuit;
    public GameObject player;
    public GameObject shopMenu;
    public GameObject shopMenu2;
    public HealthPlayer Health;
    public GameObject Dead;
    public GameObject SureGameover;
    public GameObject progressText;


    public GameObject GUI;

    void Awake()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        GUI = GameObject.Find("GUI");
    }

    void Update()
    {
        DeadPlayer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused==true && GameOver==false)
            {
                Resume();
            }
            else if (GameIsPaused == false && shopMenu.active == false && shopMenu2.active == false && GameOver == false && BigMiniMap.MiniMapOpen==false)
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
        GameOver = true;
        pauseMenuUI.SetActive(false);
        AreYouSureMenu.SetActive(true);
    }

    public void SureQuit()
    {
        GameOver = true;
        pauseMenuUI.SetActive(false);
        AreYouSureQuit.SetActive(true);
    }

    public void SureQuitGameOver()
    {
        Dead.SetActive(false);
        SureGameover.SetActive(true);
    }

    public void LoadMenu()
    {
        GameOver = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Wyjdz");
        Application.Quit();
    }    

    public void NoSure()
    {
        GameOver = false;
        pauseMenuUI.SetActive(true);
        AreYouSureMenu.SetActive(false); 
        AreYouSureQuit.SetActive(false);
    }

    public void NoSureGameOver()
    {
        Dead.SetActive(true);
        SureGameover.SetActive(false);
    }

    public void DeadPlayer()
    {
        Health = FindObjectOfType<HealthPlayer>();
        if(Health.currentHealth <= 0 || WaveManager.BuildingCount == 0)
        {
            GUI.SetActive(false);
            Dead.SetActive(true);
            GameOver = true;
            Health.currentHealth = 1;
            Time.timeScale = 0f;
            GameIsPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<PlayerShoot>().enabled = false;
        }
    }

}
