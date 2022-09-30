using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private TextMeshProUGUI TextMesh => _textMesh == null ? _textMesh = GetComponentInChildren<TextMeshProUGUI>() : _textMesh;

    private void Start()
    {
        SetText();
    }

    private void OnEnable()
    {
        EventManager.OnCoinDataChanged.AddListener(SetText);
    }

    private void OnDisable()
    {
        EventManager.OnCoinDataChanged.RemoveListener(SetText);
    }

    private void SetText() 
    {
        string text = GameManager.Instance.Coin.ToString();
        TextMesh.SetText(text);
    }
}
