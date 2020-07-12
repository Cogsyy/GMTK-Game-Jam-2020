using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenControl : Controls
{
    protected override void Execute(params string[] parameters)
    {
        base.Execute(parameters);
        GameController.Instance.touchingTransition.Invoke();
    }

    protected override bool CanActivate(out string errorMessage)
    {
        return base.CanActivate(out errorMessage) && GameController.Instance.touchingTransition != null;
    }
}
