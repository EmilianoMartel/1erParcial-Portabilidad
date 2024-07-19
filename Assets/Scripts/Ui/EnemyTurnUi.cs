using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnUi : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _text;

    public void HandleChangeText(string text)
    {
        _text.text = text;
    }
}