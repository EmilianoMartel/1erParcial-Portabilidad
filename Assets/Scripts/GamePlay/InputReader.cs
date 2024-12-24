using EventChannel;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private TurnController _controller;
    [SerializeField] private Vector2Channel _moveChannel;

    private bool _isPlayeble;

    public Action<Vector2> moveEvent = delegate { };

    private void OnEnable()
    {
        _controller.actualCharacter += HandleIsPlayeable;
    }

    private void OnDisable()
    {
        _controller.actualCharacter -= HandleIsPlayeable;
    }

    private void Awake()
    {
        if (!_controller)
        {
            Debug.LogError($"{name}: Controller is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    public void HandleUpEvent(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started && _isPlayeble)
        {
            moveEvent?.Invoke(new Vector2(0, 1));
            _moveChannel.InvokeEvent(new Vector2(0, 1));
        }
            
    }

    public void HandleDownEvent(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started && _isPlayeble)
        {
            moveEvent?.Invoke(new Vector2(0, -1));
            _moveChannel.InvokeEvent(new Vector2(0, -1));
        }
    }

    public void HandleRightEvent(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started && _isPlayeble)
        {
            moveEvent?.Invoke(new Vector2(1, 0));
            _moveChannel.InvokeEvent(new Vector2(1, 0));
        }
    }

    public void HandleLeftEvent(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started && _isPlayeble)
        {
            moveEvent?.Invoke(new Vector2(-1, 0));
            _moveChannel.InvokeEvent(new Vector2(-1, 0));
        }
    }

    public void HandleIsPlayeable(Character character)
    {
        _isPlayeble = character._characterData.isPlayable;
    }
}