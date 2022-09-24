using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public bool IsControllable { get; private set; }

    private void Awake()
    {
        Instance = this;
        IsControllable = true;
    }
}
