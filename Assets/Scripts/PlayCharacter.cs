using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayCharacter : Character
{
    private bool _isMoving = false;

    private void HandleInputMovement(Vector2 dir)
    {
        if (!_isMoving)
            StartCoroutine(TryToMove(dir));
    }

    private IEnumerator TryToMove(Vector2 dir)
    {
        _isMoving = true;
        yield return new WaitForSeconds(.5f);
        Movement(dir);
        _isMoving = false;
    }
}