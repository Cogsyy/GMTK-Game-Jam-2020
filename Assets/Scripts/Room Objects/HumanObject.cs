using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanObject : BaseObject, IObjective
{

    private Coroutine _talkCo = null;
    private bool _talked = false;

    public override bool CanInteract()
    {
        return _talkCo == null;
    }

    public override string GetErrorMessage()
    {
        return "Human is already blabbering\n";
    }

    public override void Interact(ControllableEntity player)
    {
        CommandTyper.Instance.PlayCommandSound(GetSFX());

        _talkCo = StartCoroutine(TalkCo(player));
    }

    private IEnumerator TalkCo(ControllableEntity player)
    {
        yield return player.LoadForDuration(4);
        _talked = true;
        _talkCo = null;
    }

    public bool Triggered()
    {
        return _talked;
    }
}
