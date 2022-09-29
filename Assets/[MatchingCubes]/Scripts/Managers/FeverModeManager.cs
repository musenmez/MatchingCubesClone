using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverModeManager : Singleton<FeverModeManager>
{
    public bool IsFeverModeEnabled { get; private set; }
    public bool IsFeverModeLocked { get; private set; }

    private const float FEVER_MODE_DURATION = 4f;
   
    private Coroutine _disableCoroutine = null;    

    private void OnEnable()
    {
        EventManager.OnSceneLoaded.AddListener(ResetValues);
        EventManager.OnSpeedUpFloorInteracted.AddListener(EnableFeverMode);
        EventManager.OnRampJumpingStarted.AddListener(OnJumpingStarted);
        EventManager.OnRampJumpingCompleted.AddListener(OnJumpingCompleted);       
    }

    private void OnDisable()
    {
        EventManager.OnSceneLoaded.RemoveListener(ResetValues);
        EventManager.OnSpeedUpFloorInteracted.RemoveListener(EnableFeverMode);
        EventManager.OnRampJumpingStarted.RemoveListener(OnJumpingStarted);
        EventManager.OnRampJumpingCompleted.RemoveListener(OnJumpingCompleted);       
    }
   
  
    private void EnableFeverMode() 
    {
        DisableCountdown();

        if (IsFeverModeEnabled)
            return;

        IsFeverModeEnabled = true;
        EventManager.OnFeverModeEnabled.Invoke();
    }

    private void DisableFeverMode() 
    {
        if (!IsFeverModeEnabled)
            return;

        if (IsFeverModeLocked)
            return;

        IsFeverModeEnabled = false;
        EventManager.OnFeverModeDisabled.Invoke();
    }

    private void DisableCountdown() 
    {
        if (_disableCoroutine != null)
            StopCoroutine(_disableCoroutine);

        _disableCoroutine = Utilities.ExecuteAfterDelay(this, FEVER_MODE_DURATION, () => DisableFeverMode());
    }

    private void OnJumpingStarted() 
    {
        IsFeverModeLocked = true;
        EnableFeverMode();
    }

    private void OnJumpingCompleted() 
    {
        IsFeverModeLocked = false;
        DisableFeverMode();
    }

    private void ResetValues() 
    {
        IsFeverModeEnabled = false;
        IsFeverModeLocked = false;
    }
}
