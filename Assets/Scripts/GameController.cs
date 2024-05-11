using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TurnController _turnController;
    [SerializeField] private InputReader _inputReader;

    private bool _isMoving = false;

    private Character _actualCharacter;

    private void OnEnable()
    {
        _inputReader.moveEvent += HandleMovemente;
        _turnController.characterTurn += HandleCurrentCharacter;
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= HandleMovemente;
        _turnController.characterTurn -= HandleCurrentCharacter;
    }

    private void HandleMovemente(Vector2 dir)
    {
        if (!_isMoving)
            StartCoroutine(TryToMove(dir));
    }

    private void HandleCurrentCharacter(Character currentPlayer)
    {
        _actualCharacter = currentPlayer;
    }

    private IEnumerator TryToMove(Vector2 dir)
    {
        _isMoving = true;
        yield return new WaitForSeconds(.5f);
        Debug.Log(dir);
        _isMoving = false;
    }
}