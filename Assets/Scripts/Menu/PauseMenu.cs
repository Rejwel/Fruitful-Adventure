using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    private static bool _gameOver;
    private bool _isSettingsMenu;
    private bool _isInBuildMode;
    
    private WaveManagerSubscriber _waveManager;
    private Money _money;
    private GroundCotroller _groundCotroller;
    
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject areYouSureMenu;
    [SerializeField] private GameObject areYouSureQuit;
    [SerializeField] private GameObject areYouSureQuitMainMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject shopMenu2;
    [SerializeField] private HealthPlayer health;
    [SerializeField] private GameObject dead;
    [SerializeField] private GameObject sureGameover;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject GUI;

    void Awake()
    {
        _waveManager = FindObjectOfType<WaveManagerSubscriber>();
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        GUI = GameObject.Find("GUI");
        _money = FindObjectOfType<Money>();
        _groundCotroller = FindObjectOfType<GroundCotroller>();
    }

    void Update()
    {
        DeadPlayer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == true && _gameOver == false && !_isInBuildMode)
            {
                Resume();
            }
            else if (gameIsPaused == false && shopMenu.activeSelf == false && shopMenu2.activeSelf == false &&
                     _gameOver == false && BigMiniMap.MiniMapOpen == false && !_isInBuildMode)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<PlayerShoot>().HoldFire = false;
    }

    public void Pause()
    {
        player.GetComponent<PlayerShoot>().HoldFire = true;
        pauseMenuUI.SetActive(true);
        GUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void End()
    {
        player.GetComponent<PlayerShoot>().HoldFire = true;
        GUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SureMenu()
    {
        _gameOver = true;
        pauseMenuUI.SetActive(false);
        areYouSureMenu.SetActive(true);
    }

    public void InGameSettingsMenu()
    {
        _gameOver = true;
        _isSettingsMenu = true;
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OutInGameSettingsMenu()
    {
        _gameOver = false;
        _isSettingsMenu = false;
        pauseMenuUI.SetActive(true);
        settingsMenu.SetActive(false);
    }
    

    public void SureQuitDesktop()
    {
        _gameOver = true;
        pauseMenuUI.SetActive(false);
        areYouSureQuit.SetActive(true);
    }
    
    public void SureQuitMainMenu()
    {
        _gameOver = true;
        pauseMenuUI.SetActive(false);
        areYouSureQuitMainMenu.SetActive(true);
    }

    public void SureQuitGameOver()
    {
        dead.SetActive(false);
        sureGameover.SetActive(true);
    }

    public void LoadMenu()
    {
        _gameOver = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Wyjdz");
        Application.Quit();
    }    
    

    public void NoSure()
    {
        _gameOver = false;
        pauseMenuUI.SetActive(true);
        areYouSureMenu.SetActive(false); 
        areYouSureQuit.SetActive(false);
        areYouSureQuitMainMenu.SetActive(false);
    }

    public void NoSureGameOver()
    {
        dead.SetActive(true);
        
        sureGameover.SetActive(false);
    }

    public void DeadPlayer()
    {
        health = FindObjectOfType<HealthPlayer>();
        _money.GetEarnedMoney();
        _groundCotroller.GetPlacedTurrets();
        if(health.currentHealth <= 0 || _waveManager.BuildingCount == 0)
        {
            dead.SetActive(true);
            GUI.SetActive(false);
            _gameOver = true;
            health.currentHealth = 1;
            Time.timeScale = 0f;
            gameIsPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<PlayerShoot>().enabled = false;
        }
    }
    
    public void SetIsInBuildMode(bool temp)
    {
        _isInBuildMode = temp;
    }

}
