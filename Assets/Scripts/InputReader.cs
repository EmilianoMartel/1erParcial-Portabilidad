using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Action<Vector2> moveEvent = delegate { };

    public void HandleMovement(InputAction.CallbackContext inputContext)
    {
        moveEvent?.Invoke(inputContext.ReadValue<Vector2>());
    }
}