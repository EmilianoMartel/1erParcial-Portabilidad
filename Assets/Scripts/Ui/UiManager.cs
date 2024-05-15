using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Canvas _endCanvas;
    [SerializeField] private Canvas _lifeCanvas;
    [SerializeField] private TMPro.TMP_Text _finalText;

    [SerializeField] private GameManager _gameManager;

    private void OnEnable()
    {
        _gameManager.winnerEvent += HandleEndGame;
    }

    private void OnDisable()
    {
        _gameManager.winnerEvent -= HandleEndGame;
    }

    private void Awake()
    {
        Validate();
        _endCanvas.gameObject.SetActive(false);
    }

    private void HandleEndGame(bool isWinning, Character  winCharacter)
    {
        _endCanvas.gameObject.SetActive(true);
        _gameCanvas.gameObject.SetActive(false);
        _lifeCanvas.gameObject.SetActive(false);
        if (!isWinning)
            _finalText.text = "Loose";
        else
            _finalText.text = winCharacter._characterData.name + " Wins!";
    }

    private void Validate()
    {
        if (!_gameCanvas)
        {
            Debug.LogError($"{name}: Game Canvas is null.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_endCanvas)
        {
            Debug.LogError($"{name}: End Canvas is null.\nDisabling component.");
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
