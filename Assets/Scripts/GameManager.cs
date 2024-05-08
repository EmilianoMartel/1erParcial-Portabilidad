using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Character> _characters = new();

    private int _currentTurn = 0;
    public Action<Character> characterTurn = delegate { };

    private void HandleEndTurn()
    {
        _currentTurn++;
        if (_characters.Count >= _currentTurn)
            _currentTurn = 0;

        characterTurn?.Invoke(_characters[_currentTurn]);
    }
}