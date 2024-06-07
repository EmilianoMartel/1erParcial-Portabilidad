using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewInstanciator : MonoBehaviour
{
    [SerializeField] private CharacterUI _viewPrefab;
    [SerializeField] private MapManager _mapManager;

    private void OnEnable()
    {
        _mapManager.createdCharacter += HandleStartCharacter;
    }

    private void OnDisable()
    {
        _mapManager.createdCharacter -= HandleStartCharacter;
    }

    private void HandleStartCharacter(Character character)
    {
        var reference = Resources.Load<CharacterUI>("Prefabs/" + _viewPrefab.name);
        var view = Instantiate(reference,transform);
        view.SetCharacter(character);
        view.transform.SetParent(transform);
    }
}