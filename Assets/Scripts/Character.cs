using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHealthPoints
{
    [SerializeField] protected int _life = 10;

    protected Vector2 p_actualPosition = Vector2.zero;
    
    public Action<Character, Vector2> characterMovement;

    protected void Movement(Vector2 direction)
    {
        characterMovement?.Invoke(this, direction);
    }

    public void SetFirstPosition(Vector2 position)
    {
        p_actualPosition = position;
    }
}