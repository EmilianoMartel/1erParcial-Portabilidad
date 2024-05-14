using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterDataSO")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _nameCharacter = "Enemy";
    [SerializeField] private Sprite _visual;
    [SerializeField] private bool _isPlayable = false;
    [SerializeField] protected int _life = 10;
    [SerializeField] private int _movement = 1;

    [Header("Mele attack")]
    [SerializeField] private int _damageMele = 0;

    [Header("Range attack")]
    [SerializeField] private int _damageRange = 0;
    [SerializeField] private int _rangeDistance = 0;

    [Header("Health")]
    [SerializeField] private int _pointsToHealth;
    [SerializeField] private bool _selfHealer;

    public string nameCharacter => _nameCharacter;
    public Sprite visual => _visual;
    public bool isPlayable => _isPlayable;
    public int life => _life;
    public int movement => _movement;
    public int damageMele => _damageMele;
    public int damageRange => _damageRange;
    public int rangeDistance => _rangeDistance;
    public int  pointsToHealth => _pointsToHealth;
    public bool selfHealer => _selfHealer;
}