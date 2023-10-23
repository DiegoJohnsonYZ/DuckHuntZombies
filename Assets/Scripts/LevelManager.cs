using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Evento
{
    public float time;
    public float duration;
    public string type;
    public string message;
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
    public List<Evento> eventList;   

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        




    }





}