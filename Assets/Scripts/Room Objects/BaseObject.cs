using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class BaseObject : MonoBehaviour, IInteractible
{
    [SerializeField] private AudioClip _interactFx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.Instance.nearbyInteractible = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.Instance.nearbyInteractible = null;
        }
    }

    public AudioClip GetSFX()
    {
        return _interactFx;
    }

    public abstract string GetErrorMessage();
    public abstract bool CanInteract();
    public abstract void Interact(ControllableEntity player);
}
