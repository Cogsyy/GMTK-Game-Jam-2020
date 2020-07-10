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
    public bool active;

    public bool HaveControl { get; private set; }

    public event Action<bool> OnControlsChanged;

    private void SetHaveControl(bool hasControl)
    {
        HaveControl = hasControl;
        OnControlsChanged?.Invoke(hasControl);//remove or set red in the list of controls?
    }
}
