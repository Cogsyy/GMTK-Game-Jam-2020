using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DirectionalControl : Controls
{
    [SerializeField] private Direction _direction;
    private float _speed = 1;

    public Vector3 GetMoveDelta()
    {
        return  active ? new Vector3(GetHorizontalDelta(), GetVerticalDelta(), 0) : Vector3.zero;
    }

    private float GetHorizontalDelta()
    {
        return (_direction == Direction.Left ? -1 : _direction == Direction.Right ? 1 : 0) * _speed * Time.deltaTime;
    }

    private float GetVerticalDelta()
    {
        return (_direction == Direction.Up ? 1 : _direction == Direction.Down? -1 : 0) * _speed * Time.deltaTime;
    }
}

public enum Direction
{
    None = 0,
    Up,
    Down,
    Left,
    Right,
}
