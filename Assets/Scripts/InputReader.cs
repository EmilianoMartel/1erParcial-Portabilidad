using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Action<Vector2> moveEvent = delegate { };

    public void HandleUpEvent(InputAction.CallbackContext inputContext)
    {
        moveEvent?.Invoke(new Vector2(0,1));
    }

    public void HandleDownEvent(InputAction.CallbackContext inputContext)
    {
        moveEvent?.Invoke(new Vector2(0, -1));
    }

    public void HandleRightEvent(InputAction.CallbackContext inputContext)
    {
        moveEvent?.Invoke(new Vector2(1, 0));
    }

    public void HandleLeftEvent(InputAction.CallbackContext inputContext)
    {
        moveEvent?.Invoke(new Vector2(-1, 0));
    }
}