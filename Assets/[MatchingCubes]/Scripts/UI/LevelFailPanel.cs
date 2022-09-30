using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailPanel : FadePanelBase
{
    private const float FADE_DURATION = 0.25f;   

    private void OnEnable()
    {
        EventManager.OnLevelFailed.AddListener(() => ShowPanelWithFade(FADE_DURATION));
        EventManager.OnSceneLoading.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        EventManager.OnLevelFailed.RemoveListener(() => ShowPanelWithFade(FADE_DURATION));
        EventManager.OnSceneLoading.RemoveListener(HidePanel);
    }

    public void RetryButtonAction() 
    {
        LevelManager.Instance.RestartLevel();
    }
}
