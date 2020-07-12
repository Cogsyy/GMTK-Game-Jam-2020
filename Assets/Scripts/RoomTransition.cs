using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private Transform _controllableEntity;
    [SerializeField] private Transform _transitionSpawnPoint;
    [SerializeField] private GameObject _activeRoom;
    [SerializeField] private GameObject _transitionToRoom;
    [SerializeField] private Collider2D _collider;

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameController.Instance.touchingTransition = Transition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.Instance.touchingTransition = null;
        }
    }

    private void Transition()
    {
        _collider.enabled = false;
        _transitionToRoom.SetActive(true);
        _activeRoom.SetActive(false);

        if (_transitionSpawnPoint != null)
        {
            _controllableEntity.position = _transitionSpawnPoint.position;
        }
    }
}
