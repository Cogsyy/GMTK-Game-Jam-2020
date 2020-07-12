using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : BaseObject, IObjective
{
    [SerializeField] private float _showerDuration = 4;
    private bool _usedShower;

    private Coroutine _showerCo;
    
    public override bool CanInteract()
    {
        return _showerCo == null;
    }

    public override string GetErrorMessage()
    {
        return "Error, cleaning routines already active\n";
    }

    public override void Interact(ControllableEntity player)
    {
        CommandTyper.Instance.PlayCommandSound(GetSFX());

        _showerCo = StartCoroutine(ShowerAsAnObjectiveCo(player));
    }

    public bool Triggered()
    {
        return _usedShower;
    }

    private IEnumerator ShowerAsAnObjectiveCo(ControllableEntity player)
    {
        yield return player.LoadForDuration(_showerDuration);
        _usedShower = true;
        _showerCo = null;
    }
}
