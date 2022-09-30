using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public bool IsJumping { get; private set; }
    public bool IsFailed { get; private set; }
    public bool IsSucceded { get; private set; }
    public bool IsControllable { get; private set; }
    public bool CanMoveForward { get; private set; }

    [HideInInspector]
    public UnityEvent OnPlayerActivated = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPlayerFailed = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPlayerSucceeded = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPlayerJumpingStarted = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPlayerJumpingCompleted = new UnityEvent();

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
        if (IsSucceded || IsFailed)
            return;

        IsFailed = true;
        IsControllable = false;
        CanMoveForward = false;

        OnPlayerFailed.Invoke();
        EventManager.OnLevelFailed.Invoke();
    }

    public void TriggerSuccess() 
    {
        if (IsSucceded || IsFailed)
            return;

        IsSucceded = true;
        IsControllable = false;
        CanMoveForward = false;

        OnPlayerSucceeded.Invoke();
        EventManager.OnLevelCompleted.Invoke();
    }

    private void ActivatePlayer() 
    {
        IsControllable = true;
        CanMoveForward = true;
        OnPlayerActivated.Invoke();
    }

    private void OnJumpingStarted() 
    {
        IsJumping = true;
        CanMoveForward = false;
        OnPlayerJumpingStarted.Invoke();
    }

    private void OnJumpingCompleted() 
    {
        IsJumping = false;
        CanMoveForward = true;
        OnPlayerJumpingCompleted.Invoke();
    }
}
