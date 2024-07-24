using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TurnController : MonoBehaviour
{
    [SerializeField] private MapManager _mapManager;

    private List<Character> _characters = new();
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
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].onDead -= HandleCharacterDead;
        }
    }

    private void HandleStartGame()
    {
        _characters[_currentTurn].Reset();
        actualCharacter?.Invoke(_characters[_currentTurn]);
    }

    private void AddCharacter(Character character)
    {
        _characters.Add(character);
        character.onDead += HandleCharacterDead;
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
        if (_currentTurn >= _characters.Count)
            _currentTurn = 0;


        _characters[_currentTurn].Reset();
        actualCharacter?.Invoke(_characters[_currentTurn]);
    }

    private void HandleCharacterDead(Character character)
    {
        character.onDead -= HandleCharacterDead;
        if (_characters.Contains(character))
            _characters.Remove(character);
    }

    public List<Character> CheckPossibleActions(int minDistance, int maxDistance,Character character)
    {
        List<Character> temp = new();
        for (int i = 0; i < _characters.Count; i++)
        {
            int dx = (int)Math.Abs(_characters[i].actualPosition.x - character.actualPosition.x);
            int dy = (int)Math.Abs(_characters[i].actualPosition.y - character.actualPosition.y);

            int gridDistance = Math.Max(dx, dy);

            if (gridDistance > minDistance && gridDistance <= maxDistance)
            {
                temp.Add(_characters[i]);
            }
        }
        return temp;
    }
}