using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : BaseObject, IObjective
{
    private Coroutine _talkBossCo = null;
    private bool _talkedBoss = false;

    public override bool CanInteract()
    {
        return _talkBossCo == null;
    }

    public override string GetErrorMessage()
    {
        return "Human is already begging\n";
    }

    public override void Interact(ControllableEntity player)
    {
        CommandTyper.Instance.PlayCommandSound(GetSFX());

        _talkBossCo = StartCoroutine(TalkBossCo(player));
    }

    private IEnumerator TalkBossCo(ControllableEntity player)
    {
        yield return player.LoadForDuration(4);
        _talkedBoss = true;
        _talkBossCo = null;
    }

    public bool Triggered()
    {
        return _talkedBoss;
    }
}
