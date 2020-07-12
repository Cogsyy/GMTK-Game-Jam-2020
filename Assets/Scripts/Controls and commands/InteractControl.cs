using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractControl : Controls
{
    [SerializeField] private ControllableEntity _player;

    protected override bool CanActivate(out string errorMessage)
    {
        errorMessage = "";//none

        IInteractible interactible = GameController.Instance.nearbyInteractible;
        if (interactible == null)
        {
            errorMessage = "Error, nothing to interact with\n";
        }
        else if (!interactible.CanInteract())
        {
            errorMessage = interactible.GetErrorMessage();
        }

        return HaveControl && interactible != null && interactible.CanInteract();
    }

    protected override void Execute(params string[] parameters)
    {
        base.Execute(parameters);
        CommandTyper.Instance.DeactivateAllCommands();
        GameController.Instance.nearbyInteractible.Interact(_player);

        StartCoroutine(SmallDelayedReset());
    }

    private IEnumerator SmallDelayedReset()
    {
        yield return new WaitForSeconds(0.5f);
        Deactivate();
    }
}
