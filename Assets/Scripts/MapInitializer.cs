using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class MapInitializer : MonoBehaviour
{
    [SerializeField] private LevelData _data;

    private List<List<bool>> _mapList = new();
    private Vector2 _characterPosition = Vector2.zero;

    public Action<Character> createdCharacter = delegate { };
    public Action setGame = delegate { };

    private void Awake()
    {
        StartCoroutine(StartMap());
    }

    private IEnumerator StartMap()
    {
        yield return StartCoroutine(PlaceCells());
        yield return StartCoroutine(PlaceCharacters(_data.players));
        yield return StartCoroutine(PlaceCharacters(_data.enemies));
        setGame?.Invoke();
    }

    private IEnumerator PlaceCells()
    {
        var reference = Resources.Load<GameObject>("Prefabs/" + _data.cellGameObjectName);
        for (int i = 0; i < _data.column; i++)
        {
            List<bool> row = new();
            for (int j = 0; j < _data.row; j++)
            {
                var gridCell = Instantiate(reference, transform);

                gridCell.transform.localPosition = new Vector3(i, j, 1);
                row.Add(false);
            }
            _mapList.Add(row);
        }
        yield return null;
    }

    private IEnumerator PlaceCharacters(List<Character> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            yield return StartCoroutine(RandomPosition());
            var player =Instantiate(list[i], transform);
            player.transform.localPosition = new Vector3(_characterPosition.x, _characterPosition.y, 1);
            player.SetFirstPosition(new Vector2(_characterPosition.x, _characterPosition.y));
            createdCharacter?.Invoke(player);
        }
    }

    private IEnumerator RandomPosition()
    {
        bool isValid = true;
        while (isValid)
        {
            int column = UnityEngine.Random.Range(0, _data.column);
            int row = UnityEngine.Random.Range(0, _data.row);

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
        if (!_mapList[(int)position.x][(int)position.y])
            return true;

        return false;
    }
}