using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

public class Enemy
{
    public Action<Vector2> moveAction;

    public void StartTurn(Character character, MapManager manager)
    {
        EnemyTurn(character,manager);
    }

    private IEnumerator EnemyTurn(Character character, MapManager manager)
    {
        while (character.CanMove())
        {
            int x = Random.Range(0,2);
            int y = Random.Range(0, 2);
            if (manager.CheckEmptyMapPosition(character.actualPosition + new Vector2(x,y)))
            {
                character.SetPosition(manager.MoveCharacter(character.actualPosition, character.actualPosition + new Vector2(x, y), character));
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
}
