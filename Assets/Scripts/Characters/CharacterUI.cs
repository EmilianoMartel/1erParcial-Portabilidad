using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Image _view;
    [SerializeField] private TMPro.TMP_Text _lifeText;

    private Character _character;

    private void OnDisable()
    {
        if(_character != null)
        {
            _character.onLifeChange -= HandleLifeChange;
            _character.onDead -= HandleOnDead;
        }
    }

    private void Awake()
    {
        Validate();
    }

    public void SetCharacter(Character character)
    {
        _view.sprite = character._characterData.view;
        _lifeText.text = character.currentLife.ToString();
        character.onLifeChange += HandleLifeChange;
        character.onDead += HandleOnDead;
    }

    private void HandleLifeChange(int life)
    {
        _lifeText.text = life.ToString();
    }

    private void HandleOnDead(Character character)
    {
        gameObject.SetActive(false);
    }

    private void Validate()
    {
        if (!_view)
        {
            Debug.LogError($"{name}: View is null.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_lifeText)
        {
            Debug.LogError($"{name}: LifeText is null.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}
