using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Ramp : InteractableBase
{
    [SerializeField] private Transform _startPoint;  
    [SerializeField] private Transform _lastPoint;
    [Space(10)]
    [SerializeField] private Transform _movementParent;

    private const Ease MOVEMENT_EASE = Ease.Linear;
    private const float MOVEMENT_SPEED = 10f;

    private const float PATH_HEIGHT = 8f;
    private const int PATH_RESOLUTION = 20;
   
    private string _pathTweenID;

    private void Awake()
    {
        _movementParent.position = _startPoint.position;
        _pathTweenID = GetInstanceID() + "PathTweenID";
    }

    public override void InteractAction()
    {        
        PathTween(onStart: StartJumping, onComplete: CompleteJumping);
    }

    private void StartJumping() 
    {        
        Interactor.Body.SetParent(_movementParent);
        EventManager.OnRampJumpingStarted.Invoke();
    }

    private void CompleteJumping() 
    {
        Interactor.Body.SetParent(null);
        EventManager.OnRampJumpingCompleted.Invoke();
    }

    private void PathTween(Action onStart, Action onComplete) 
    {
        Vector3[] path = GetPath();
        DOTween.Kill(_pathTweenID);
        _movementParent.DOPath(path, MOVEMENT_SPEED).SetSpeedBased(true).SetEase(MOVEMENT_EASE).SetId(_pathTweenID).OnStart(() => onStart?.Invoke()).OnComplete(() => onComplete?.Invoke());
    }

    private Vector3[] GetPath() 
    {        
        float step = 1f / PATH_RESOLUTION;       

        Vector3 first = _startPoint.position;
        Vector3 second = (_startPoint.position + _lastPoint.position) / 2f + Vector3.up * PATH_HEIGHT;
        Vector3 last = _lastPoint.position;

        List<Vector3> path = new List<Vector3>();
        for (int i = 0; i <= PATH_RESOLUTION; i++)
        {
            path.Add(Utilities.BezierCurve(first, second, last, step * i));
        }
        return path.ToArray();
    }
}
