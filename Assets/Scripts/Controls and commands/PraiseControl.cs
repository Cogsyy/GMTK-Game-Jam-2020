using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraiseControl : Controls, IObjective
{
    private static bool _praised = false;

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
        _praised = true;
        Triggered();
    }

    public bool Triggered()
    {
        return _praised;
    }
}
