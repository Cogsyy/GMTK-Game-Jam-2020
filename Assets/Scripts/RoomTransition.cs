﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private Transform _controllableEntity;
    [SerializeField] private Transform _transitionSpawnPoint;
    [SerializeField] private GameObject _activeRoom;
    [SerializeField] private GameObject _transitionToRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _transitionToRoom.SetActive(true);
            _controllableEntity.position = _transitionSpawnPoint.position;
            _activeRoom.SetActive(false);
        }
    }
}