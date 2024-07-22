using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    [Tooltip("First menu in the list is the default one :)")]
    [SerializeField] private List<MenuDataSource> _menusWithId;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] private MenuDataSource _finalScreenId;

    private int _currentMenuIndex = 0;

    private void Start()
    {
        foreach (var menu in _menusWithId)
        {
            if(menu.DataInstance != null)
            {
                menu.DataInstance.Setup();
                menu.DataInstance.OnChangeMenu += HandleChangeMenu;
                menu.DataInstance.gameObject.SetActive(false);
            }
        }

        if (_menusWithId.Count > 0)
        {
            _menusWithId[_currentMenuIndex].DataInstance.gameObject.SetActive(true);
        }
    }

    private void HandleChangeMenu(string id)
    {
        _gameManager.HandleSpecialEvents(id);
        
        for (var i = 0; i < _menusWithId.Count; i++)
        {
            var menuWithId = _menusWithId[i];
            if (menuWithId.menuId == id)
            {
                _menusWithId[_currentMenuIndex].DataInstance.gameObject.SetActive(false);
                menuWithId.DataInstance.gameObject.SetActive(true);
                _currentMenuIndex = i;
                break;
            }
        }
    }

    private void FinalScreen(bool win)
    {
        for (var i = 0; i < _menusWithId.Count; i++)
        {
            var menuWithId = _menusWithId[i];
            if (menuWithId.menuId == _finalScreenId.menuId)
            {
                _menusWithId[_currentMenuIndex].DataInstance.gameObject.SetActive(false);
                menuWithId.DataInstance.gameObject.SetActive(true);
                _currentMenuIndex = i;
                break;
            }
        }
    }
}