using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TurnController _turnController;
    [SerializeField] private InputReader _inputReader;

    private bool _isMoving = false;

    private Character _actualCharacter;
    private Vector2 _nextPosiblePosition = Vector2.zero;

    public Action<bool> isPlayeble = delegate { };
    private void OnEnable()
    {
        _inputReader.moveEvent += HandleMovement;
        _turnController.characterTurn += HandleCurrentCharacter;
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= HandleMovement;
        _turnController.characterTurn -= HandleCurrentCharacter;
    }

    private void HandleMovement(Vector2 dir)
    {
        Debug.Log(dir);
        _actualCharacter.Move(dir);
    }

    private void HandleCurrentCharacter(Character currentPlayer)
    {
        _actualCharacter = currentPlayer;
        isPlayeble?.Invoke(currentPlayer.isPlayed);
    }
}