using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHealthPoints
{
    [SerializeField] private int _life = 10;
    [SerializeField] private int _movement = 1; 

    public bool isActive = false;
    public bool isPlayed = false;

    private Vector2 _actualPosition = Vector2.zero;
    private Vector2 _nextPosiblePosition = Vector2.zero;
    
    public Action<Character, Vector2> characterMovement;

    public void SetFirstPosition(Vector2 position)
    {
        _actualPosition = position;
    }

    public void Move(Vector2 dir)
    {
        _nextPosiblePosition = _actualPosition + dir;
        characterMovement?.Invoke(this, _nextPosiblePosition);
    }
}