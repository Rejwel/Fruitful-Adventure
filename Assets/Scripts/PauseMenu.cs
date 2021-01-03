using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject Camera;
    public GameObject CrossHair;
    public GameObject HP;
    public GameObject Money;
    public GameObject Waves;
    public GameObject help;
    public GameObject Guns;
    public GameObject skills;

    void Start()
    {
        pauseMenuUI.SetActive(false);
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
        CrossHair.SetActive(true);
        Money.SetActive(true);
        HP.SetActive(true);
        Waves.SetActive(true);
        help.SetActive(true);
        Guns.SetActive(true);
        skills.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        CrossHair.SetActive(false);
        HP.SetActive(false);
        Money.SetActive(false);
        Waves.SetActive(false);
        help.SetActive(false);
        Guns.SetActive(false);
        skills.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Wyjdz");
        Application.Quit();
    }    

}
