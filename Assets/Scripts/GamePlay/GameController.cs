using EventChannel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameController : MonoBehaviour
{
    [SerializeField] private Vector2Channel _moveChannel;
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private TurnController _controller;

    private Character _character;

    public Action<List<Character>, ActionData> meleeAttackEvent = delegate { };
    public Action<List<Character>, ActionData> rangeAttackEvent = delegate { };
    public Action<List<Character>, ActionData> healingEvent = delegate { };
    public Action<Character> enemyTurnEvent;
    public Action playerTurnEvent;

    private void OnEnable()
    {
        _controller.actualCharacter += HandleCharacter;
        _moveChannel.Sucription(HandleMovement);
    }

    private void OnDisable()
    {
        _controller.actualCharacter -= HandleCharacter;
        _moveChannel.Unsuscribe(HandleMovement);
    }

    private void HandleMovement(Vector2 dir)
    {
        if (_character.CanMove() && _mapManager.CheckEmptyMapPosition(_character.actualPosition + dir))
        {
            _character.SetPosition(_mapManager.MoveCharacter(_character.actualPosition, _character.actualPosition + dir, _character));
            _character.Action(_mapManager);
        }
    }

    private void HandleCharacter(Character character)
    {
        if (_character != null)
            _character.actionEvent -= HandleActions;

        _character = character;
        if (_character._characterData.isPlayable)
        {
            _character.actionEvent += HandleActions;
            _character.Action(_mapManager);
            playerTurnEvent.Invoke();
        }
        else
        {
            enemyTurnEvent?.Invoke(_character);
        }
       
    }

    private void HandleActions(ActionRules action)
    {
        if (action is MeeleAttack)
            meleeAttackEvent?.Invoke(_controller.CheckPossibleActions(action.actionData.minDistance, action.actionData.maxDistance, _character), action.actionData);
        if (action is RangeAttack)
            rangeAttackEvent?.Invoke(_controller.CheckPossibleActions(action.actionData.minDistance, action.actionData.maxDistance, _character), action.actionData);
        if (action is Health)
            healingEvent?.Invoke(_controller.CheckPossibleActions(action.actionData.minDistance, action.actionData.maxDistance, _character), action.actionData);

    }
}