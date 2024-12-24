using EventChannel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { LEFT, RIGTH, UP, DOWN }
public class MoveJoystick : MonoBehaviour
{
    [SerializeField] Transform directionIcon;
    [SerializeField] private Vector2Channel _moveChannel;
    public bool isGoingLeft {get; private set;}
    public bool isGoingRight {get; private set;}
    public bool isGoingUp {get; private set;}
    public bool isGoingDown {get; private set;}

    public void OnMouseEnter_Left()
    {
        isGoingLeft = true;
        _moveChannel.InvokeEvent(new Vector2(-1, 0));
    }

    public void OnMouseExit_Left()
    {
        isGoingLeft = false;
        _moveChannel.InvokeEvent(new Vector2(0, 0));
    }

    public void OnMouseEnter_Right()
    {
        isGoingRight = true;
        _moveChannel.InvokeEvent(new Vector2(1, 0));
    }

    public void OnMouseExit_Right()
    {
        isGoingRight = false;
        _moveChannel.InvokeEvent(new Vector2(0, 0));
    }

    public void OnMouseEnter_Up()
    {
        isGoingUp = true;
        _moveChannel.InvokeEvent(new Vector2(0, 1));
    }

    public void OnMouseExit_Up()
    {
        isGoingUp = false;
        _moveChannel.InvokeEvent(new Vector2(0, 0));
    }

    public void OnMouseEnter_Down()
    {
        isGoingDown = true;
        _moveChannel.InvokeEvent(new Vector2(0, -1));
    }

    public void OnMouseExit_Down()
    {
        isGoingDown = false;
        _moveChannel.InvokeEvent(new Vector2(0, 0));
    }

    private void Update()
    {
        float posX = 0;
        if (isGoingLeft) posX = -50;
        else if (isGoingRight) posX = 50;

        float posY = 0;
        if (isGoingUp) posY = 50;
        else if (isGoingDown) posY = -50;
        
        directionIcon.localPosition = new Vector2(posX, posY);
    }
}
