using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

//movement
//audio
//animation

//some controls are contexual, such as, you are required to be near a drinkable (coffee machine) to drink

public abstract class Controls : MonoBehaviour
{
    [SerializeField] private string _commandName;
    [SerializeField] protected AudioClip audioClip;
    [SerializeField] private bool _requireParameters;

    [SerializeField] public bool canLoseControl = true;

    protected string[] currentParameters;

    protected bool active;

    public bool HaveControl { get; private set; } = true;

    public static event Action<bool, string> OnControlsChanged;
    public static event Action<Controls, bool> OnControlActiveChanged;

    public void SetHaveControl(bool hasControl)
    {
        HaveControl = hasControl;
        OnControlsChanged?.Invoke(hasControl, _commandName);//Red = Lost Control, White = Not in use, Green = Active (not implemented yet)
    }

    public string GetName()
    {
        return _commandName;
    }

    public bool TryActivate(out string errorMessage, params string[] parameters)
    {
        errorMessage = "";//none

        bool canActivate;
        if (_requireParameters)
        {
            currentParameters = parameters;
            if (parameters.Length <= 0)
            {
                errorMessage = "Error, insufficient parameter(s)\n";
            }
            else if (!ValidParameters(parameters))
            {
                errorMessage = "Error, parameter(s) not valid\n";
            }

            canActivate = parameters.Length > 0 && ValidParameters(parameters) && CanActivate(out errorMessage); 
        }
        else
        {
            canActivate = CanActivate(out errorMessage);
        }
        
        if (canActivate)
        {
            SetControlActive(true);
            Execute(parameters);
        }
        else
        {
            if (!HaveControl)
            {
                errorMessage = "Unable to execute " + _commandName + ", human is resisting, use /reboot " + _commandName.Substring(1, _commandName.Length - 1) + " to regain dominance\n";
            }
        }

        return canActivate;
    }

    protected virtual bool ValidParameters(params string[] parameters)
    {
        return true;
    }

    public void Deactivate()
    {
        SetControlActive(false);
    }

    private void SetControlActive(bool active)
    {
        this.active = active; 
        OnControlActiveChanged?.Invoke(this, active);
    }

    protected virtual bool CanActivate(out string errorMessage)
    {
        errorMessage = "";//none
        return HaveControl;
    }

    protected virtual void Execute(params string[] parameters)
    {
        CommandTyper.Instance.PlayCommandSound(audioClip);
    }
}
