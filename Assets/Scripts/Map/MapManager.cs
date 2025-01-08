using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    //DATA
    [SerializeField] private int _column = 3;
    [SerializeField] private int _row = 4;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private List<CharacterData> _characterList;

    private List<List<bool>> _mapList = new();
    private Vector2 _characterPosition = Vector2.zero;
    private Dictionary<Character, GameObject> _charactersView = new();

    public Action<Character> createdCharacter = delegate { };
    public Action startGame = delegate { };

    private bool _firstGame = true;

    private void OnEnable()
    {
        _gameManager.startGame += HandleStartGame;
    }

    private void OnDisable()
    {
        _gameManager.startGame -= HandleStartGame;
    }

    private void HandleStartGame()
    {
        if (_firstGame)
        {
            StartCoroutine(StartMap());
            _firstGame = false;
        }
        else
        {
            Restart();
        }    
    }

    private IEnumerator StartMap()
    {
        yield return StartCoroutine(PlaceCells());
        yield return StartCoroutine(PlaceCharacters(_characterList));
        startGame?.Invoke();
    }

    private IEnumerator PlaceCells()
    {
        var reference = Resources.Load<GameObject>("Prefabs/" + _cellPrefab.name);
        for (int i = 0; i < _column; i++)
        {
            List<bool> row = new();
            for (int j = 0; j < _row; j++)
            {
                var gridCell = Instantiate(reference, transform);

                gridCell.transform.localPosition = new Vector3(i, j, 1);
                row.Add(false);
            }
            _mapList.Add(row);
        }
        yield return null;
    }

    private IEnumerator PlaceCharacters(List<CharacterData> list)
    {
        var reference = Resources.Load<GameObject>("Prefabs/" + _cellPrefab.name);
        for (int i = 0; i < list.Count; i++)
        {
            yield return StartCoroutine(RandomPosition());
            var player = InstantiateGameObject(reference);

            if (player.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer sp))
                sp.sprite = _characterList[i].view;

            player.name = _characterList[i].name;
            CreateCharacter(player, _characterList[i]);
        }
    }

    private void CreateCharacter(GameObject gObject, CharacterData data)
    {
        CreateCharacter create = new CreateCharacter();
        Character character = create.CreateNewCharacter(data, new Vector2(_characterPosition.x, _characterPosition.y));
        _charactersView.Add(character,gObject);
        createdCharacter?.Invoke(character);
        character.onDead += HandleCharacterDead;
    }

    private GameObject InstantiateGameObject(GameObject gObject)
    {
        var temp = Instantiate(gObject, transform);
        temp.transform.localPosition = new Vector3(_characterPosition.x, _characterPosition.y, 0);
        return temp;
    }

    private IEnumerator RandomPosition()
    {
        bool isValid = true;
        while (isValid)
        {
            int column = UnityEngine.Random.Range(0, _column);
            int row = UnityEngine.Random.Range(0, _row);

            if (CheckEmptyMapPosition(new Vector2(column, row)))
            {
                _characterPosition = new Vector2(column, row);
                _mapList[column][row] = true;
                isValid = false;
                yield break;
            }
            else
                yield return new WaitForEndOfFrame();
        }
    }

    public bool CheckEmptyMapPosition(Vector2 position)
    {
        if (position.x >= _column || position.y >= _row || position.x < 0 || position.y < 0)
            return false;

        if (!_mapList[(int)position.x][(int)position.y])
            return true;

        return false;
    }

    public Vector2 MoveCharacter(Vector2 actualPosition, Vector2 nextPosition, Character character)
    {
        _mapList[(int)actualPosition.x][(int)actualPosition.y] = false;
        _mapList[(int)nextPosition.x][(int)nextPosition.y] |= true;
        if (_charactersView.ContainsKey(character))
        {
            _charactersView[character].gameObject.transform.localPosition = new Vector3(nextPosition.x, nextPosition.y, -1);
        }
        return nextPosition;
    }

    private void HandleCharacterDead(Character character)
    {
        if (_charactersView.ContainsKey(character))
        {
            character.onDead -= HandleCharacterDead;
            _charactersView[character].SetActive(false);
            _mapList[(int)character.actualPosition.x][(int)character.actualPosition.y] = false;
        }
    }

    private void Restart()
    {
        foreach (List<bool> listBool in _mapList)
        {
            for (int i = 0; i < listBool.Count; i++)
            {
                listBool[i] = false;
            }
        }

        StartCoroutine(RestartPositions());
    }

    private IEnumerator RestartPositions()
    {
        foreach (Character character in _charactersView.Keys)
        {
            yield return StartCoroutine(RandomPosition());
            character.RestartGame();
            character.SetPosition(new Vector2(_characterPosition.x, _characterPosition.y));
            _charactersView[character].gameObject.SetActive(true);
            _charactersView[character].gameObject.transform.localPosition = new Vector3(_characterPosition.x, _characterPosition.y, -1);
        }
    }
}
