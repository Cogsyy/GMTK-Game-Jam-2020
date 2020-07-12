using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : RoomTransition, IObjective
{
    private bool _sleeping;

    public override bool CanInteract()
    {
        return !_sleeping;
    }

    public override string GetErrorMessage()
    {
        return "Error, human already rebooting\n";
    }

    public override void Interact(ControllableEntity player)
    {
        _sleeping = true;
        player.transform.position = transform.position;
        player.transform.eulerAngles = new Vector3(0, 0, 90);

        Transition();
    }

    public bool Triggered()
    {
        return _sleeping;
    }
}
