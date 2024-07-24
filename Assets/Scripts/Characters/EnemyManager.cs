using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore.Text;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private GameController _controller;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private EnemyTurnUi _enemyTurnUi;

    [SerializeField] private float _waitForEnemy = 0.5f;

    private Character _character;

    private void OnEnable()
    {
        _controller.enemyTurnEvent += HandleStartTurn;
    }

    private void OnDisable()
    {
        _controller.enemyTurnEvent -= HandleStartTurn;
    }

    private void HandleStartTurn(Character character)
    {
        _character = character;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        _enemyTurnUi.HandleChangeText($"{_character.GetCharacterName()} turn");
        yield return new WaitForSeconds(_waitForEnemy);
        yield return StartCoroutine(EnemyMovement());
        yield return StartCoroutine(EnemyAttack());
        _turnController.EndTurn();
    }

    private IEnumerator EnemyMovement()
    {
        int count = 0;
        while (_character.CanMove() && count < 5)
        {
            int x = UnityEngine.Random.Range(0, 2);
            int y = UnityEngine.Random.Range(0, 2);
            if (_mapManager.CheckEmptyMapPosition(_character.actualPosition + new Vector2(x, y)))
            {
                _enemyTurnUi.HandleChangeText($"{_character.GetCharacterName()} move");
                Debug.Log($"{_character.GetCharacterName()} move");
                _character.SetPosition(_mapManager.MoveCharacter(_character.actualPosition, _character.actualPosition + new Vector2(x, y), _character));
            }
            else
            {
                _enemyTurnUi.HandleChangeText($"{_character.GetCharacterName()} try to move and fail!");
                Debug.Log($"{_character.GetCharacterName()} try to move and fail!");
                count++;
            }
            yield return new WaitForSeconds(_waitForEnemy);
        }
    }

    private IEnumerator EnemyAttack()
    {
        for (int i = 0; i < _character._characterData.effectData.Count; i++)
        {
            yield return new WaitForSeconds(_waitForEnemy);
            List<Character> characters = _turnController.CheckPossibleActions(_character._characterData.effectData[i].minDistance, 
                                                                              _character._characterData.effectData[i].maxDistance,
                                                                              _character);
            if(characters.Count > 0)
            {
                
                int index = UnityEngine.Random.Range(0, characters.Count);
                characters[index].ReciveLifeChanger(_character._characterData.effectData[i].lifeModifier);
                _enemyTurnUi.HandleChangeText($"{_character.GetCharacterName()} attack to {characters[index].GetCharacterName()}");
                Debug.Log($"{_character.GetCharacterName()} attack to {characters[index].GetCharacterName()}");
                yield break;
            }
            _enemyTurnUi.HandleChangeText($"{_character.GetCharacterName()} try to attack and fail");
            Debug.Log($"{_character.GetCharacterName()} try to attack and fail");
        }

        yield return new WaitForSeconds(_waitForEnemy);
    }
}