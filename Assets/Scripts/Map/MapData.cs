using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDatta", menuName = "MapData")]
public class MapData : ScriptableObject
{
    [SerializeField] private int _column = 5;
    [SerializeField] private int _row = 4;
    [SerializeField] private GameObject _cellGameObjectName;
    [SerializeField] private List<CharacterData> _characters;

    public List<CharacterData> characters => _characters;
    public int column { get { return _column; } }
    public int row { get { return _row; } }
    public string cellGameObjectName { get { return _cellGameObjectName.name; } }
}
