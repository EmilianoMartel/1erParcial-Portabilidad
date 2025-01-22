using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Image _view;
    [SerializeField] private TMPro.TMP_Text _lifeText;
    [SerializeField] private TMPro.TMP_Text _textTitle;

    private Character _character;

    private void OnDisable()
    {
        if(_character != null)
        {
            _character.onLifeChange -= HandleLifeChange;
            _character.onDead -= HandleOnDead;
            _character.reset -= HandleReset;
            _character.revive -= HandleReset;
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
        character.reset += HandleReset;
        character.revive += HandleReset;
        _character = character;
    }

    private void HandleLifeChange(int life)
    {
        _lifeText.text = life.ToString();
    }

    private void HandleOnDead(Character character)
    {
        _view.gameObject.SetActive(false);
        _lifeText.gameObject.SetActive(false);
        _textTitle.gameObject.SetActive(false);
    }

    private void HandleReset(Character character)
    {
        _view.gameObject.SetActive(true);
        _lifeText.gameObject.SetActive(true);
        _textTitle.gameObject.SetActive(true);
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
