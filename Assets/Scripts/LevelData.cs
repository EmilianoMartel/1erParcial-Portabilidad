using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int _column = 2;
    [SerializeField] private int _row = 3;
    [SerializeField] private GameObject _cellGameObjectName;
    [SerializeField] private List<Character> _enemies;
    [SerializeField] private List<Character> _players;

    public List<Character> enemies => _enemies;
    public List<Character> players => _players;
    public int column { get { return _column; } }
    public int row { get { return _row; } }
    public string cellGameObjectName { get { return _cellGameObjectName.name; } }
}
