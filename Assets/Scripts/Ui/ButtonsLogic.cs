using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsLogic : MonoBehaviour
{
    [SerializeField] private ActionButton _buttonPrefab;
    [SerializeField] private List<ActionButton> _buttons;
    [SerializeField] private Transform _buttonsContainer;

    public Action actionPerformedEvent = delegate { };

    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].action -= HandleActionPerformed;
        }
    }

    private void Awake()
    {
        Validate();
    }

    public void SetButtons(List<Character> characters, ActionData data)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (i < characters.Count)
            {
                _buttons[i].gameObject.SetActive(true);
                _buttons[i].CreateListener(characters[i], data);
                _buttons[i].action += HandleActionPerformed;
            }
            else
            {
                _buttons[i].gameObject.SetActive(false);
                _buttons[i].action -= HandleActionPerformed;
            }
        }
        for (int i = _buttons.Count; i < characters.Count; i++)
        {
            var button = CreateNewButton(characters[i], data);
            button.action += HandleActionPerformed;
            _buttons.Add(button);
        }
    }

    private ActionButton CreateNewButton(Character character, ActionData data)
    {
        var reference = Resources.Load<ActionButton>("Prefabs/" + _buttonPrefab.name);
        ActionButton newButton = Instantiate(reference, transform);
        newButton.transform.SetParent(_buttonsContainer);
        newButton.CreateListener(character, data);
        return newButton;
    }

    private void HandleActionPerformed()
    {
        actionPerformedEvent?.Invoke();
    }

    private void Validate()
    {
        if (!_buttonPrefab)
        {
            Debug.Log($"{name}: Button profab is null.\nDisabling component..");
            enabled = false;
            return;
        }
    }
}