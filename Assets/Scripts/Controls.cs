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

    protected bool active;

    public bool HaveControl { get; private set; }

    public event Action<bool> OnControlsChanged;

    private void SetHaveControl(bool hasControl)
    {
        HaveControl = hasControl;
        OnControlsChanged?.Invoke(hasControl);//remove or set red in the list of controls?
    }

    public string GetName()
    {
        return _commandName;
    }

    public void TryActivate()
    {
        if (CanActivate())
        {
            active = true;
        }
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
