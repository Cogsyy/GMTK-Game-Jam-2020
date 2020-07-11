using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//movement
//audio
//animation

//some controls are contexual, such as, you are required to be near a drinkable (coffee machine) to drink

public abstract class Controls : MonoBehaviour
{
    [SerializeField] private string _commandName;
    [SerializeField] private AudioClip _audioClip;

    protected bool active;

    public bool HaveControl { get; private set; }

    public static event Action<bool, string> OnControlsChanged;

    public void SetHaveControl(bool hasControl)
    {
        HaveControl = hasControl;
        OnControlsChanged?.Invoke(hasControl, _commandName);//Red = Lost Control, White = Not in use, Green = Active (not implemented yet)
    }

    public string GetName()
    {
        return _commandName;
    }

    public bool TryActivate()
    {
        bool canActivate = CanActivate();
        if (canActivate)
        {
            active = true;
        }

        return canActivate;
    }

    public void Deactivate()
    {
        active = false;
    }

    protected virtual bool CanActivate()
    {
        return true;
    }
}
