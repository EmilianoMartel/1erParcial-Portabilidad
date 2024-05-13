using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHealthPoints
{
    [SerializeField] private int _life = 10;
    [SerializeField] private int _movement = 1;

    private MapInitializer _mapInitializer;
    public MapInitializer map { set { _mapInitializer = value; } }

    public bool isActive = false;
    public bool isPlayed = false;

    private Vector2 _actualPosition = Vector2.zero;
    private Vector2 _nextPosiblePosition = Vector2.zero;
    private int _currentMovement = 0;

    public void SetFirstPosition(Vector2 position)
    {
        _actualPosition = position;
    }

    public void Move(Vector2 dir)
    {
        _nextPosiblePosition = _actualPosition + dir;
        if (_mapInitializer.CheckEmptyMapPosition(_nextPosiblePosition) && _currentMovement < _movement)
        {
            _currentMovement++;
            _mapInitializer.MoveCharacter(_actualPosition, _nextPosiblePosition, this);
            _actualPosition = _nextPosiblePosition;
        }
    }

    public void Restart()
    {
        _currentMovement = 0;
    }
}