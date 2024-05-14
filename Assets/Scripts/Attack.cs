using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public int damage;
    public int range;
    public int minRange;

    public virtual List<Character> CheckCanAttack(Vector2 position)
    {
        List<Character> list = new List<Character>();
        for (int i = -range; i < range; i++)
        {
            for (int j = -range; j < range; j++)
            {
                if (Math.Abs(i) == minRange && Math.Abs(j) == minRange)
                {
                    // Ignorar los valores en el área central de 3x3
                    continue;
                }
                new Vector2(i, j);
                //Check if i and j is empty
            }
        }
        return list;
    }

    public virtual int Damage() { return damage; }
}