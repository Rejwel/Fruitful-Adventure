using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    
    [SerializeField] private AudioSource audioEffect;
    [SerializeField] private AudioSource audioBackground;
    
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;


    void Start()
    {
        resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution. width &&
                resolutions[i].height == Screen.currentResolution. height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && (gameplayMenu.active || audioMenu.active || videoMenu.active))
        {
            gameplayMenu.SetActive(false);
            audioMenu.SetActive(false);
            videoMenu.SetActive(false);
            optionsMenu.SetActive(true);
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && optionsMenu.active)
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void QuitGame()
    {
        Debug.Log("Wychodze");
        Application.Quit();
    }

    public void setEffectVolume(float volume)
    {
        audioEffect.volume = volume;
    }
    
    public void setBackgroundVolume(float volume)
    {
        audioBackground.volume = volume;
    }
    
    public void setVolume(float volume)
    {
        audioEffect.volume = volume;
        audioBackground.volume = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
}
