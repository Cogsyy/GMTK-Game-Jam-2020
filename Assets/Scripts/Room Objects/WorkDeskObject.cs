using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkDeskObject : BaseObject, IObjective
{
    private Coroutine _workCo = null;
    private bool _worked = false;

    public override bool CanInteract()
    {
        return _workCo == null;
    }

    public override string GetErrorMessage()
    {
        return "Human is already preforming pointless ritual\n";
    }

    public override void Interact(ControllableEntity player)
    {
        CommandTyper.Instance.PlayCommandSound(GetSFX());

        _workCo = StartCoroutine(WorkCo(player));
    }

    private IEnumerator WorkCo(ControllableEntity player)
    {
        yield return player.LoadForDuration(4);
        _worked = true;
        _workCo = null;
    }

    public bool Triggered()
    {
        return _worked;
    }
}
