using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public CharacterData _characterData;

    private Vector2 _actualPosition;
    private int _currentMove = 0;
    private int _currentLife = 0;
    private List<ActionRules> _actionRules = new();

    public Vector2 actualPosition => _actualPosition;
    public Action<ActionRules> actionEvent;
    public Action<int> onLifeChange;
    public Action<Character> onDead;

    public void CreateCharacter(CharacterData characterData)
    {
        _characterData = characterData;
        _currentLife = characterData.life;
    }

    public void Action(MapManager manager)
    {
        foreach (var rules in _actionRules)
        {
            actionEvent?.Invoke(rules);
        }
    }

    public bool CanMove()
    {
        if (_currentMove < _characterData.movement)
            return true;

        return false;
    }

    public void ReciveLifeChanger(int change)
    {
        _currentLife += change;
        onLifeChange?.Invoke(_currentLife);
        Debug.Log($"{_characterData.nameCharacter}: {_currentLife}");
        if (_currentLife <= 0)
            onDead?.Invoke(this);
    }

    public void SetActionRules(ActionRules action)
    {
        _actionRules.Add(action);
    }

    public void SetPosition(Vector2 position)
    {
        _currentMove++;
        _actualPosition = position;
    }
    
    public void Reset()
    {
        _currentMove = 0;
    }
}