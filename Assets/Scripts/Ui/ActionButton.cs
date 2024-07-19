using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ActionButton : MonoBehaviour
{
    private Character _character;
    private ActionData _action;
    [SerializeField] private Button _button;

    private bool _isPressed = false;

    public event Action action;

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void SetActionButton()
    {
        _button.onClick.AddListener(OnClick);
    }

    public void CreateListener(Character character, ActionData actionData)
    {
        _character = character;
        _action = actionData;
        _button.image.sprite = character._characterData.view;
        SetActionButton();
    }

    private void OnClick()
    {
        if (_isPressed)
            return;

        StartCoroutine(ClickAction());
    }

    private IEnumerator ClickAction()
    {
        _isPressed = true;
        yield return new WaitForSeconds(0.5f);
        _character.ReciveLifeChanger(_action.lifeModifier);
        _isPressed = false;
        action?.Invoke();
    }
}