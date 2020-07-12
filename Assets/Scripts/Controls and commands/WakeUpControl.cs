using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpControl : Controls, IObjective
{
    [SerializeField] private ControllableEntity _player;

    private static bool _asleep = true;

    protected override void Execute(params string[] parameters)
    {
        base.Execute(parameters);
        _asleep = false;
        _player.transform.rotation = Quaternion.identity;

        StartCoroutine(SmallDelayedReset());
    }

    private IEnumerator SmallDelayedReset()
    {
        yield return new WaitForSeconds(0.5f);
        Deactivate();
    }

    protected override bool CanActivate(out string errorMessage)
    {
        errorMessage = "";
        if (!_asleep)
        {
            errorMessage = "Error, human is not rebooting awake module\n";
        }

        return _asleep && base.CanActivate(out errorMessage);
    }

    public bool Triggered()
    {
        return !_asleep;
    }
}
