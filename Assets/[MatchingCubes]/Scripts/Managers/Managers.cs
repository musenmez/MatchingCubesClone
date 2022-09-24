using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
