using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : BaseObject
{
    [SerializeField] private Transform _controllableEntity;
    private Transform _humanObject;
    [SerializeField] private Transform _transitionSpawnPoint;
    [SerializeField] private GameObject _activeRoom;
    [SerializeField] private GameObject _transitionToRoom;
    [SerializeField] private Collider2D _collider;

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    protected void Transition()
    {
        StaticTrigger.Instance.TriggerStaticTransition();
        _collider.enabled = false;
        _transitionToRoom.SetActive(true);
        _activeRoom.SetActive(false);

        if (_transitionSpawnPoint != null)
        {
            _controllableEntity.position = _transitionSpawnPoint.position;
            _controllableEntity.GetChild(0).localPosition = Vector3.zero;
        }
    }

    public override bool CanInteract()
    {
        return true;
    }

    public override string GetErrorMessage()
    {
        return "";//N/A
    }

    public override void Interact(ControllableEntity player)
    {
        CommandTyper.Instance.PlayCommandSound(GetSFX());
        Transition();
    }
}
