using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopControl : Controls
{
    protected override void Execute(params string[] parameters)
    {
        base.Execute();
        CommandTyper.Instance.DeactivateAllCommands();
    }
}
