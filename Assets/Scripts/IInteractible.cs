using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    string GetErrorMessage();
    bool CanInteract();
    void Interact(ControllableEntity player);
    AudioClip GetSFX();
}
