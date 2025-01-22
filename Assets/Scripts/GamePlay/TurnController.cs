using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TurnController : MonoBehaviour
{
    [SerializeField] private MapManager _mapManager;

    private List<Character> _activeCharacters = new();
    private List<Character> _allCharacters = new();
    private int _currentTurn = 0;
    public Action<Character> actualCharacter;
    public Action startEndTurn;

    private void OnEnable()
    {
        _mapManager.startGame += HandleStartGame;
        _mapManager.createdCharacter += AddCharacter;
    }

    private void OnDisable()
    {
        _mapManager.startGame -= HandleStartGame;
        _mapManager.createdCharacter -= AddCharacter;
        for (int i = 0; i < _allCharacters.Count; i++)
        {
            _activeCharacters[i].onDead -= HandleCharacterDead;
            _activeCharacters[i].revive -= HandleCharacterRevive;
        }
    }

    private void HandleStartGame()
    {
        _currentTurn = 0;
        actualCharacter?.Invoke(_activeCharacters[_currentTurn]);
    }

    private void AddCharacter(Character character)
    {
        _activeCharacters.Add(character);
        character.onDead += HandleCharacterDead;
        character.revive += HandleCharacterRevive;
        _allCharacters.Add(character);
    }

    private void HandleActionInCharacter(int life)
    {
        EndTurn();
    }

    public void EndTurn()
    {
        StartCoroutine(EndTurnLogic());
    }

    private IEnumerator EndTurnLogic()
    {
        startEndTurn?.Invoke();
        yield return new WaitForEndOfFrame();
        _currentTurn++;
        if (_currentTurn >= _activeCharacters.Count)
            _currentTurn = 0;


        _activeCharacters[_currentTurn].Reset();
        actualCharacter?.Invoke(_activeCharacters[_currentTurn]);
    }

    private void HandleCharacterDead(Character character)
    {
        character.onDead -= HandleCharacterDead;
        if (_activeCharacters.Contains(character))
            _activeCharacters.Remove(character);
    }

    public List<Character> CheckPossibleActions(int minDistance, int maxDistance,Character character)
    {
        List<Character> temp = new();
        for (int i = 0; i < _activeCharacters.Count; i++)
        {
            int dx = (int)Math.Abs(_activeCharacters[i].actualPosition.x - character.actualPosition.x);
            int dy = (int)Math.Abs(_activeCharacters[i].actualPosition.y - character.actualPosition.y);

            int gridDistance = Math.Max(dx, dy);

            if (gridDistance > minDistance && gridDistance <= maxDistance)
            {
                temp.Add(_activeCharacters[i]);
            }
        }
        return temp;
    }

    private void HandleCharacterRevive(Character character)
    {
        character.onDead += HandleCharacterDead;
        _activeCharacters.Add(character);
    }
}