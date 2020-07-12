using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : BaseObject, IObjective
{
    private Coroutine _drinkCo = null;
    private bool _drankCoffee = false;

    public override bool CanInteract()
    {
        return _drinkCo == null;
    }

    public override string GetErrorMessage()
    {
        return "Human is already changing oil\n";
    }

    public override void Interact(ControllableEntity player)
    {
        CommandTyper.Instance.PlayCommandSound(GetSFX());

        _drinkCo = StartCoroutine(DrinkCo(player));
    }

    private IEnumerator DrinkCo(ControllableEntity player)
    {
        yield return player.LoadForDuration(4);
        _drankCoffee = true;
        _drinkCo = null;
    }

    public bool Triggered()
    {
        return _drankCoffee;
    }
}
