using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject mainMenu;

    [Header("Settings")]
    [SerializeField]
    private GameObject settings;
    [SerializeField]
    private Slider sensitivitySlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private AudioMixer audioMixer;

    [Header("Post-Processing")]
    [SerializeField]
    private Volume globalVolume;

    private bool gamePaused = true;        
    private DepthOfField depthOfField;

    private int mouseSensitivity = 5;

    public bool GamePaused { get => gamePaused; set => gamePaused = value; }
    public int MouseSensitivity { get => mouseSensitivity; set => mouseSensitivity = value; }

    private void Start()
    {
        globalVolume.profile.TryGet(out depthOfField);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityValueChange);
        musicSlider.onValueChanged.AddListener(OnMusicValueChange);
        sfxSlider.onValueChanged.AddListener(OnSFXValueChange);
    }

    public void OnPlayButtonClicked()
    {
        mainMenu.SetActive(false);
        depthOfField.active = false;
        gamePaused = false;
    }

    public void OnSettingsButtonPressed()
    {
        if (gamePaused) 
        {
            settings.SetActive(true);
            depthOfField.active = true;
            sensitivitySlider.value = mouseSensitivity;
        }
        else
        {
            settings.SetActive(false);
            depthOfField.active = false;
        }
    }

    public void OnSensitivityValueChange(float value)
    {
        mouseSensitivity = (int) value;
    }

    public void OnMusicValueChange(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void OnSFXValueChange(float value)
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(value) * 20);
    }
}
