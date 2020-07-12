using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryControl : Controls
{
    protected override void Execute(params string[] parameters)
    {
        base.Execute(parameters);
        StartCoroutine(SmallDelayedReset());
        
    }

    private IEnumerator SmallDelayedReset()
    {
        yield return new WaitForSeconds(2f);
        Deactivate();
    }
}
