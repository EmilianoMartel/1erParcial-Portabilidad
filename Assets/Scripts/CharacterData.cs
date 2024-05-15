using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _nameCharacter;
    [SerializeField] private Sprite _view;

    [SerializeField] private bool _isPlayable = false;
    [SerializeField] protected int _life = 10;
    [SerializeField] private int _movement = 1;

    [SerializeField] private List<ActionData> _effectData;

    public string nameCharacter => _nameCharacter;
    public Sprite view => _view;
    public bool isPlayable => _isPlayable;
    public int life => _life;
    public int movement => _movement;
    public List<ActionData> effectData => _effectData;
}