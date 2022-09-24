using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    public bool IsLevelStarted { get; private set; }

    [HideInInspector]
    public UnityEvent OnLevelStarted = new UnityEvent();

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void StartLevel() 
    {
        if (IsLevelStarted)
            return;

        IsLevelStarted = true;
        OnLevelStarted.Invoke();
    }
}
