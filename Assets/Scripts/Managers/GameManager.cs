using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{

    [SerializeField] private string playId = "Play";
    [SerializeField] private string exitId = "Exit";
    [SerializeField] private MapManager _mapManager;

    private List<Character> _characters = new();

    public Action<bool, Character> winnerEvent = delegate { };

    public event Action startGame;
    public event Action startAd;
    public event Action endGame;

    private void OnEnable()
    {
        _mapManager.createdCharacter += AddCharacter;
    }

    private void OnDisable()
    {
        _mapManager.createdCharacter -= AddCharacter;
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].onDead -= HandleCharacterDead;
        }
    }

    private void Awake()
    {
        Validate();
    }

    public void HandleSpecialEvents(string id)
    {
        if (id == playId)
            startGame?.Invoke();
        if (id == exitId)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }

    private void AddCharacter(Character character)
    {
        _characters.Add(character);
        character.onDead += HandleCharacterDead;
    }

    private void HandleCharacterDead(Character character)
    {
        character.onDead -= HandleCharacterDead;
        if (_characters.Contains(character))
            _characters.Remove(character);

        if (character._characterData.isPlayable)
            GameOverLogic();
    }

    private void GameOverLogic()
    {
        if (CheckIfAreEnemies())
            winnerEvent?.Invoke(false, new());
        if (_characters.Count == 1)
            winnerEvent?.Invoke(true, _characters[0]);
    }

    private bool CheckIfAreEnemies()
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            if (!_characters[i]._characterData.isPlayable)
                return true;
        }
        return false;
    }

    private void Validate()
    {
        if (!_mapManager)
        {
            Debug.LogError($"{name}: Map Manager is null.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}