using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private MapDataSO _data;
    [SerializeField] private List<Character> _characters = new();

    private List<List<bool>> _mapList = new();
    private Vector2 _characterPosition = Vector2.zero;

    private void Awake()
    {
        StartMap();
    }

    private void StartMap()
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
        StartCoroutine(PlaceCharacters());
    }

    private IEnumerator PlaceCharacters()
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            yield return StartCoroutine(RandomPosition());
            var character =Instantiate(_characters[i], transform);
            character.transform.localPosition = new Vector3(_characterPosition.x, _characterPosition.y, 1);
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

    [ContextMenu("Debug")]
    private void DebugList()
    {
        for (int i = 0; i < _mapList.Count; i++)
        {
            for (int j = 0; j < _mapList[i].Count; j++)
            {
                Debug.Log("i: " + i+ " j: " + j +" state " +_mapList[i][j]);
            }
        }
    }
}