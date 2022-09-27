using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnSceneLoaded = new UnityEvent();
    public static UnityEvent OnRandomGateInteracted = new UnityEvent();
    public static UnityEvent OnOrderGateInteracted = new UnityEvent();
}
