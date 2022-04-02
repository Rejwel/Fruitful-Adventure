using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider headBobbingSlider;
    
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [SerializeField] private Toggle fullScreenToggle;
    private int _screenInt;
    private bool _isFullScreen;
    [SerializeField] private Toggle vSyncToggle;
    private int _vSyncInt;
    private bool _isVSync;
    
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;
    [SerializeField] private AudioSource audioEffect;
    [SerializeField] private AudioSource audioBackground;
    
    private MouseLook _mouseLook;
    private HeadBobbing _headBobbing;
    
    private const string graphicsOption = "graphics_option";
    private const string resolutionOption = "resolution_option";
    private const string fullScreenOption = "full_screen_option";
    private const string vSyncOption = "vSync_option";
    private const string musicSliderValue = "music_slider_value";
    private const string effectsSliderValue = "effects_slider_value";
    private const string mouseSensitivitySliderValue = "mouse_sensitivity_slider_value";
    private const string headBobbingSliderValue = "head_bobbing_slider_value";

    void Awake()
    {
        _mouseLook = FindObjectOfType<MouseLook>();
        _headBobbing = FindObjectOfType<HeadBobbing>();
        
        
        _screenInt = PlayerPrefs.GetInt(fullScreenOption, 1);
        _vSyncInt = PlayerPrefs.GetInt(vSyncOption, 1);

        if (_screenInt == 1)
        {
            _isFullScreen = true;
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }
        
        if (_vSyncInt == 1)
        {
            _isVSync = true;
            vSyncToggle.isOn = true;
        }
        else
        {
            vSyncToggle.isOn = false;
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
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat(mouseSensitivitySliderValue, 300f);
        headBobbingSlider.value = PlayerPrefs.GetFloat(headBobbingSliderValue, 0.4f);
        effectsSlider.value = PlayerPrefs.GetFloat(effectsSliderValue, 0.4f);
        musicSlider.value = PlayerPrefs.GetFloat(musicSliderValue, 0.4f);
        
        initResolutions();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (gameplayMenu.active || audioMenu.active || videoMenu.active))
        {
            gameplayMenu.SetActive(false);
            audioMenu.SetActive(false);
            videoMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }

    }

    private void initResolutions()
    {
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60 || resolution.refreshRate == 144).ToArray();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(resolutionOption, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
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
        audioEffect.volume = volume;
    }
    
    public void setBackgroundVolume(float volume)
    {
        PlayerPrefs.SetFloat(musicSliderValue, volume);
        audioBackground.volume = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        if (!isFullScreen)
        {
            PlayerPrefs.SetInt(fullScreenOption, 0);
        }
        else
        {
            isFullScreen = true;
            PlayerPrefs.SetInt(fullScreenOption, 1);
        }
    }

    public void SetVSync(bool isVSync)
    {
        if (!isVSync)
        {
            PlayerPrefs.SetInt(vSyncOption, 0);
            QualitySettings.vSyncCount = PlayerPrefs.GetInt(vSyncOption);
        }
        else
        {
            isVSync = true;
            PlayerPrefs.SetInt(vSyncOption, 1);
            QualitySettings.vSyncCount = PlayerPrefs.GetInt(vSyncOption);
        }
    }
    
    public void setMouseSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat(mouseSensitivitySliderValue, sensitivity);
        _mouseLook.mouseSensitivity = sensitivity;
    }

    public void setHeadBobbing(float amplitude)
    {
        PlayerPrefs.SetFloat(headBobbingSliderValue, amplitude);
        _headBobbing._amplitude = amplitude;
    }
    
}