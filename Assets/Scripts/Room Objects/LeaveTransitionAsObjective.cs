using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTransitionAsObjective : RoomTransition, IObjective
{
    [SerializeField] private LevelBase _level;

    private bool _triggered;

    public override bool CanInteract()
    {
        return _level.CanLeave();
    }

    public override string GetErrorMessage()
    {
        return "Error, must perform standard human tasks first\n";//N/A
    }

    public override void Interact(ControllableEntity player)
    {
        base.Interact(player);
        _triggered = true;
    }

    public bool Triggered()
    {
        return _triggered;
    }
}
