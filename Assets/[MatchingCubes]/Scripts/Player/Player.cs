using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public bool IsControllable { get; private set; }
    public bool CanMoveForward { get; private set; }

    [HideInInspector]
    public UnityEvent OnPlayerActivated = new UnityEvent();

    private void Awake()
    {        
        Instance = this;        
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStarted.AddListener(ActivatePlayer);
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStarted.RemoveListener(ActivatePlayer);
    }

    private void ActivatePlayer() 
    {
        IsControllable = true;
        CanMoveForward = true;
        OnPlayerActivated.Invoke();
    }
}
