using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsTurnUi : MonoBehaviour
{
    [SerializeField] private Image _photo;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private GameController _gameConntroller;

    [SerializeField] private ButtonsLogic _melee;
    [SerializeField] private ButtonsLogic _range;
    [SerializeField] private ButtonsLogic _health;

    private bool _canDoAction = true;

    private void OnEnable()
    {
        _turnController.actualCharacter += HandleCharacter;
        _turnController.startEndTurn += HandleEndTurn;
        _gameConntroller.meleeAttackEvent += HandleMeleeAttack;
        _gameConntroller.rangeAttackEvent += HanldeRangeAttack;
        _gameConntroller.healingEvent += HandleHealth;
        _gameConntroller.enemyTurnEvent += HandleEnemyTurn;
        _melee.actionPerformedEvent += HandleActionPerformed;
        _range.actionPerformedEvent += HandleActionPerformed;
        _health.actionPerformedEvent += HandleActionPerformed;
    }

    private void OnDisable()
    {
        _turnController.actualCharacter -= HandleCharacter;
        _turnController.startEndTurn -= HandleEndTurn;
        _gameConntroller.meleeAttackEvent -= HandleMeleeAttack;
        _gameConntroller.rangeAttackEvent -= HanldeRangeAttack;
        _gameConntroller.enemyTurnEvent -= HandleEnemyTurn;
        _gameConntroller.healingEvent -= HandleHealth;
        _melee.actionPerformedEvent -= HandleActionPerformed;
        _range.actionPerformedEvent -= HandleActionPerformed;
        _health.actionPerformedEvent -= HandleActionPerformed;
    }

    private void Awake()
    {
        Validate();
    }

    private void HandleCharacter(Character character)
    {
        _photo.sprite = character._characterData.view;
    }

    private void HandleMeleeAttack(List<Character> list, ActionData data)
    {
        SetButtonsLogic(_melee, list, data);
    }

    private void HanldeRangeAttack(List<Character> list, ActionData data)
    {
        SetButtonsLogic(_range, list, data);
    }

    private void HandleHealth(List<Character> list, ActionData data)
    {
        SetButtonsLogic(_health, list, data);
    }

    private void SetButtonsLogic(ButtonsLogic logic, List<Character> list, ActionData data)
    {
        if (!_canDoAction)
            return;

        if (list.Count <= 0)
        {
            logic.gameObject.SetActive(false);
            return;
        }

        logic.gameObject.SetActive(true);
        logic.SetButtons(list, data);
    }

    private void HandleEnemyTurn(Character character)
    {
        HandleActionPerformed();
    }

    private void HandleActionPerformed()
    {
        _health.gameObject.SetActive(false);
        _melee.gameObject.SetActive(false);
        _range.gameObject.SetActive(false);
        _canDoAction = false;
    }

    private void HandleEndTurn()
    {
        _canDoAction = true;
    }

    private void Validate()
    {
        if (!_turnController)
        {
            Debug.LogError($"{name}: Turn controller is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_gameConntroller)
        {
            Debug.LogError($"{name}: Game controller is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_melee)
        {
            Debug.LogError($"{name}: Melee is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_range)
        {
            Debug.LogError($"{name}: Range is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_health)
        {
            Debug.LogError($"{name}: Health is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}