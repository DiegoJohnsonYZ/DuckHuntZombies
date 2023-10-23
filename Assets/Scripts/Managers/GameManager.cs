using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private AudioSource introAudioSource;
    [SerializeField]
    private AudioSource mainAudioSource;
    
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

    [Header("Game Over")]
    [SerializeField]
    private GameObject gameOverContainer;

    [Header("Post-Processing")]
    [SerializeField]
    private Volume globalVolume;

    private bool gamePaused = false;        
    private DepthOfField depthOfField;

    private int mouseSensitivity = 5;
    private bool startLoop = false;
    private float levelDuration = 150f;

    public bool GamePaused { get => gamePaused; set => gamePaused = value; }
    public int MouseSensitivity { get => mouseSensitivity; set => mouseSensitivity = value; }

    private void Start()
    {
        globalVolume.profile.TryGet(out depthOfField);
        depthOfField.active = false;
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityValueChange);
        musicSlider.onValueChanged.AddListener(OnMusicValueChange);
        sfxSlider.onValueChanged.AddListener(OnSFXValueChange);

        DOTween.Sequence().AppendInterval(levelDuration).AppendCallback(() => EndLevel());
    }

    void Update()
    {
        if (!introAudioSource.isPlaying && !startLoop)
        {
            mainAudioSource.Play();
            startLoop = true;
        }
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

    public void EndLevel()
    {
        gameOverContainer.SetActive(true);
        depthOfField.active = true;
    }

    public void GameOver()
    {
        gameOverContainer.SetActive(true);
        depthOfField.active = true;
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnBackMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitGameButtonClicked()
    {
        Application.Quit();
    }
}
