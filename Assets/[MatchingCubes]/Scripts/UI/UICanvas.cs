using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public static UICanvas Instance;

    private Canvas _canvas;
    public Canvas Canvas => _canvas == null ? _canvas = GetComponentInChildren<Canvas>() : _canvas;

    private void Awake()
    {
        Instance = this;
    }
}
