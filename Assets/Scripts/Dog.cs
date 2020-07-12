using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : SimpleLoadObject
{
    [SerializeField] private bool _level5Dog;
    [SerializeField] private DogBarks _barks;

    public override void Interact(ControllableEntity player)
    {
        base.Interact(player);
        
        if (_level5Dog)
        {
            _barks.PlayLevel5Sequence();
        }
        else
        {
            _barks.PlayLevel1Sequence();
        }
    }
}
