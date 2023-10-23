using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class Evento
{
    public float time;
    public float duration;
    public string type;
    public string message;

    public Evento(float _time, float _duration, string _type, string _message)
    {
        time = _time;
        duration = _duration;
        type = _type;
        message = _message;

    }
}


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    #region variables
    [Header("Variables")]
    public float walkingSpeed = 2f;
    public float flyingSpeed = 3f;
    public float diveSpeed = 4f;
    #endregion


    [Header("Particles")]
    public ParticleSystem fog;
    public ParticleSystem rain;

    private float elapsedTime = 0;

    [Header("Eventos")]
    private int iterator = 0;
    public List<Evento> eventList;
    public TMP_Text notification;

    [Header("Spawner")]
    public SpawnDuck duckSpawner;
    public SpawnFly flyDuckSpawner;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        eventList = new List<Evento>();
        eventList.Add(new Evento(20f, 20f, "Rain", "Rain coming!"));
        eventList.Add(new Evento(30f, 15f, "Horde", "Horde coming!"));
        eventList.Add(new Evento(50f, 5f, "Break", "Break time"));
        eventList.Add(new Evento(55f, 1f, "Break", "Break is over!"));
        eventList.Add(new Evento(70f, 20f, "Horde", "Horde coming!"));
        eventList.Add(new Evento(80f, 10f, "Fog", "Fog coming!"));
        eventList.Add(new Evento(95f, 5f, "Break", "Break time"));
        eventList.Add(new Evento(100f, 1f, "BreakOver", "Break is over!"));
        eventList.Add(new Evento(110f, 20f, "Rain", "Break is over!"));
        eventList.Add(new Evento(120f, 10f, "Fog", "Fog coming!"));
        eventList.Add(new Evento(140f, 1f, "Finish", "LevelFinished!"));

        FinishHorde();

    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (iterator < eventList.Count)
        {
            if (elapsedTime > eventList[iterator].time)
            {
                ShowNotification(eventList[iterator].message);
                switch (eventList[iterator].type)
                {
                    case "Rain":
                        rain.Play();
                        Invoke("TurnOffRain", eventList[iterator].duration);
                        rain.gameObject.GetComponent<AudioSource>().Play();
                        break;
                    case "Fog":
                        fog.Play();
                        Invoke("TurnOffFog", eventList[iterator].duration);
                        break;
                    case "Horde":
                        duckSpawner.spawnInterval = 6f;
                        flyDuckSpawner.spawnInterval = 5f;
                        Invoke("FinishHorde", eventList[iterator].duration);
                        break;
                    case "Break":
                        duckSpawner.breakTime = true;
                        flyDuckSpawner.breakTime = true;

                        break;
                    case "BreakOver":
                        duckSpawner.breakTime = false;
                        flyDuckSpawner.breakTime = false;
                        break;
                }
                iterator++;

            }
        }
        





    }

    private void ShowNotification(string message)
    {
        notification.text = message;
        notification.transform.parent.transform.parent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-237.68f, -110.31f),0.3f);
        Invoke("HideNotification", 1f);

    }
    private void HideNotification()
    {
        notification.transform.parent.transform.parent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(241.2f, -110.31f), 0.3f);

    }



    private void TurnOffRain()
    {
        rain.Stop();
        rain.gameObject.GetComponent<AudioSource>().Stop();
    }
    private void TurnOffFog()
    {
        fog.Stop();
    }

    private void FinishHorde()
    {
        duckSpawner.spawnInterval = 8f;
        flyDuckSpawner.spawnInterval = 10f;
    }





}
