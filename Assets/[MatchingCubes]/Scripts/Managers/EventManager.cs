using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnSceneLoaded = new UnityEvent();

    public static UnityEvent OnRandomGateInteracted = new UnityEvent();
    public static UnityEvent OnOrderGateInteracted = new UnityEvent();

    public static UnityEvent OnRampJumpingStarted = new UnityEvent();
    public static UnityEvent OnRampJumpingCompleted = new UnityEvent();

    public static UnityEvent OnSpeedUpFloorInteracted = new UnityEvent();

    public static UnityEvent OnFeverModeEnabled = new UnityEvent();
    public static UnityEvent OnFeverModeDisabled = new UnityEvent();
}
