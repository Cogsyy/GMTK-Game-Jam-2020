using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        InitSingleton(this as T);
    }

    protected virtual void OnDestroy()
    {
        Instance = null;
    }

    private void InitSingleton(T newInstance)
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = newInstance;
            OnSetSingleton();
        }
    }

    protected virtual void OnSetSingleton()
    {

    }
}
