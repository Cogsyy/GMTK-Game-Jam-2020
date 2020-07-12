using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLoadObject : BaseObject, IObjective
{
    [SerializeField] private float _duration;
    [SerializeField] private string _errorMessage;
    private Coroutine _doTheThing;
    private bool _usedOnce;

    public override bool CanInteract()
    {
        return _doTheThing == null;
    }

    public override string GetErrorMessage()
    {
        return _errorMessage;
    }

    public override void Interact(ControllableEntity player)
    {
        CommandTyper.Instance.PlayCommandSound(GetSFX());
        _doTheThing = StartCoroutine(DoTheThingAndTriggerObjectiveCo(player));
    }

    public bool Triggered()
    {
        return _usedOnce;
    }

    private IEnumerator DoTheThingAndTriggerObjectiveCo(ControllableEntity player)
    {
        yield return player.LoadForDuration(_duration);
        _usedOnce = true;
        _doTheThing = null;
    }
}
