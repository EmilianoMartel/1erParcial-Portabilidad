using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "MapDataSO")]
public class MapDataSO : ScriptableObject
{
    [SerializeField] private int _column = 2;
    [SerializeField] private int _row = 3;
    [SerializeField] private GameObject _cellGameObjectName;

    public int column { get { return _column; } }
    public int row { get { return _row; } }
    public string cellGameObjectName { get { return _cellGameObjectName.name; } }
}
