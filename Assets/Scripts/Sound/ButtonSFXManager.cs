using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFXManager : MonoBehaviour
{
    [SerializeField] private List<ButtonsLogic> _buttons;
    [Tooltip("This list is linked to the other, it must have the desired sounds in the same position")]
    [SerializeField] private List<AudioSource> _buttonsSFX;

    private List<ButtonSFX> _sfxControllers = new();

    private void OnEnable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            ButtonSFX sfx = new ButtonSFX(_buttons[i], _buttonsSFX[i]);
            _sfxControllers.Add(sfx);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _sfxControllers.Count; i++)
        {
            _sfxControllers[i].DestroyButton();
        }
    }

    private void Awake()
    {
        if (_buttons.Count != _buttonsSFX.Count)
        {
            Debug.LogError($"{name}: Both list need to have the same lenght.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}