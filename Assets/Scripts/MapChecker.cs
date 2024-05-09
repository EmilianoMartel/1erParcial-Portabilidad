using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChecker
{
    private List<List<bool>> _mapList = new();

    public bool CheckEmptyMapPosition(Vector2 position)
    {
        if (!_mapList[(int)position.x][(int)position.y])
            return true;

        return false;
    }
}