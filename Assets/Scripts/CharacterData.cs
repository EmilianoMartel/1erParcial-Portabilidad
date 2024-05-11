using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterDataSO")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _nameCharacter = "Enemy";
    [SerializeField] private bool _isPlayable = false;
    [SerializeField] protected int _life = 10;
    [SerializeField] private int _movement = 1;
}