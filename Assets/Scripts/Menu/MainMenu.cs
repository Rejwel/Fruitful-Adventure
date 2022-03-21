﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider generalSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [SerializeField] private Toggle fullScreenToggle;
    private int _screenInt;
    private bool isFullScreen = false;

    [SerializeField] private AudioSource audioEffect;
    [SerializeField] private AudioSource audioBackground;
    
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;

    private const string graphicsOption = "graphics_option";
    private const string resolutionOption = "resolution_option";
    private const string fullScreenOption = "full_screen_option";
    private const string generalSliderValue = "general_slider_value";
    private const string musicSliderValue = "music_slider_value";
    private const string effectsSliderValue = "master_slider_value";
    

    void Awake()
    {
        _screenInt = PlayerPrefs.GetInt(fullScreenOption);

        if (_screenInt == 1)
        {
            isFullScreen = true;
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }
        
        qualityDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(graphicsOption, qualityDropdown.value);
            PlayerPrefs.Save();
        }));    
        
        resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
            {
                PlayerPrefs.SetInt(resolutionOption, resolutionDropdown.value);
                PlayerPrefs.Save();
            }));
    }

    void Start()
    {
        qualityDropdown.value = PlayerPrefs.GetInt(graphicsOption, 3);

        generalSlider.value = PlayerPrefs.GetFloat(generalSliderValue, 0.4f);
        effectsSlider.value = PlayerPrefs.GetFloat(effectsSliderValue, 0.4f);
        musicSlider.value = PlayerPrefs.GetFloat(musicSliderValue, 0.4f);
        
        
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
        resolutionDropdown.value = PlayerPrefs.GetInt(resolutionOption, currentResolutionIndex);
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
        PlayerPrefs.SetFloat(effectsSliderValue, volume);
        audioEffect.volume = PlayerPrefs.GetFloat(effectsSliderValue);
    }
    
    public void setBackgroundVolume(float volume)
    {
        PlayerPrefs.SetFloat(musicSliderValue, volume);
        audioBackground.volume = PlayerPrefs.GetFloat(musicSliderValue);
    }
    
    public void setVolume(float volume)
    {
        PlayerPrefs.SetFloat(generalSliderValue, volume);
        audioEffect.volume = PlayerPrefs.GetFloat(generalSliderValue);
        audioBackground.volume = PlayerPrefs.GetFloat(generalSliderValue);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        if (isFullScreen == false)
        {
            PlayerPrefs.SetInt(fullScreenOption, 0);
        }
        else
        {
            isFullScreen = true;
            PlayerPrefs.SetInt(fullScreenOption, 1);
        }
    }
    
}
