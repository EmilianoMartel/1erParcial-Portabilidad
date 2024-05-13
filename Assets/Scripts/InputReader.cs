using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private GameController _controller;
    private bool _isPlayeble;

    public Action<Vector2> moveEvent = delegate { };

    private void OnEnable()
    {
        _controller.isPlayeble += HandleIsPlayeable;
    }

    private void OnDisable()
    {
        _controller.isPlayeble -= HandleIsPlayeable;
    }

    public void HandleUpEvent(InputAction.CallbackContext inputContext)
    {
        if(inputContext.started && _isPlayeble)
            moveEvent?.Invoke(new Vector2(0,1));
    }

    public void HandleDownEvent(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started && _isPlayeble)
            moveEvent?.Invoke(new Vector2(0, -1));
    }

    public void HandleRightEvent(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started && _isPlayeble)
            moveEvent?.Invoke(new Vector2(1, 0));
    }

    public void HandleLeftEvent(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started && _isPlayeble)
            moveEvent?.Invoke(new Vector2(-1, 0));
    }

    public void HandleIsPlayeable(bool isPlayable)
    {
        _isPlayeble = isPlayable;
    }
}