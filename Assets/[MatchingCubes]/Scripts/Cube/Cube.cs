using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : CollectableBase
{
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody>() : _rigidbody;
    public bool IsBottom { get; set; }
    public bool IsMatched { get; private set; }
    public CubeType CubeType => _cubeType;

    [SerializeField] private CubeType _cubeType;

    [HideInInspector]
    public UnityEvent OnMatched = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnDestroyed= new UnityEvent();

    public void Match() 
    {
        if (IsMatched)
            return;

        IsMatched = true;
        OnMatched.Invoke();
    }    

    public void DestroyCube() 
    {
        OnDestroyed.Invoke();
        Destroy(gameObject);
    }

    public void OnStackMovementStarted() 
    {
        Rigidbody.isKinematic = true;
    }

    public void OnStackMovementCompleted() 
    {
        if (IsBottom && FeverModeManager.Instance.IsFeverModeEnabled)
            return;

        Rigidbody.isKinematic = false;
    }
}
