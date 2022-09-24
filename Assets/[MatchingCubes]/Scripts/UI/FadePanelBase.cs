using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System;


public abstract class FadePanelBase : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public CanvasGroup CanvasGroup => _canvasGroup == null ? _canvasGroup = GetComponentInParent<CanvasGroup>() : _canvasGroup;
    public bool IsPanelShowing { get; protected set; }    

    protected const float MAX_FADE = 1f;
    protected const float MIN_FADE = 0f;   

    protected string _fadeTweenID;

    protected virtual void Awake()
    {        
        _fadeTweenID = GetInstanceID() + "FadeTweenID";
    }

    public virtual void ShowPanelWithFade(float duration, float delay)
    {    
        FadeTween(MAX_FADE, duration, delay, ShowPanel);     
    }

    public virtual void HidePanelWithFade(float duration, float delay)
    {        
        FadeTween(MIN_FADE, duration, delay, HidePanel);       
    }

    public virtual void ShowPanel()
    {        
        IsPanelShowing = true;

        CanvasGroup.alpha = 1;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;     
    }

    public virtual void HidePanel()
    {        
        IsPanelShowing = false;

        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }
    public void TogglePanel()
    {
        DOTween.Kill(_fadeTweenID);

        if (CanvasGroup.interactable)
            HidePanel();
        else
            ShowPanel();
    }

    protected void FadeTween(float endValue, float duration, float delay,Action onComplete = null)
    {
        DOTween.Kill(_fadeTweenID);
        CanvasGroup.DOFade(endValue, duration).SetId(_fadeTweenID).SetDelay(delay).SetEase(Ease.Linear).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}
