using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { LEFT, RIGTH, UP, DOWN }
public class MoveJoystick : MonoBehaviour
{
    [SerializeField] Transform directionIcon;
    public bool isGoingLeft {get; private set;}
    public bool isGoingRight {get; private set;}
    public bool isGoingUp {get; private set;}
    public bool isGoingDown {get; private set;}

    public void OnMouseEnter_Left()
    {
        isGoingLeft = true;
    }

    public void OnMouseExit_Left()
    {
        isGoingLeft = false;
    }

    public void OnMouseEnter_Right()
    {
        isGoingRight = true;
    }

    public void OnMouseExit_Right()
    {
        isGoingRight = false;
    }

    public void OnMouseEnter_Up()
    {
        isGoingUp = true;
    }

    public void OnMouseExit_Up()
    {
        isGoingUp = false;
    }

    public void OnMouseEnter_Down()
    {
        isGoingDown = true;
    }

    public void OnMouseExit_Down()
    {
        isGoingDown = false;
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
