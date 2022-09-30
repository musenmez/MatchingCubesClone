using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : FadePanelBase
{
    private const float HIDE_DURATION = 0.25f;
    private const float HIDE_DELAY = 1f;   

    protected override void Awake()
    {
        base.Awake();
        ShowPanel();
    }

    private void OnEnable()
    {
        EventManager.OnSceneLoaded.AddListener(OnSceneLoaded);
    }

    private void OnDisable()
    {
        EventManager.OnSceneLoaded.RemoveListener(OnSceneLoaded);
    }

    private void OnSceneLoaded() 
    {
        HidePanelWithFade(HIDE_DURATION, HIDE_DELAY);
    }
}
