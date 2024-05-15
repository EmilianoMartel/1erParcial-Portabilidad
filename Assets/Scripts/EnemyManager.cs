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
        yield return StartCoroutine(EnemyMovement());
        yield return StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyMovement()
    {
        int count = 0;
        while (_character.CanMove() || count > 5)
        {
            int x = UnityEngine.Random.Range(0, 2);
            int y = UnityEngine.Random.Range(0, 2);
            if (_mapManager.CheckEmptyMapPosition(_character.actualPosition + new Vector2(x, y)))
            {
                _character.SetPosition(_mapManager.MoveCharacter(_character.actualPosition, _character.actualPosition + new Vector2(x, y), _character));
            }
            else
            {
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
                characters[i].ReciveLifeChanger(_character._characterData.effectData[i].lifeModifier);
                yield break;
            }
        }
        Debug.Log("llega");
    }
}
