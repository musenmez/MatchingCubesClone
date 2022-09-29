using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public bool IsFailed { get; private set; }
    public bool IsControllable { get; private set; }
    public bool CanMoveForward { get; private set; }

    [HideInInspector]
    public UnityEvent OnPlayerActivated = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPlayerFailed = new UnityEvent();

    private void Awake()
    {        
        Instance = this;        
    }

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.OnLevelStarted.AddListener(ActivatePlayer);
        EventManager.OnRampJumpingStarted.AddListener(OnJumpingStarted);
        EventManager.OnRampJumpingCompleted.AddListener(OnJumpingCompleted);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.OnLevelStarted.RemoveListener(ActivatePlayer);
        EventManager.OnRampJumpingStarted.RemoveListener(OnJumpingStarted);
        EventManager.OnRampJumpingCompleted.RemoveListener(OnJumpingCompleted);
    }  

    public void TriggerFail() 
    {
        if (IsFailed)
            return;

        IsFailed = true;
        IsControllable = false;
        CanMoveForward = false;
        OnPlayerFailed.Invoke();
    }

    private void ActivatePlayer() 
    {
        IsControllable = true;
        CanMoveForward = true;
        OnPlayerActivated.Invoke();
    }

    private void OnJumpingStarted() 
    {       
        CanMoveForward = false;
    }

    private void OnJumpingCompleted() 
    {                                 
        CanMoveForward = true;
    }
}
