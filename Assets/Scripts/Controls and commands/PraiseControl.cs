using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraiseControl : Controls, IObjective
{
    protected override void Execute(params string[] parameters)
    {
        base.Execute(parameters);
        CommandTyper.Instance.PlayPraiseAnim(true);
        StartCoroutine(SmallDelayedReset());

    }

    private IEnumerator SmallDelayedReset()
    {
        yield return new WaitForSeconds(2f);
        CommandTyper.Instance.PlayPraiseAnim(false);
        Deactivate();
    }

    public bool Triggered()
    {
        return true;
    }
}
