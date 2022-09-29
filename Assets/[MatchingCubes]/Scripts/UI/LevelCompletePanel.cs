using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletePanel : FadePanelBase
{
    private const float FADE_DURATION = 0.25f;

    private void OnEnable()
    {
        EventManager.OnLevelCompleted.AddListener(() => ShowPanelWithFade(FADE_DURATION));
        EventManager.OnSceneLoading.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        EventManager.OnLevelCompleted.RemoveListener(() => ShowPanelWithFade(FADE_DURATION));
        EventManager.OnSceneLoading.RemoveListener(HidePanel);
    }

    public void NextButtonAction()
    {
        LevelManager.Instance.RestartLevel();
    }
}
