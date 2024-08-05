using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX
{
    private ButtonsLogic _button;
    private AudioSource _audio;

    public ButtonSFX(ButtonsLogic actionButtons, AudioSource audio)
    {
        _button = actionButtons;
        _audio = audio;
        _button.actionPerformedEvent += HandlePlaySound;
    }

    private void HandlePlaySound()
    {
        _audio.Play();
    }

    public void DestroyButton()
    {
        _button.actionPerformedEvent -= HandlePlaySound;
    }
}
