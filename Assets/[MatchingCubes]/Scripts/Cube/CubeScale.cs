using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeScale : MonoBehaviour
{
    private Cube _cube;
    private Cube Cube => _cube == null ? _cube = GetComponentInParent<Cube>() : _cube;

    [SerializeField] private Transform _body;
    [SerializeField] private Transform _visual;

    private const float MIN_SCALE_MULTIPLIER = 0.01f;
    private const float SCALE_DURATION = 0.3f;
    private const Ease SCALE_EASE = Ease.OutBack;

    private const float PUNCH_STRENGTH = 0.2f;
    private const float PUNCH_DURATION = 0.3f;
    private const Ease PUNCH_EASE = Ease.InOutSine;

    private string _scaleTweenID;
    private string _punchTweenID;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _scaleTweenID = GetInstanceID() + "ScaleTweenID";
        _punchTweenID = GetInstanceID() + "PunchTweenID";

        _defaultScale = _body.localScale;
    }

    private void OnEnable()
    {
        Cube.OnCollected.AddListener(OnCollected);
        Cube.OnMatched.AddListener(OnMatched);
    }

    private void OnDisable()
    {
        Cube.OnCollected.RemoveListener(OnCollected);
        Cube.OnMatched.RemoveListener(OnMatched);
    }

    private void OnCollected() 
    {
        Vector3 from = _defaultScale * MIN_SCALE_MULTIPLIER;
        Vector3 to = _defaultScale;
        ScaleTween(from, to, SCALE_DURATION);      
    }

    private void OnMatched() 
    {
        KillTweens();
        Sequence matchSequence = DOTween.Sequence();
        matchSequence.Append(_visual.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION, vibrato: 1).SetId(_punchTweenID).SetEase(PUNCH_EASE))
        .Append(_visual.DOScale(_defaultScale * MIN_SCALE_MULTIPLIER, SCALE_DURATION).SetEase(Ease.Linear).SetId(_scaleTweenID)).OnComplete(() => Cube.DestroyCube());
    }

    private void ScaleTween(Vector3 from, Vector3 to, float duration) 
    {
        KillTweens();
        _body.localScale = from;
        _body.DOScale(to, duration).SetEase(SCALE_EASE).SetId(_scaleTweenID);
    }

    private void PunchScaleTween() 
    {
        KillTweens();
        _body.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION, vibrato: 1).SetId(_punchTweenID).SetEase(PUNCH_EASE);
    }

    private void KillTweens() 
    {
        DOTween.Kill(_scaleTweenID);
        DOTween.Complete(_punchTweenID);
    }
}
