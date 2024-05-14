using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Character : MonoBehaviour, IHealthPoints
{
    [SerializeField] private CharacterData _characterData;

    private MapInitializer _mapInitializer;
    public MapInitializer map { set { _mapInitializer = value; } }

    private int _currentLife;

    public bool isPlayed = false;

    private List<Attack> _attacks;

    private Vector2 _actualPosition = Vector2.zero;
    private Vector2 _nextPosiblePosition = Vector2.zero;
    private int _currentMovement = 0;

    private void Awake()
    {
        if (!_characterData)
        {
            Debug.LogError($"{name}: Character data is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        _currentLife = _characterData.life;
    }

    public void SetFirstPosition(Vector2 position)
    {
        _actualPosition = position;
    }

    public void Move(Vector2 dir)
    {
        _nextPosiblePosition = _actualPosition + dir;
        if (_mapInitializer.CheckEmptyMapPosition(_nextPosiblePosition) && _currentMovement < _characterData.movement)
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

    public void RaciveDamage(int damage)
    {
        _currentLife -= damage;
    }
}