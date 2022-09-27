using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : CollectableBase
{
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
}
