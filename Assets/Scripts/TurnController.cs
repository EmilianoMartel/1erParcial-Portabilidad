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

    private void OnEnable()
    {
        _initializer.createdCharacter += HandleCharacter;
        _initializer.setGame += HandleStartGame;
    }

    private void OnDisable()
    {
        _initializer.createdCharacter -= HandleCharacter;
        _initializer.setGame -= HandleStartGame;
    }

    private void HandleStartGame()
    {
        HandleNextTurn();
    }

    private void HandleCharacter(Character character)
    {
        _characters.Add(character);
    }

    [ContextMenu("EndTurn")]
    private void HandleNextTurn()
    {
        characterTurn?.Invoke(_characters[_currentTurn]);
        Debug.Log(_characters[_currentTurn].name);

        _currentTurn++;
        if (_characters.Count <= _currentTurn)
            _currentTurn = 0;
    }
}