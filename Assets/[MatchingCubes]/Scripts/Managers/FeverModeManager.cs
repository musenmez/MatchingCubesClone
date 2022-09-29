using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverModeManager : Singleton<FeverModeManager>
{
    public bool IsFeverModeEnabled { get; private set; }
    public bool IsFeverModeLocked { get; private set; }

    private const float FEVER_MODE_DURATION = 4f;

    private const float MATCH_COUNT_RESET_DELAY = 4f;
    private const int MATCH_COUNT_THRESHOLD = 3;
    
    private Coroutine _disableCoroutine = null;
    private Coroutine _matchCountCoroutine = null;

    private int _currentMatchCount;

    private void OnEnable()
    {
        EventManager.OnSceneLoaded.AddListener(ResetValues);
        EventManager.OnSpeedUpFloorInteracted.AddListener(EnableFeverMode);
        EventManager.OnRampJumpingStarted.AddListener(OnJumpingStarted);
        EventManager.OnRampJumpingCompleted.AddListener(OnJumpingCompleted);
        EventManager.OnPlayerMatchedCubes.AddListener(CheckMatchCount);
    }

    private void OnDisable()
    {
        EventManager.OnSceneLoaded.RemoveListener(ResetValues);
        EventManager.OnSpeedUpFloorInteracted.RemoveListener(EnableFeverMode);
        EventManager.OnRampJumpingStarted.RemoveListener(OnJumpingStarted);
        EventManager.OnRampJumpingCompleted.RemoveListener(OnJumpingCompleted);
        EventManager.OnPlayerMatchedCubes.RemoveListener(CheckMatchCount);
    }
    
    private void CheckMatchCount() 
    {
        MatchCountResetCountdown();

        _currentMatchCount++;
        if (_currentMatchCount >= MATCH_COUNT_THRESHOLD)
        {
            ResetMatchCount();
            EnableFeverMode();
        }
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

    private void MatchCountResetCountdown() 
    {
        if (_matchCountCoroutine != null)
            StopCoroutine(_matchCountCoroutine);

        _matchCountCoroutine = Utilities.ExecuteAfterDelay(this, MATCH_COUNT_RESET_DELAY, () => ResetMatchCount());
    }

    private void ResetMatchCount() 
    {
        _currentMatchCount = 0;
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
