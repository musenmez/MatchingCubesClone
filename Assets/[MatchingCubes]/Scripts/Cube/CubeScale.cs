using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CubeScale : MonoBehaviour
{
    private Cube _cube;
    private Cube Cube => _cube == null ? _cube = GetComponentInParent<Cube>() : _cube;

    private CubeHit _cubeHit;
    private CubeHit CubeHit => _cubeHit == null ? _cubeHit = GetComponentInParent<CubeHit>() : _cubeHit;

    [SerializeField] private Transform _body;
    [SerializeField] private Transform _visual;

    private const float MIN_SCALE_MULTIPLIER = 0.01f;
    private const float SCALE_DURATION = 0.2f;
    private const Ease SCALE_EASE = Ease.Linear;

    private const float LAVA_SCALE_DURATION = 0.4f;
    private const Ease LAVA_SCALE_EASE = Ease.Linear;

    private const float PUNCH_STRENGTH = 0.2f;
    private const float PUNCH_DURATION = 0.3f;
    private const Ease PUNCH_EASE = Ease.InOutSine;

    private string _scaleTweenID;  
    private Vector3 _defaultScale;

    private void Awake()
    {
        _scaleTweenID = GetInstanceID() + "ScaleTweenID";
        _defaultScale = _body.localScale;
    }

    private void OnEnable()
    {
        Cube.OnCollected.AddListener(OnCollected);
        Cube.OnMatched.AddListener(OnMatched);
        CubeHit.OnHitLava.AddListener(OnHitLava);
    }

    private void OnDisable()
    {
        Cube.OnCollected.RemoveListener(OnCollected);
        Cube.OnMatched.RemoveListener(OnMatched);
        CubeHit.OnHitLava.RemoveListener(OnHitLava);
    }

    private void OnCollected() 
    {
        Vector3 from = _defaultScale * MIN_SCALE_MULTIPLIER;
        Vector3 to = _defaultScale;
        ScaleTween(_body, from, to, SCALE_DURATION);      
    }

    private void OnHitLava() 
    {
        Vector3 from = transform.localScale;
        Vector3 to = _defaultScale * MIN_SCALE_MULTIPLIER;
        ScaleTween(_body, from, to, LAVA_SCALE_DURATION, ease: LAVA_SCALE_EASE, onComplete: Cube.DestroyCube);
    }

    private void OnMatched() 
    {
        DOTween.Kill(_scaleTweenID);
        Sequence matchSequence = DOTween.Sequence();
        matchSequence.Append(_visual.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION, vibrato: 1).SetId(_scaleTweenID).SetEase(PUNCH_EASE))
        .Append(_visual.DOScale(_defaultScale * MIN_SCALE_MULTIPLIER, SCALE_DURATION).SetEase(Ease.Linear).SetId(_scaleTweenID)).OnComplete(Cube.DestroyCube).SetId(_scaleTweenID);
    }

    private void ScaleTween(Transform target, Vector3 from, Vector3 to, float duration, Ease ease = SCALE_EASE, Action onComplete = null) 
    {
        DOTween.Kill(_scaleTweenID);
        target.localScale = from;
        target.DOScale(to, duration).SetEase(ease).SetId(_scaleTweenID).OnComplete(() => onComplete?.Invoke());
    }  
}
