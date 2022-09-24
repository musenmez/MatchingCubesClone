using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader
{
    private const string MANAGERS_PATH = "Managers";
    private const string UI_PATH = "UI";

    [RuntimeInitializeOnLoadMethod((RuntimeInitializeLoadType.BeforeSceneLoad))]
    static void OnBeforeSceneLoad()
    {
        GameObject.Instantiate(Resources.Load<GameObject>(MANAGERS_PATH));
        GameObject.Instantiate(Resources.Load<GameObject>(UI_PATH));
        
        Debug.Log("Before Scene is loaded and game is running");
    }

    [RuntimeInitializeOnLoadMethod((RuntimeInitializeLoadType.AfterSceneLoad))]
    static void OnAfterSceneLoad()
    {
        EventManager.OnSceneLoaded.Invoke();
        Debug.Log("After Scene is loaded and game is running");
    }
}
