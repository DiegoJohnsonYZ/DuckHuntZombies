using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; protected set; }
    public static event Action InstanceSet;

    protected virtual void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = (T)this;

            if (InstanceSet != null)
                InstanceSet();
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
