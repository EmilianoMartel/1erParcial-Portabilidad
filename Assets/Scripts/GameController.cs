using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MapInitializer _initializer;
    [SerializeField] private InputReader _inputReader;

    private bool _isMoving = false;
    private List<Character> _playersList;
    private List<Character> _enemies;
    private Character _actualCharacter;

    private void OnEnable()
    {
        _inputReader.moveEvent += HandleMovemente;
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= HandleMovemente;
    }

    private void HandleMovemente(Vector2 dir)
    {
        if (!_isMoving)
            StartCoroutine(TryToMove(dir));
    }

    private void HandleCharacterList(List<Character> playersList)
    {
        _playersList = playersList;
        for (int i = 0; i < playersList.Count; i++)
        {
            Debug.Log(playersList[i].name);
        }
    }

    private IEnumerator TryToMove(Vector2 dir)
    {
        _isMoving = true;
        yield return new WaitForSeconds(.5f);
        Debug.Log(dir);
        _isMoving = false;
    }
}