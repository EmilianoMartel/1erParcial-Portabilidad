using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _mainMenuSound;
    [SerializeField] private AudioSource _gameSound;
    [SerializeField] private GameManager _gameManager;

    private void OnEnable()
    {
        _gameManager.startGame += HandleStarGame;
        _gameManager.endGame += HandleMenu;
    }

    private void OnDisable()
    {
        _gameManager.startGame -= HandleStarGame;
        _gameManager.endGame -= HandleMenu;
    }

    private void Awake()
    {
        ValidateNullReferences();
    }

    private void Start()
    {
        HandleMenu();
    }

    private void HandleStarGame()
    {
        _mainMenuSound.Stop();
        _gameSound.Play();
    }

    private void HandleMenu()
    {
        _mainMenuSound.Play();
        _gameSound.Stop();
    }

    private void ValidateNullReferences()
    {
        if (!_mainMenuSound)
        {
            Debug.LogError($"{name}: Menu sound is null.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_gameSound)
        {
            Debug.LogError($"{name}: Game sound is null.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_gameManager)
        {
            Debug.LogError($"{name}: Game Manager is null.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}