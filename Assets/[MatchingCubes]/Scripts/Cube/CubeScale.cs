using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeScale : MonoBehaviour
{
    private Cube _cube;
    private Cube Cube => _cube == null ? _cube = GetComponentInParent<Cube>() : _cube;

    [SerializeField] private Transform _scaleBody;

    private const float MIN_SCALE_MULTIPLIER = 0.01f;
    private const float SCALE_DURATION = 0.3f;
    private const Ease SCALE_EASE = Ease.OutBack;

    private const float PUNCH_STRENGTH = 0.1f;
    private const float PUNCH_DURATION = 0.3f;
    private const Ease PUNCH_EASE = Ease.InOutSine;

    private string _scaleTweenID;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _scaleTweenID = GetInstanceID() + "ScaleTweenID";
        _defaultScale = _scaleBody.localScale;
    }

    private void OnEnable()
    {
        Cube.OnCollected.AddListener(OnCollected);
    }

    private void OnDisable()
    {
        Cube.OnCollected.RemoveListener(OnCollected);
    }

    private void OnCollected() 
    {
        Vector3 from = _defaultScale * MIN_SCALE_MULTIPLIER;
        Vector3 to = _defaultScale;
        ScaleTween(from, to, SCALE_DURATION);      
    }

    private void ScaleTween(Vector3 from, Vector3 to, float duration) 
    {        
        DOTween.Kill(_scaleTweenID);
        _scaleBody.localScale = from;
        _scaleBody.DOScale(to, duration).SetEase(SCALE_EASE).SetId(_scaleTweenID);
    }

    private void PunchScaleTween() 
    {
        DOTween.Complete(_scaleTweenID);
        _scaleBody.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION, vibrato: 1).SetId(_scaleTweenID).SetEase(PUNCH_EASE);
    }
}
