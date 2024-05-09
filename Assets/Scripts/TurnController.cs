using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private MapInitializer _initializer;

    private List<Character> _characters = new();

    private int _currentTurn = 0;
    public Action<Character> characterTurn = delegate { };

    private void Start()
    {
        characterTurn?.Invoke(_characters[0]);
    }

    private void HandleEnemies(Character character)
    {
        _characters.Add(character);
    }

    [ContextMenu("EndTurn")]
    private void HandleEndTurn()
    {
        _currentTurn++;
        if (_characters.Count >= _currentTurn)
            _currentTurn = 0;

        characterTurn?.Invoke(_characters[_currentTurn]);
    }
}